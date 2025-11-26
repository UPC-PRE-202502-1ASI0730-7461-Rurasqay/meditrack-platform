namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record CreateAdminResource(
    int UserId,
    int OrganizationId,
    string FirstName,
    string LastName
    );