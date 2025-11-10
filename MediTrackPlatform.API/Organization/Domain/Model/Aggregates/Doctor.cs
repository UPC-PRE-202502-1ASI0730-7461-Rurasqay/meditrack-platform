using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Organization.Domain.Model.Aggregates;

public partial class Doctor
{
    public int Id { get; }
    public int UserId { get; private set; }
    public int OrganizationId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public Email Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Specialty { get; private set; }
    public string ImageUrl { get; private set; }

    public Doctor()
    {
        UserId = -1;
        OrganizationId = -1;
        FirstName = string.Empty;
        LastName = string.Empty;
        Age = -1;
        Email = new Email();
        PhoneNumber = string.Empty;
        Specialty = string.Empty;
        ImageUrl = string.Empty;
    }

    public Doctor(CreateDoctorCommand command)
    {
        UserId = command.UserId;
        OrganizationId = command.OrganizationId;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Age = command.Age;
        Email = new Email(command.Email);
        PhoneNumber = command.PhoneNumber;
        Specialty = command.Specialty;
        ImageUrl = command.ImageUrl;
    }

    public string GetFullName() => $"{FirstName} {LastName}";
    public int GetOrganizationId() => OrganizationId;
    public bool BelongsToOrganization(int organizationId) => OrganizationId == organizationId;

    public Doctor UpdatePersonalInformation(UpdateDoctorCommand command)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Age = command.Age;
        Email = new Email(command.Email);
        PhoneNumber = command.PhoneNumber;
        return this;
    }
    
    public Doctor UpdateSpecialty(string specialty)
    {
        Specialty = specialty;
        return this;
    }

    public Doctor UpdateImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
        return this;
    }

    public Doctor AssignUserId(int userId)
    {
        UserId = userId;
        return this;
    }

    public void AssignToSeniorCitizen(int seniorCitizenId, int seniorCitizenOrganizationId)
    {
        if (!BelongsToOrganization(seniorCitizenOrganizationId))
            throw new Exception("Can't assign senior citizen to doctor: They belong to different organizations. " +
                                $"Doctor Org.: {OrganizationId} - Senior Citizen Org.: {seniorCitizenOrganizationId}");
        
        // TODO: Implement Assignment Logic with Event Publishing
    }

    public void UnassignFromSeniorCitizen(int seniorCitizenId, int seniorCitizenOrganizationId)
    {
        if (!BelongsToOrganization(seniorCitizenOrganizationId))
            throw new Exception("Can't unassign senior citizen from doctor: They belong to different organizations. " +
                                $"Doctor Org.: {OrganizationId} - Senior Citizen Org.: {seniorCitizenOrganizationId}");
        
        // TODO: Implement Unassignment Logic with Event Publishing
    }

    public void MarkForDeletion()
    {
        // TODO: Implement Event
    }
}