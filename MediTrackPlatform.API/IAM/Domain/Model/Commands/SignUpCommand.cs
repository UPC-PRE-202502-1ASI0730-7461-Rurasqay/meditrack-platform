namespace MediTrackPlatform.API.IAM.Domain.Model.Commands;

public record SignUpCommand(
    string Email, 
    string Password, 
    string Role,
    string? FirstName = null,
    string? LastName = null,
    string? OrganizationName = null
);