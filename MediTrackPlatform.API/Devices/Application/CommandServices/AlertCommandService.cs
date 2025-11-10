using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Devices.Application.CommandServices;

public class AlertCommandService(IAlertRepository alertRepository, IUnitOfWork unitOfWork) 
    : IAlertCommandService
{
    public async Task<Alert?> Handle(CreateAlertCommand command)
    {
        var alert = new Alert(command);
        await alertRepository.AddAsync(alert);
        await unitOfWork.CompleteAsync();
        return alert;
    }
}