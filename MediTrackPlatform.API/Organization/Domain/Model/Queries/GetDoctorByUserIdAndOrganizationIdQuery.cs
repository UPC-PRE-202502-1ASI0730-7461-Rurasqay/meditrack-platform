namespace MediTrackPlatform.API.Organization.Domain.Model.Queries;

public record GetDoctorByUserIdAndOrganizationIdQuery(int UserId, int OrganizationId);