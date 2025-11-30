using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Commands;
using MediTrackPlatform.API.Relatives.Domain.Model.Entities;
using MediTrackPlatform.API.Relatives.Domain.Model.ValueObjects;
using MediTrackPlatform.API.Relatives.Domain.Repositories;
using MediTrackPlatform.API.Relatives.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Relatives.Application.Internal.CommandServices;

public class RelativeCommandService(
    IRelativeRepository relativeRepository,
    ISeniorCitizenQueryService seniorCitizenQueryService,
    IUnitOfWork unitOfWork
) : IRelativeCommandService
{
    public async Task<Relative?> Handle(CreateRelativeCommand command)
    {
        // 1. Fetch SeniorCitizen from Organization Context
        var query = new GetSeniorCitizenByIdQuery(command.SeniorCitizenId);
        var organizationSeniorCitizen = await seniorCitizenQueryService.Handle(query);
        
        if (organizationSeniorCitizen is null)
            throw new Exception($"Senior citizen with id {command.SeniorCitizenId} not found");

        // 2. Create Local SeniorCitizen (Snapshot)
        var localSeniorCitizen = new SeniorCitizen(
            organizationSeniorCitizen.FirstName,
            organizationSeniorCitizen.LastName,
            organizationSeniorCitizen.Dni,
            organizationSeniorCitizen.Gender,
            organizationSeniorCitizen.Height,
            organizationSeniorCitizen.BirthDate.ToString() ?? "",
            organizationSeniorCitizen.Weight,
            organizationSeniorCitizen.ImageUrl,
            organizationSeniorCitizen.DeviceId
        );

        // 3. Create Relative
        Relative relative;
        if (command.UserId.HasValue && command.UserId > 0)
        {
            relative = new Relative(
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                command.UserId.Value,
                localSeniorCitizen
            );
        }
        else
        {
            relative = new Relative(
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                localSeniorCitizen
            );
        }
        
        // 4. Set Plan
        if (Enum.TryParse<PlanType>(command.PlanType, true, out var planType))
        {
            relative.SetPlan(planType);
        }
        else 
        {
             throw new ArgumentException($"Invalid plan type: {command.PlanType}");
        }

        // 5. Save
        await relativeRepository.AddAsync(relative);
        await unitOfWork.CompleteAsync();
        
        return relative;
    }
}
