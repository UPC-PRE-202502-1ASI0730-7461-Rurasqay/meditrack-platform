namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record CreateAdminCommand(int UserId, int OrganizationId, string FirstName, string LastName);