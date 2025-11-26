using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class AdminCreatedEvent(int adminId, int organizationId) : IEvent
{
    public int AdminId { get; } = adminId;
    public int OrganizationId { get; } = organizationId;
}