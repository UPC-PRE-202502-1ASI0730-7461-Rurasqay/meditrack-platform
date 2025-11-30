using MediTrackPlatform.API.IAM.Application.Internal.OutboundServices;
using MediTrackPlatform.API.IAM.Domain.Model.Queries;
using MediTrackPlatform.API.IAM.Domain.Services;
using MediTrackPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace MediTrackPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        // Skip authorization if the endpoint is marked with [AllowAnonymous] attribute
        var allowAnonymous
            = context.Request.HttpContext.GetEndpoint()!.Metadata
                .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        Console.WriteLine($"Allow Anonymous is {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }

        Console.WriteLine("Entering authorization");
        
        // Get token from request header
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        // If token is null then throw exception
        if (token == null) throw new Exception("Null or invalid token");
        // Validate token
        var userId = await tokenService.ValidateToken(token);
        // If userId is invalid then throw exception
        if (userId == null) throw new Exception("Invalid user id");
        // Get user by id
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        // Set user in HttpContext.Items["User"]
        var user = await userQueryService.Handle(getUserByIdQuery);
        Console.WriteLine("Successful authorization. Updating Context...");
        context.Items["User"] = user;
        Console.WriteLine("Continuing with Middleware Pipeline");
        // Call next middleware
        await next(context);
    }
}