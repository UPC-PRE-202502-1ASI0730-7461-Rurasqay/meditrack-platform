using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOrganizationsConfiguration(this ModelBuilder builder)
    {
        // Organization Bounded Context
        // Admin
        builder.Entity<Admin>().HasKey(a => a.Id);
        builder.Entity<Admin>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Admin>().Property(a => a.UserId);
        builder.Entity<Admin>().Property(a => a.OrganizationId);
        builder.Entity<Admin>().Property(a => a.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<Admin>().Property(a => a.LastName).IsRequired().HasMaxLength(100);
        // Ensure explicit table name matches snake_case plural convention to avoid provider/platform mismatches
        builder.Entity<Admin>().ToTable(typeof(Admin).Name.ToPlural().ToSnakeCase());
        // Caregiver
        builder.Entity<Caregiver>().HasKey(c => c.Id);
        builder.Entity<Caregiver>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Caregiver>().Property(c => c.UserId);
        builder.Entity<Caregiver>().Property(c => c.OrganizationId);
        builder.Entity<Caregiver>().Property(c => c.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<Caregiver>().Property(c => c.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<Caregiver>().Property(c => c.Age);
        builder.Entity<Caregiver>().Property(c => c.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Caregiver>().Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Entity<Caregiver>().Property(c => c.ImageUrl).HasMaxLength(300);
        builder.Entity<Caregiver>().ToTable(typeof(Caregiver).Name.ToPlural().ToSnakeCase());
        // Doctor
        builder.Entity<Doctor>().HasKey(d => d.Id);
        builder.Entity<Doctor>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Doctor>().Property(d => d.UserId);
        builder.Entity<Doctor>().Property(d => d.OrganizationId);
        builder.Entity<Doctor>().Property(d => d.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<Doctor>().Property(d => d.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<Doctor>().Property(d => d.Age);
        builder.Entity<Doctor>().Property(d => d.Email).IsRequired().HasMaxLength(100);
        builder.Entity<Doctor>().Property(d => d.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Entity<Doctor>().Property(d => d.Specialty).IsRequired().HasMaxLength(80);
        builder.Entity<Doctor>().Property(d => d.ImageUrl).HasMaxLength(300);
        builder.Entity<Doctor>().ToTable(typeof(Doctor).Name.ToPlural().ToSnakeCase());
        // Organization
        builder.Entity<Domain.Model.Aggregates.Organization>().HasKey(o => o.Id);
        builder.Entity<Domain.Model.Aggregates.Organization>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Domain.Model.Aggregates.Organization>().Property(o => o.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Domain.Model.Aggregates.Organization>().Property(o => o.Type).IsRequired().HasMaxLength(50);
        builder.Entity<Domain.Model.Aggregates.Organization>().ToTable(typeof(Domain.Model.Aggregates.Organization).Name.ToPlural().ToSnakeCase());
        // Senior Citizen
        builder.Entity<SeniorCitizen>().HasKey(s => s.Id);
        builder.Entity<SeniorCitizen>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SeniorCitizen>().Property(s => s.DeviceId);
        builder.Entity<SeniorCitizen>().Property(s => s.AssignedDoctorId);
        builder.Entity<SeniorCitizen>().Property(s => s.AssignedCaregiverId);
        builder.Entity<SeniorCitizen>().Property(s => s.FirstName).IsRequired().HasMaxLength(100);
        builder.Entity<SeniorCitizen>().Property(s => s.LastName).IsRequired().HasMaxLength(100);
        builder.Entity<SeniorCitizen>().Property(s => s.Dni).IsRequired().HasMaxLength(20);
        builder.Entity<SeniorCitizen>().Property(s => s.BirthDate).IsRequired().HasColumnType("datetime");
        builder.Entity<SeniorCitizen>().Property(s => s.Age);
        builder.Entity<SeniorCitizen>().Property(s => s.Gender).IsRequired().HasMaxLength(50);
        builder.Entity<SeniorCitizen>().Property(s => s.Weight).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.Height).IsRequired();
        builder.Entity<SeniorCitizen>().Property(s => s.ImageUrl).HasMaxLength(300);
        builder.Entity<SeniorCitizen>().ToTable(typeof(SeniorCitizen).Name.ToPlural().ToSnakeCase());
    }
}