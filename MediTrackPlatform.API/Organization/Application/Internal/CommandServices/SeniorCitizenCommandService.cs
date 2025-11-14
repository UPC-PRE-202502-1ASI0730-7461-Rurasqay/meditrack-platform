using Cortex.Mediator;
using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Application.Internal.CommandServices;

public class SeniorCitizenCommandService(
    ISeniorCitizenRepository seniorCitizenRepository,
    IDoctorRepository doctorRepository,
    ICaregiverRepository caregiverRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher
    ) : ISeniorCitizenCommandService
{
    public async Task<int?> Handle(CreateSeniorCitizenCommand command)
    {
        var seniorCitizen = new SeniorCitizen(command);
        await seniorCitizenRepository.AddAsync(seniorCitizen);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenCreatedEvent(seniorCitizen.Id, seniorCitizen.OrganizationId));
        return seniorCitizen.Id;
    }

    public async Task<SeniorCitizen?> Handle(UpdateSeniorCitizenCommand command)
    {
        var result = seniorCitizenRepository.FindByIdAsync(command.SeniorCitizenId);
        if (result.Result is null) throw new Exception();
        var seniorCitizenToUpdate = result.Result;
        var updatedSeniorCitizen = seniorCitizenToUpdate.UpdatePersonalInformation(command);
        seniorCitizenRepository.Update(updatedSeniorCitizen);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenUpdatedEvent(updatedSeniorCitizen.Id, updatedSeniorCitizen.OrganizationId));
        return updatedSeniorCitizen;
    }

    public async Task Handle(DeleteSeniorCitizenCommand command)
    {
        var result = seniorCitizenRepository.FindByIdAsync(command.SeniorCitizenId);
        if (result.Result is null) throw new Exception();
        var seniorCitizenToDelete = result.Result;
        seniorCitizenRepository.Remove(seniorCitizenToDelete);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenDeletedEvent(seniorCitizenToDelete.Id, seniorCitizenToDelete.OrganizationId));
    }

    public async Task<SeniorCitizen?> Handle(AssignSeniorCitizenToDoctorCommand command)
    {
        var seniorCitizenTask = seniorCitizenRepository.FindByIdAsync(command.SeniorCitizenId);
        var doctorTask = doctorRepository.FindByIdAsync(command.DoctorId);
        if (seniorCitizenTask.Result is null || doctorTask.Result is null) throw new Exception();
        var seniorCitizen = seniorCitizenTask.Result;
        var doctor = doctorTask.Result;
        
        seniorCitizen.AssignToDoctor(doctor.Id, doctor.OrganizationId);
        doctor.AssignToSeniorCitizen(seniorCitizen.Id, seniorCitizen.OrganizationId);
        
        seniorCitizenRepository.Update(seniorCitizen);
        doctorRepository.Update(doctor);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenAssignedToDoctorEvent(seniorCitizen.Id, doctor.Id, doctor.OrganizationId));
        
        return seniorCitizen;
    }

    public async Task<SeniorCitizen?> Handle(UnassignSeniorCitizenFromDoctorCommand command)
    {
        var seniorCitizenTask = seniorCitizenRepository.FindByIdAsync(command.SeniorCitizenId);
        var doctorTask = doctorRepository.FindByIdAsync(command.DoctorId);
        if (seniorCitizenTask.Result is null || doctorTask.Result is null) throw new Exception();
        var seniorCitizen = seniorCitizenTask.Result;
        var doctor = doctorTask.Result;
        
        seniorCitizen.UnassignFromDoctor(doctor.Id, doctor.OrganizationId);
        doctor.UnassignFromSeniorCitizen(seniorCitizen.Id, seniorCitizen.OrganizationId);
        
        seniorCitizenRepository.Update(seniorCitizen);
        doctorRepository.Update(doctor);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenUnassignedFromDoctorEvent(seniorCitizen.Id, doctor.Id, doctor.OrganizationId));
        
        return seniorCitizen;
    }

    public async Task<SeniorCitizen?> Handle(AssignSeniorCitizenToCaregiverCommand command)
    {
        var seniorCitizenTask = seniorCitizenRepository.FindByIdAsync(command.SeniorCitizenId);
        var caregiverTask = caregiverRepository.FindByIdAsync(command.CaregiverId);
        if (seniorCitizenTask.Result is null || caregiverTask.Result is null) throw new Exception();
        var seniorCitizen = seniorCitizenTask.Result;
        var caregiver = caregiverTask.Result;
        
        seniorCitizen.AssignToCaregiver(caregiver.Id, caregiver.OrganizationId);
        caregiver.AssignToSenior(seniorCitizen.Id, seniorCitizen.OrganizationId);
        
        seniorCitizenRepository.Update(seniorCitizen);
        caregiverRepository.Update(caregiver);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenAssignedToCaregiverEvent(seniorCitizen.Id, caregiver.Id, caregiver.OrganizationId));
        
        return seniorCitizen;
    }

    public async Task<SeniorCitizen?> Handle(UnassignSeniorCitizenFromCaregiverCommand command)
    {
        var seniorCitizenTask = seniorCitizenRepository.FindByIdAsync(command.SeniorCitizenId);
        var caregiverTask = caregiverRepository.FindByIdAsync(command.CaregiverId);
        if (seniorCitizenTask.Result is null || caregiverTask.Result is null) throw new Exception();
        var seniorCitizen = seniorCitizenTask.Result;
        var caregiver = caregiverTask.Result;
        
        seniorCitizen.UnassignFromCaregiver(caregiver.Id, caregiver.OrganizationId);
        caregiver.UnassignFromSenior(seniorCitizen.Id, seniorCitizen.OrganizationId);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new SeniorCitizenUnassignedFromCaregiverEvent(seniorCitizen.Id, caregiver.Id, caregiver.OrganizationId));
        
        return seniorCitizen;
    }
}