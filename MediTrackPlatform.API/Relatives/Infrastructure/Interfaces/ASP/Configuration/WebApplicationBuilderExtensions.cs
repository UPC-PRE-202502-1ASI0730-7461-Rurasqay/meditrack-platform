using MediTrackPlatform.API.Relatives.Application.Internal.QueryServices;
using MediTrackPlatform.API.Relatives.Domain.Repositories;
using MediTrackPlatform.API.Relatives.Domain.Services;
using MediTrackPlatform.API.Relatives.Infrastructure.Persistence.Repositories;

namespace MediTrackPlatform.API.Relatives.Infrastructure.Interfaces.ASP.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static void AddRelativesContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRelativeRepository, RelativeRepository>();
        builder.Services.AddScoped<IRelativeQueryService, RelativeQueryService>();
    }
}
