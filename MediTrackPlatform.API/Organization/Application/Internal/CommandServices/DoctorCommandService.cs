using Cortex.Mediator;
using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Application.Internal.CommandServices;

public class DoctorCommandService(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher
    ) 
    : IDoctorCommandService
{
    public async Task<int?> Handle(CreateDoctorCommand command)
    {
        var doctor = new Doctor(command);
        await doctorRepository.AddAsync(doctor);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new DoctorCreatedEvent(doctor.Id, doctor.OrganizationId));
        return doctor.Id;
    }

    public async Task<Doctor?> Handle(UpdateDoctorCommand command)
    {
        var result = doctorRepository.FindByIdAsync(command.DoctorId);
        if (result.Result is null) throw new Exception();
        var doctorToUpdate = result.Result;
        var updatedDoctor = doctorToUpdate.UpdatePersonalInformation(command);
        doctorRepository.Update(updatedDoctor);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new DoctorUpdatedEvent(updatedDoctor.Id, updatedDoctor.OrganizationId));
        return updatedDoctor;
    }

    public async Task Handle(DeleteDoctorCommand command)
    {
        var result = doctorRepository.FindByIdAsync(command.DoctorId);
        if (result.Result is null) throw new Exception();
        var doctorToDelete = result.Result;
        doctorRepository.Remove(doctorToDelete);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new DoctorDeletedEvent(doctorToDelete.Id, doctorToDelete.OrganizationId));
    }

    public async Task<Doctor?> Handle(AssignSeniorCitizenToDoctorCommand command)
    {
        var result = doctorRepository.FindByIdAsync(command.DoctorId);
        if (result.Result is null) throw new Exception();
        var doctorToAssign = result.Result;
        
        // Note: The actual assignment logic is handled in SeniorCitizenCommandService
        // This method is kept for consistency but the assignment should be done through SeniorCitizenCommandService
        // which handles the full business logic including validation and mutual exclusion

        await unitOfWork.CompleteAsync();
        return doctorToAssign;
    }

    public async Task<Doctor?> Handle(UnassignSeniorCitizenFromDoctorCommand command)
    {
        var result = doctorRepository.FindByIdAsync(command.DoctorId);
        if (result.Result is null) throw new Exception();
        var doctorToUnassign = result.Result;
        
        // Note: The actual unassignment logic is handled in SeniorCitizenCommandService
        // This method is kept for consistency but the unassignment should be done through SeniorCitizenCommandService
        // which handles the full business logic
        
        await unitOfWork.CompleteAsync();
        return doctorToUnassign;
    }
}