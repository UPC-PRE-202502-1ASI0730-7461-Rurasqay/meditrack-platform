using Cortex.Mediator;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Application.Internal.CommandServices;

public class OrganizationCommandService(
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher
    ) : IOrganizationCommandService
{
    public async Task<int?> Handle(CreateOrganizationCommand command)
    {
        var organization = new Domain.Model.Aggregates.Organization(command);
        await organizationRepository.AddAsync(organization);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new OrganizationCreatedEvent(organization.Id));
        return organization.Id;
    }

    public async Task<Domain.Model.Aggregates.Organization?> Handle(UpdateOrganizationCommand command)
    {
        var result = organizationRepository.FindByIdAsync(command.OrganizationId);
        if (result.Result is null) throw new Exception();
        var organizationToUpdate = result.Result;
        var updatedOrganization = organizationToUpdate.UpdateInformation(command);
        organizationRepository.Update(updatedOrganization);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new OrganizationUpdatedEvent(updatedOrganization.Id));
        return updatedOrganization;
    }
    
    public async Task Handle(DeleteOrganizationCommand command)
    {
        var result = organizationRepository.FindByIdAsync(command.OrganizationId);
        if (result.Result is null) throw new Exception();
        var organizationToDelete = result.Result;
        organizationRepository.Remove(organizationToDelete);
        await unitOfWork.CompleteAsync();
        await domainEventPublisher.PublishAsync(new OrganizationDeletedEvent(organizationToDelete.Id));
    }
}