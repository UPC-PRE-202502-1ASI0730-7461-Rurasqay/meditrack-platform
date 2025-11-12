namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record AdminResource(
    int Id,
    int UserId,
    int OrganizationId,
    string FirstName,
    string LastName,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
    );