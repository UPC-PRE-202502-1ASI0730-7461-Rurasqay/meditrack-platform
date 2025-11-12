using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class DoctorUpdatedEvent(int doctorId, int organizationId) : IEvent
{
    public int DoctorId { get; } = doctorId;
    public int OrganizationId { get; } = organizationId;
}