using MediTrackPlatform.API.IAM.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MediTrackPlatform.API.IAM.Infrastructure.Hashing.BCrypt.Services;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
        => BCryptNet.HashPassword(password);
    public bool VerifyPassword(string password, string passwordHash)
        => BCryptNet.Verify(password, passwordHash);
}