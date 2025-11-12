using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface IAdminCommandService
{
    Task<int?> Handle(CreateAdminCommand command);
    Task<Admin?> Handle(UpdateAdminCommand command);
    Task Handle(DeleteAdminCommand command);
}