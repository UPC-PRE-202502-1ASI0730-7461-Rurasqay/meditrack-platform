using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Model.Entities;

public partial class CaregiverAssignment
{
    public int CaregiverId { get; private set; }
    public int SeniorCitizenId { get; private set; }

    public CaregiverAssignment()
    {
        CaregiverId = -1;
        SeniorCitizenId = -1;
    }

    public CaregiverAssignment(AssignSeniorCitizenToCaregiverCommand command)
    {
        CaregiverId = command.CaregiverId;
        SeniorCitizenId = command.SeniorCitizenId;
    }
}