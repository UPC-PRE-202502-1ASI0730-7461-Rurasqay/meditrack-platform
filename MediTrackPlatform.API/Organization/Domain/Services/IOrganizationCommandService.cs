using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface IOrganizationCommandService
{
    Task<int?> Handle(CreateOrganizationCommand command);
    Task<Model.Aggregates.Organization?> Handle(UpdateOrganizationCommand command);
    Task Handle(DeleteOrganizationCommand command);
}