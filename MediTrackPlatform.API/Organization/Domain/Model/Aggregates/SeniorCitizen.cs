using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Model.Aggregates;

public partial class SeniorCitizen
{
    public int Id { get; }
    public int OrganizationId { get; private set; }
    public int DeviceId { get; private set; }
    public int AssignedDoctorId { get; private set; }
    public int AssignedCaregiverId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Dni { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string ImageUrl { get; private set; }
    public string PlanType { get; private set; }

    public SeniorCitizen()
    {
        OrganizationId = -1;
        DeviceId = -1;
        AssignedDoctorId = -1;
        AssignedCaregiverId = -1;
        FirstName = string.Empty;
        LastName = string.Empty;
        Dni = string.Empty;
        BirthDate = null;
        Age = 0;
        Gender = string.Empty;
        Weight = 0;
        Height = 0;
        ImageUrl = string.Empty;
        PlanType = "freemium";
    }

    public SeniorCitizen(CreateSeniorCitizenCommand command)
    {
        OrganizationId = command.OrganizationId;
        DeviceId = -1; // Will be set automatically after device creation via event handler
        AssignedDoctorId = -1; // No assignment initially
        AssignedCaregiverId = -1; // No assignment initially
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        BirthDate = command.BirthDate;
        Gender = command.Gender;
        Weight = command.Weight;
        Height = command.Height;
        ImageUrl = command.ImageUrl;
        PlanType = "freemium"; // Default plan type
    }

    public string GetFullName() => $"{FirstName} {LastName}";
    public bool BelongsToOrganization(int organizationId) => OrganizationId == organizationId;
    public bool CanBeAssignedToDoctor() => AssignedCaregiverId == -1;
    public bool CanBeAssignedToCaregiver() => AssignedDoctorId == -1;
    public bool IsAssignedToAnyDoctor() => AssignedDoctorId != -1;
    public bool IsAssignedToAnyCaregiver() => AssignedCaregiverId != -1;
    public bool IsAssignedToDoctor(int doctorId) => AssignedDoctorId == doctorId;
    public bool IsAssignedToCaregiver(int caregiverId) => AssignedCaregiverId == caregiverId;
    public bool IsAssignedTo(int personId) => IsAssignedToAnyDoctor() || IsAssignedToAnyCaregiver();

    public SeniorCitizen UpdatePersonalInformation(UpdateSeniorCitizenCommand command)
    {
        OrganizationId = command.OrganizationId;
        DeviceId = command.DeviceId;
        AssignedDoctorId = command.AssignedDoctorId;
        AssignedCaregiverId = command.AssignedCaregiverId;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        BirthDate = command.BirthDate;
        Gender = command.Gender;
        Weight = command.Weight;
        Height = command.Height;
        ImageUrl = command.ImageUrl;
        PlanType = command.PlanType ?? "freemium";
        return this;
    }
    
    public SeniorCitizen UpdateDeviceId(int deviceId)
    {
        DeviceId = deviceId;
        return this;
    }

    public void AssignToDoctor(int doctorId, int doctorOrganizationId)
    {
        if (!BelongsToOrganization(doctorOrganizationId))
            throw new Exception("Cannot assign senior citizen to doctor: They belong to different organizations. " +
                                $"Senior Citizen Org.: {OrganizationId} - Doctor Org.: {doctorOrganizationId}");

        if (!CanBeAssignedToDoctor())
            throw new Exception("Cannot assign senior citizen to doctor because they're assigned to a caregiver. " +
                                $"Current Caregiver: {AssignedCaregiverId}");
        
        int previousDoctorId = AssignedDoctorId;
        if (IsAssignedToAnyDoctor())
        {
            UnassignFromDoctor(AssignedDoctorId, OrganizationId);
            // TODO: Trigger Unassignment Event!
        }
        
        AssignedDoctorId = doctorId;
        AssignedCaregiverId = -1; // Unassigned
        // TODO: Trigger Assignment Event!
    }

    public void UnassignFromDoctor(int doctorId, int doctorOrganizationId)
    {
        if (!BelongsToOrganization(doctorOrganizationId))
            throw new Exception("Cannot unassign senior citizen from doctor: They belong to different organizations " +
                                $"(Senior Citizen Org.: {OrganizationId} -  Doctor Org.: {doctorOrganizationId})");

        if (!IsAssignedToDoctor(doctorId))
            throw new Exception("Cannot unassign senior citizen from a doctor they aren't assigned to. " +
                                $"(Current doctor: {AssignedDoctorId} - Doctor To Unassign: {doctorId})");
        
        AssignedDoctorId = -1; // Unassigned
        // TODO: Trigger Event!
    }

    public void AssignToCaregiver(int caregiverId, int caregiverOrganizationId)
    {
        if (!BelongsToOrganization(caregiverOrganizationId))
            throw new Exception("Cannot assign senior citizen to caregiver because they belong to different organizations. " +
                                $"(Senior Citizen Org.: {OrganizationId} - Caregiver Org.: {caregiverOrganizationId})");

        if (!CanBeAssignedToCaregiver())
            throw new Exception("Cannot assign senior citizen to caregiver because they're assigned to a doctor. " +
                                $"Current Doctor: {AssignedDoctorId}");
        
        int previousCaregiverId = AssignedCaregiverId;
        if (IsAssignedToAnyCaregiver())
        {
            UnassignFromCaregiver(AssignedCaregiverId, OrganizationId);
            // TODO: Trigger Event!
        }
        
        AssignedCaregiverId = caregiverId;
        AssignedDoctorId = -1; // Unassigned
        // TODO: Trigger Event!
    }

    public void UnassignFromCaregiver(int caregiverId, int caregiverOrganizationId)
    {
        if (!BelongsToOrganization(caregiverOrganizationId))
            throw new Exception("Cannot unassign senior citizen from caregiver: They belong to different organizations. " +
                                $"(Senior Citizen Org.: {OrganizationId} - Caregiver Org.: {caregiverOrganizationId})");

        if (!IsAssignedToCaregiver(caregiverId))
            throw new Exception("Cannot unassign senior citizen from a caregiver they aren't assigned to. " +
                                $"Current Caregiver: {AssignedCaregiverId} - Caregiver To Unassign: {caregiverId}");
        
        AssignedCaregiverId = -1; // Unassigned
        // TODO: Trigger Event!
    }

    public void MarkForDeletion()
    {
        // TODO: Call Event!
    }
}