using MediTrackPlatform.API.IAM.Domain.Model.Aggregates;
using MediTrackPlatform.API.IAM.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
        => new AuthenticatedUserResource(user.Id, user.Email, token);
}