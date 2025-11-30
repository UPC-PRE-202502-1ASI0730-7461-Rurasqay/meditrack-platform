namespace MediTrackPlatform.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Email, 
    string Password, 
    string Role,
    string? FirstName,
    string? LastName,
    string? OrganizationName
);