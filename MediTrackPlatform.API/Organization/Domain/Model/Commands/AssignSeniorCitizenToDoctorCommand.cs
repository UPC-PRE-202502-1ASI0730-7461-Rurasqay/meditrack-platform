namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record AssignSeniorCitizenToDoctorCommand(int SeniorCitizenId, int DoctorId);