using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

namespace MediTrackPlatform.API.Organization.Infrastructure.Interfaces.ASP.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static void AddOrganizationContextServices(this WebApplicationBuilder builder)
    {
        // Organization Bounded Context
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<ICaregiverRepository, CaregiverRepository>();
        builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
        builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        builder.Services.AddScoped<ISeniorCitizenRepository, SeniorCitizenRepository>();
    }
}