using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Organization.Domain.Model.Events;

public class SeniorCitizenAssignedToCaregiverEvent(int seniorCitizenId, int caregiverId, int organizationId) : IEvent
{
    public int SeniorCitizenId { get; } = seniorCitizenId;
    public int CaregiverId { get; } = caregiverId;
    public int OrganizationId { get; } = organizationId;
}