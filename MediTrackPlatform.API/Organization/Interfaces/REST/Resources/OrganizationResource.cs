namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record OrganizationResource(
    int Id,
    string Name,
    string Type,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
    );