namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record CreateOrganizationResource(
    string Name,
    string Type
    );