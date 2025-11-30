using System.Text.Json.Serialization;

namespace MediTrackPlatform.API.IAM.Domain.Model.Aggregates;

public class User(string email, string passwordHash, string role)
{
    public User() : this(string.Empty, string.Empty, string.Empty) {}

    public int Id { get; }
    public string Email { get; private set; } = email;
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    public string Role { get; private set; } = role;

    public User UpdateEmail(string email)
    {
        Email = email;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
    
    public User UpdateRole(string role)
    {
        Role = role;
        return this;
    }
}