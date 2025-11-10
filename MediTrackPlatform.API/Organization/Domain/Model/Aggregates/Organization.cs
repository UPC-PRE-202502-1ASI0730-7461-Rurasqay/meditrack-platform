using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Model.Aggregates;

public partial class Organization
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Type { get; private set; }

    public Organization()
    {
        Name = string.Empty;
        Type = string.Empty;
    }

    public Organization(CreateOrganizationCommand command)
    {
        Name = command.Name;
        Type = command.Type;
    }

    public bool IsClinic()
    {
        return string.Equals("clinic", Type);
    }

    public bool IsResidence()
    {
        return string.Equals("residence", Type);
    }
    
    // TODO: Add events
}