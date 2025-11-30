using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Configuration.Extensions;
using MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using MediTrackPlatform.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using MediTrackPlatform.API.Relatives.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyDevicesConfiguration();
        builder.ApplyOrganizationsConfiguration();
        builder.ApplyIamConfiguration();
        builder.ApplyRelativesConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}