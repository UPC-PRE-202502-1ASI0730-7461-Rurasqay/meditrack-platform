using MediTrackPlatform.API.IAM.Domain.Model.Aggregates;
using MediTrackPlatform.API.IAM.Domain.Model.Queries;

namespace MediTrackPlatform.API.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
}