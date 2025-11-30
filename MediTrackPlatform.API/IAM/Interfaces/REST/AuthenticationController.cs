using System.Net.Mime;
using MediTrackPlatform.API.IAM.Domain.Services;
using MediTrackPlatform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using MediTrackPlatform.API.IAM.Interfaces.REST.Resources;
using MediTrackPlatform.API.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediTrackPlatform.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication Endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign In",
        Description = "Sign in a user",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var resource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        return Ok(resource);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign-up",
        Description = "Sign up a new user",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status201Created, "The user was created successfully", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var user = await userCommandService.Handle(signUpCommand);
        
        if (user is null) return BadRequest();

        var signInCommand = new Domain.Model.Commands.SignInCommand(signUpResource.Email, signUpResource.Password);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        
        var resource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        return CreatedAtAction(nameof(SignIn), resource);
    }
}