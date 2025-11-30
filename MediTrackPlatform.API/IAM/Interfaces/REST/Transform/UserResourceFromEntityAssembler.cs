using MediTrackPlatform.API.IAM.Domain.Model.Aggregates;
using MediTrackPlatform.API.IAM.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
        => new UserResource(user.Id, user.Email);
}