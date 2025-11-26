using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Model.Aggregates;

public partial class Caregiver
{
    public int Id { get; }
    public int UserId { get; private set; }
    public int OrganizationId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string ImageUrl { get; private set; }

    public Caregiver()
    {
        UserId = -1;
        OrganizationId = -1;
        FirstName = string.Empty;
        LastName = string.Empty;
        Age = 0;
        Email = string.Empty;
        PhoneNumber = string.Empty;
        ImageUrl = string.Empty;
    }

    public Caregiver(CreateCaregiverCommand command)
    {
        UserId = command.UserId;
        OrganizationId = command.OrganizationId;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Age = command.Age;
        Email = command.Email;
        PhoneNumber = command.PhoneNumber;
        ImageUrl = command.ImageUrl;
    }
    
    public string GetFullName() => $"{FirstName} {LastName}";
    public bool BelongsToOrganization(int organizationId) => organizationId == OrganizationId;

    public Caregiver UpdatePersonalInformation(UpdateCaregiverCommand command)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Age = command.Age;
        Email = command.Email;
        PhoneNumber = command.PhoneNumber;
        return this;
    }

    public Caregiver UpdateImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
        return this;
    }

    public Caregiver AssignUserId(int userId)
    {
        UserId = userId;
        return this;
    }

    public void AssignToSenior(int seniorCitizenId, int seniorCitizenOrganizationId)
    {
        if (!BelongsToOrganization(seniorCitizenOrganizationId))
            throw new Exception("Cannot assign senior citizen to caregiver: They belong to different organizations. " +
                                $"(Caregiver Org.: {OrganizationId} - Senior Citizen Org.: {seniorCitizenOrganizationId})");
        
        // TODO: Assign To Senior Citizen + Call Event
    }

    public void UnassignFromSenior(int seniorCitizenId, int seniorCitizenOrganizationId)
    {
        if (!BelongsToOrganization(seniorCitizenOrganizationId))
            throw new Exception("Cannot unassign senior citizen from caregiver: They belong to different organizations. " +
                                $"(Caregiver Org.: {OrganizationId} - Senior Citizen Org.: {seniorCitizenOrganizationId})");
        
        // TODO: Unassign From Senior Citizen + Call Event
    }

    public void MarkForDeletion()
    {
        // TODO: Trigger event!
    }
}