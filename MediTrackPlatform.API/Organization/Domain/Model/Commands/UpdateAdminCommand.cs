namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateAdminCommand(int AdminId, int FirstName, int LastName);