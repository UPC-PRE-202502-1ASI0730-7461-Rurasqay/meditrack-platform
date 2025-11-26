using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Model.Entities;

public partial class DoctorAssignment
{
    public int DoctorId { get; private set; }
    public int SeniorCitizenId { get; private set; }

    public DoctorAssignment()
    {
        DoctorId = -1;
        SeniorCitizenId = -1;
    }

    public DoctorAssignment(AssignSeniorCitizenToDoctorCommand command)
    {
        DoctorId = command.DoctorId;
        SeniorCitizenId = command.SeniorCitizenId;
    }
}