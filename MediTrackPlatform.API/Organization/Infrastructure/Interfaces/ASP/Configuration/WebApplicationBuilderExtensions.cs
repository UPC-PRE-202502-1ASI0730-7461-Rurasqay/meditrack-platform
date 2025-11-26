using MediTrackPlatform.API.Organization.Application.Internal.CommandServices;
using MediTrackPlatform.API.Organization.Application.Internal.QueryServices;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;
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
        builder.Services.AddScoped<IAdminCommandService, AdminCommandService>();
        builder.Services.AddScoped<IAdminQueryService, AdminQueryService>();
        builder.Services.AddScoped<ICaregiverCommandService, CaregiverCommandService>();
        builder.Services.AddScoped<ICaregiverQueryService, CaregiverQueryService>();
        builder.Services.AddScoped<IDoctorCommandService, DoctorCommandService>();
        builder.Services.AddScoped<IDoctorQueryService, DoctorQueryService>();
        builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
        builder.Services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();
        builder.Services.AddScoped<ISeniorCitizenCommandService, SeniorCitizenCommandService>();
        builder.Services.AddScoped<ISeniorCitizenQueryService, SeniorCitizenQueryService>();
    }
}