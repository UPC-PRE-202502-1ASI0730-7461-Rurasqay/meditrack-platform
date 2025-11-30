using MediTrackPlatform.API.IAM.Domain.Model.Aggregates;
using MediTrackPlatform.API.IAM.Domain.Model.Queries;
using MediTrackPlatform.API.IAM.Domain.Repositories;
using MediTrackPlatform.API.IAM.Domain.Services;

namespace MediTrackPlatform.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}