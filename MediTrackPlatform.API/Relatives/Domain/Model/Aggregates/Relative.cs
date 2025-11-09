namespace MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;

public partial class Relative
{
    public int Id { get; private set; }
    public string Plan { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string ProfileImage { get; private set; } = string.Empty;
    public int SeniorCitizenId { get; private set; }
    
    public SeniorCitizen SeniorCitizen { get; private set; }
    
    public Relative()
    {
    }
}