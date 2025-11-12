namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UnassignSeniorCitizenFromDoctorCommand(int SeniorCitizenId, int DoctorId);