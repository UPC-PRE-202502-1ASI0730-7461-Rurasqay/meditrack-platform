using MediTrackPlatform.API.Relatives.Domain.Model.Entities;
using MediTrackPlatform.API.Relatives.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;

public partial class Relative
{
    public int Id { get; private set; }
    
    public PlanType Plan { get; private set; } = PlanType.Freemium;

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public int? UserId { get; private set; }
    
    public int SeniorCitizenId { get; private set; }
    public SeniorCitizen SeniorCitizen { get; private set; }

    public Relative()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
        SeniorCitizen = new SeniorCitizen();
    }

    public Relative(string firstName, string lastName, string phoneNumber, SeniorCitizen seniorCitizen)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        SeniorCitizen = seniorCitizen;
        Plan = PlanType.Freemium;
    }

    public Relative(string firstName, string lastName, string phoneNumber, int userId, SeniorCitizen seniorCitizen) : this(firstName, lastName, phoneNumber, seniorCitizen)
    {
        UserId = userId;
    }
    
    public void SetPlan(PlanType plan)
    {
        Plan = plan;
    }
}