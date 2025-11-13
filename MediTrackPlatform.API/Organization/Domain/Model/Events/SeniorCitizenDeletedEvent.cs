using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class SeniorCitizenDeletedEvent(int seniorCitizenId, int organizationId) : IEvent
{
    public int SeniorCitizenId { get; } = seniorCitizenId;
    public int OrganizationId { get; } = organizationId;
}