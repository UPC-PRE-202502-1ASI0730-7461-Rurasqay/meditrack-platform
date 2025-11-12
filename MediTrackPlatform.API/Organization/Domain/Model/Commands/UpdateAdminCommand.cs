namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateAdminCommand(int AdminId, string FirstName, string LastName);