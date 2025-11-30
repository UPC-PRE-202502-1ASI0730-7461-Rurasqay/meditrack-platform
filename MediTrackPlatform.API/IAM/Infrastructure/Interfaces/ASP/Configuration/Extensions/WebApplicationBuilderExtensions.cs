using MediTrackPlatform.API.IAM.Application.Internal.CommandServices;
using MediTrackPlatform.API.IAM.Application.Internal.OutboundServices;
using MediTrackPlatform.API.IAM.Application.Internal.QueryServices;
using MediTrackPlatform.API.IAM.Domain.Repositories;
using MediTrackPlatform.API.IAM.Domain.Services;
using MediTrackPlatform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using MediTrackPlatform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using MediTrackPlatform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using MediTrackPlatform.API.IAM.Infrastructure.Tokens.JWT.Services;
using MediTrackPlatform.API.IAM.Interfaces.ACL;
using MediTrackPlatform.API.IAM.Interfaces.ACL.Services;

namespace MediTrackPlatform.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddIamContextServices(this WebApplicationBuilder builder)
    {
        // IAM Bounded Context Injection Configuration

        // TokenSettings Configuration
        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

        // Service dependency injection configuration
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserCommandService, UserCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IHashingService, HashingService>();
        builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
    }

    public static void AddCorsPolicy(this WebApplicationBuilder builder)
    {
        // Add CORS Policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
}