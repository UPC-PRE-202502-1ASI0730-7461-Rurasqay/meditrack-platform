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
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
            options.EnableAnnotations();
        });
    }
}