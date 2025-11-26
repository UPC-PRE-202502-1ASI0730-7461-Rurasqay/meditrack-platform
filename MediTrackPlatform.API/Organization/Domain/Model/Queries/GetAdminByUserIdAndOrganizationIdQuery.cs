namespace MediTrackPlatform.API.Organization.Domain.Model.Queries;

public record GetAdminByUserIdAndOrganizationIdQuery(int UserId, int OrganizationId);