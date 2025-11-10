using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Model.Aggregates;

public partial class Admin
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int OrganizationId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Admin()
    {
        UserId = -1;
        OrganizationId = -1;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public Admin(CreateAdminCommand command)
    {
        UserId = command.UserId;
        OrganizationId = command.OrganizationId;
        FirstName = command.FirstName;
        LastName = command.LastName;
    }
}