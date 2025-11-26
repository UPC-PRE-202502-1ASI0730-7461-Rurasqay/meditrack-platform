namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record AssignSeniorCitizenToCaregiverCommand(int SeniorCitizenId, int CaregiverId);