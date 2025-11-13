namespace MediTrackPlatform.API.Organization.Domain.Model.Queries;

public record GetCaregiverByUserIdAndOrganizationIdQuery(int UserId, int OrganizationId);