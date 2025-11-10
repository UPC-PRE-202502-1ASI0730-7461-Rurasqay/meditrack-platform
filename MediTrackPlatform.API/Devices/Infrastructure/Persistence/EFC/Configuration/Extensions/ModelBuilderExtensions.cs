using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDevicesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>().HasKey(t => t.AlertId);
        builder.Entity<Alert>().Property(t => t.AlertId).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Alert>().Property(t => t.DeviceId).IsRequired();
        builder.Entity<Alert>().Property(t => t.EAlertType).IsRequired();
        builder.Entity<Alert>().Property(t => t.Message).IsRequired().HasMaxLength(500);
        builder.Entity<Alert>().Property(t => t.DataRegistered).IsRequired();
        builder.Entity<Alert>().Property(t => t.RegisteredAt).IsRequired();
    
        builder.Entity<Device>().HasKey(t => t.DeviceId);
        builder.Entity<Device>().Property(t => t.DeviceId).IsRequired();
        builder.Entity<Device>().Property(t => t.Model).IsRequired();
        builder.Entity<Device>().Property(t => t.Status).IsRequired();
        builder.Entity<Device>().OwnsOne<Holder>(t => t.Holder);
    
    }
}