using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Entities;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Relatives.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyRelativesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Relative>().HasKey(r => r.Id);
        builder.Entity<Relative>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Relative>().Property(r => r.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<Relative>().Property(r => r.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<Relative>().Property(r => r.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Entity<Relative>().Property(r => r.Plan).IsRequired().HasConversion<string>();
        
        // Relationship with SeniorCitizen (Local Entity)
        builder.Entity<Relative>()
            .HasOne(r => r.SeniorCitizen)
            .WithOne()
            .HasForeignKey<Relative>(r => r.SeniorCitizenId)
            .IsRequired();

        builder.Entity<Relative>().ToTable(typeof(Relative).Name.ToPlural().ToSnakeCase());
        
        // SeniorCitizen (Local Entity)
        builder.Entity<SeniorCitizen>().HasKey(s => s.Id);
        builder.Entity<SeniorCitizen>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SeniorCitizen>().Property(s => s.FirstName).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.LastName).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.Dni).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.Gender).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.Height).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.BirthDate).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.Weight).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.ProfileImage).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.DeviceId).IsRequired();
        
        builder.Entity<SeniorCitizen>().ToTable("relatives_senior_citizens");
    }
}
