using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class OrganizationUpdatedEvent(int organizationId) : IEvent
{
    public int OrganizationId { get; } = organizationId;
}