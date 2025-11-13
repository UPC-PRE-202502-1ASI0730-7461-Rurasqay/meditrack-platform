using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class CaregiverUpdatedEvent(int caregiverId, int organizationId) : IEvent
{
    public int CaregiverId { get; } = caregiverId;
    public int OrganizationId { get; } = organizationId;
}