using System.Runtime.InteropServices.JavaScript;

namespace MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;

public partial class SeniorCitizen
{
    public int SeniorCitizenId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Dni { get; private set; } = string.Empty;
    public string Gender { get; private set; } = string.Empty;
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string ProfileImage { get; private set; } = string.Empty;
    public string DeviceId { get; private set; } = string.Empty;
    
    public SeniorCitizen()
    {
    }
}