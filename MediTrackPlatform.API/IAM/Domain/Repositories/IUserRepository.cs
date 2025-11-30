using MediTrackPlatform.API.IAM.Domain.Model.Aggregates;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    bool ExistsByEmail(string email);
}