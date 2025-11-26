namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateOrganizationCommand(int OrganizationId, string Name, string Type);