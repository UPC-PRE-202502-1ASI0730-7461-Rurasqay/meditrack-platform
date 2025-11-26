using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDevicesConfiguration(this ModelBuilder builder)
    {
        // Alert Configuration
        builder.Entity<Alert>().HasKey(t => t.AlertId);
        builder.Entity<Alert>().Property(t => t.AlertId).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(t => t.DeviceId).IsRequired();
        builder.Entity<Alert>().Property(t => t.EAlertType).IsRequired();
        builder.Entity<Alert>().Property(t => t.Message).IsRequired().HasMaxLength(500);
        builder.Entity<Alert>().Property(t => t.DataRegistered).IsRequired();
        builder.Entity<Alert>().Property(t => t.RegisteredAt).IsRequired();
    
        // Device Configuration
        builder.Entity<Device>().HasKey(t => t.DeviceId);
        builder.Entity<Device>().Property(t => t.DeviceId).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Device>().Property(t => t.Model).IsRequired().HasMaxLength(100);
        builder.Entity<Device>().Property(t => t.Status).IsRequired();
        
        // Owned Entity: Holder
        builder.Entity<Device>().OwnsOne(t => t.Holder, h =>
        {
            h.Property(p => p.HolderId).IsRequired();
            h.Property(p => p.HolderType).IsRequired().HasMaxLength(50);
        });
        
        // Device-Measurements Relationship (One-to-Many)
        builder.Entity<Device>()
            .HasMany(d => d.Measurements)
            .WithOne()
            .HasForeignKey("DeviceId")
            .OnDelete(DeleteBehavior.Cascade);
        
        // Measurement Configuration (Base class with TPH - Table Per Hierarchy)
        builder.Entity<Measurement>().HasKey(m => m.MeasurementId);
        builder.Entity<Measurement>().Property(m => m.MeasurementId).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Measurement>().Property(m => m.MeasuredAt).IsRequired();
        
        // TPH Discriminator - usa la propiedad Type como discriminador
        builder.Entity<Measurement>()
            .HasDiscriminator(m => m.Type)
            .HasValue<TemperatureMeasurement>(EMeasurementType.Temperature)
            .HasValue<HeartRateMeasurement>(EMeasurementType.HeartRate)
            .HasValue<OxygenMeasurement>(EMeasurementType.Oxygen)
            .HasValue<BloodPressureMeasurement>(EMeasurementType.BloodPressure);
        
        // TemperatureMeasurement Configuration
        builder.Entity<TemperatureMeasurement>()
            .Property(t => t.Celcius).IsRequired().HasColumnName("temperature_celsius");
        builder.Entity<TemperatureMeasurement>()
            .OwnsOne(t => t.Threshold, th =>
            {
                th.WithOwner().HasForeignKey("measurement_id");
                th.Property(p => p.Min).IsRequired().HasColumnName("temperature_threshold_min");
                th.Property(p => p.Max).IsRequired().HasColumnName("temperature_threshold_max");
            });
        
        // HeartRateMeasurement Configuration
        builder.Entity<HeartRateMeasurement>()
            .Property(h => h.Bpm).IsRequired().HasColumnName("heart_rate_bpm");
        builder.Entity<HeartRateMeasurement>()
            .OwnsOne(h => h.Threshold, th =>
            {
                th.WithOwner().HasForeignKey("measurement_id");
                th.Property(p => p.Min).IsRequired().HasColumnName("heart_rate_threshold_min");
                th.Property(p => p.Max).IsRequired().HasColumnName("heart_rate_threshold_max");
            });
        
        // OxygenMeasurement Configuration
        builder.Entity<OxygenMeasurement>()
            .Property(o => o.Spo2).IsRequired().HasColumnName("oxygen_spo2");
        builder.Entity<OxygenMeasurement>()
            .OwnsOne(o => o.Threshold, th =>
            {
                th.WithOwner().HasForeignKey("measurement_id");
                th.Property(p => p.Min).IsRequired().HasColumnName("oxygen_threshold_min");
            });
        
        // BloodPressureMeasurement Configuration
        builder.Entity<BloodPressureMeasurement>()
            .Property(b => b.Diastolic).IsRequired().HasColumnName("blood_pressure_diastolic");
        builder.Entity<BloodPressureMeasurement>()
            .Property(b => b.Systolic).IsRequired().HasColumnName("blood_pressure_systolic");
        builder.Entity<BloodPressureMeasurement>()
            .OwnsOne(b => b.Threshold, th =>
            {
                th.WithOwner().HasForeignKey("measurement_id");
                th.Property(p => p.Min).IsRequired().HasColumnName("blood_pressure_threshold_min");
                th.Property(p => p.Max).IsRequired().HasColumnName("blood_pressure_threshold_max");
            });
    }
}