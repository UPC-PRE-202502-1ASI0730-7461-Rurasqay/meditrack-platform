using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class SeniorCitizenUnassignedFromDoctorEvent(int seniorCitizenId, int doctorId, int organizationId) : IEvent
{
    public int SeniorCitizenId { get; } = seniorCitizenId;
    public int DoctorId { get; } = doctorId;
    public int OrganizationId { get; } = organizationId;
}