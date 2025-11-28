using MediTrackPlatform.API.IAM.Domain.Model.Commands;
using MediTrackPlatform.API.IAM.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
        => new SignInCommand(resource.Username, resource.Password);
}