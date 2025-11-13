using Cortex.Mediator;
using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Application.Internal.CommandServices;

public class AdminCommandService(
    IAdminRepository adminRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher)
    : IAdminCommandService
{
    public async Task<int?> Handle(CreateAdminCommand command)
    {
        var admin = new Admin(command);
        await adminRepository.AddAsync(admin);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new AdminCreatedEvent(admin.Id, admin.OrganizationId));
        return admin.Id;
    }

    public async Task<Admin?> Handle(UpdateAdminCommand command)
    {
        var result = adminRepository.FindByIdAsync(command.AdminId);
        if (result.Result is null) throw new Exception();
        var adminToUpdate = result.Result;
        var updatedAmin = adminToUpdate.UpdatePersonalInformation(command);
        adminRepository.Update(updatedAmin);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new AdminUpdatedEvent(updatedAmin.Id, updatedAmin.OrganizationId));
        return updatedAmin;
    }

    public async Task Handle(DeleteAdminCommand command)
    {
        var result = adminRepository.FindByIdAsync(command.AdminId);
        if (result.Result is null) throw new Exception();
        var adminToDelete = result.Result;
        adminRepository.Remove(adminToDelete);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new AdminDeletedEvent(adminToDelete.Id, adminToDelete.OrganizationId));
    }
}