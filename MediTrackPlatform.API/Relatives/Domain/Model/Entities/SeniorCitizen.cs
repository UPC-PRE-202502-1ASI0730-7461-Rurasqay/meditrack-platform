namespace MediTrackPlatform.API.Relatives.Domain.Model.Entities;

public class SeniorCitizen
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Dni { get; set; }
    public string Gender { get; set; }
    public double Height { get; set; }
    public string BirthDate { get; set; }
    public double Weight { get; set; }
    public string ProfileImage { get; set; }
    public int DeviceId { get; set; }

    public SeniorCitizen()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Dni = string.Empty;
        Gender = string.Empty;
        BirthDate = string.Empty;
        ProfileImage = string.Empty;
        DeviceId = 0;
    }

    public SeniorCitizen(string firstName, string lastName, string dni, string gender, double height, string birthDate, double weight, string profileImage, int deviceId)
    {
        FirstName = firstName;
        LastName = lastName;
        Dni = dni;
        Gender = gender;
        Height = height;
        BirthDate = birthDate;
        Weight = weight;
        ProfileImage = profileImage;
        DeviceId = deviceId;
    }
}
