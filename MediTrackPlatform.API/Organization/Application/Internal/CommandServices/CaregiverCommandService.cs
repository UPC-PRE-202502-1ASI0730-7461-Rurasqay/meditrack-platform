using Cortex.Mediator;
using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Application.Internal.CommandServices;

public class CaregiverCommandService(
    ICaregiverRepository caregiverRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher)
    : ICaregiverCommandService
{
    public async Task<int?> Handle(CreateCaregiverCommand command)
    {
        var caregiver = new Caregiver(command);
        await caregiverRepository.AddAsync(caregiver);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new CaregiverCreatedEvent(caregiver.Id, caregiver.OrganizationId));
        return caregiver.Id;
    }

    public async Task<Caregiver?> Handle(UpdateCaregiverCommand command)
    {
        var result = caregiverRepository.FindByIdAsync(command.CaregiverId);
        if (result.Result is null) throw new Exception();
        var caregiverToUpdate = result.Result;
        var updatedCaregiver = caregiverToUpdate.UpdatePersonalInformation(command);
        caregiverRepository.Update(updatedCaregiver);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new CaregiverUpdatedEvent(updatedCaregiver.Id, updatedCaregiver.OrganizationId));
        return updatedCaregiver;
    }

    public async Task Handle(DeleteCaregiverCommand command)
    {
        var result = caregiverRepository.FindByIdAsync(command.CaregiverId);
        if (result.Result is null) throw new Exception();
        var caregiverToDelete = result.Result;
        caregiverRepository.Remove(caregiverToDelete);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new CaregiverDeletedEvent(caregiverToDelete.Id, caregiverToDelete.OrganizationId));
    }

    public async Task<Caregiver?> Handle(AssignSeniorCitizenToCaregiverCommand command)
    {
        var result = caregiverRepository.FindByIdAsync(command.CaregiverId);
        if (result.Result is null) throw new Exception();
        var caregiverToAssign = result.Result;
        
        // Note: The actual assignment logic is handled in SeniorCitizenCommandService
        // This method is kept for consistency but the assignment should be done through SeniorCitizenCommandService
        // which handles the full business logic including validation and mutual exclusion
        
        await unitOfWork.CompleteAsync();
        return caregiverToAssign;
    }

    public async Task<Caregiver?> Handle(UnassignSeniorCitizenFromCaregiverCommand command)
    {
        var result = caregiverRepository.FindByIdAsync(command.CaregiverId);
        if (result.Result is null) throw new Exception();
        var caregiverToUnassign = result.Result;
        
        // Note: The actual unassignment logic is handled in SeniorCitizenCommandService
        // This method is kept for consistency but the unassignment should be done through SeniorCitizenCommandService
        // which handles the full business logic
        
        await unitOfWork.CompleteAsync();
        return caregiverToUnassign;
    }
}