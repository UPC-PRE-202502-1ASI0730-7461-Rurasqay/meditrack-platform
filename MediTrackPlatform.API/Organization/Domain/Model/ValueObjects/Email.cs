namespace MediTrackPlatform.API.Organization.Domain.Model.ValueObjects;

public record Email(string Address)
{
    public Email() : this(string.Empty) {}
}