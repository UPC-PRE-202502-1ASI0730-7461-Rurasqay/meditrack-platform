using Microsoft.OpenApi.Models;

namespace MediTrackPlatform.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddOpenApiDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "MediTrackPlatform.API",
                    Version = "v1",
                    Description = "MediTrack Platform API",
                    Contact = new OpenApiContact
                    {
                        Name = "MediTrack Studios (Rurasqay)",
                        Email = "contact@meditrack.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });
            
            // TODO: Add security definition when IAM is implemented and enabled
            
            options.EnableAnnotations();
        });
    }
}