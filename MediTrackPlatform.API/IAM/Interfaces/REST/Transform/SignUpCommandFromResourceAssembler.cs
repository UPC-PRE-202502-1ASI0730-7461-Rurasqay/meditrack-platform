using MediTrackPlatform.API.IAM.Domain.Model.Commands;
using MediTrackPlatform.API.IAM.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        => new SignUpCommand(resource.Username, resource.Password);
}