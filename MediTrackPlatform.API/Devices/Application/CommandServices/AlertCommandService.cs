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
        var alert = new Alert
        {
            DeviceId = command.DeviceId.ToString(),
            EAlertType = command.Type,
            Message = command.Message,
            DataRegistered = command.DataRegistered,
            RegisteredAt = command.RegisteredAt
        };

        try
        {
            await alertRepository.AddAsync(alert);
            
            await unitOfWork.CompleteAsync();
            
            return alert;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the alert: {e.Message}");
            return null;
        }
    }
}