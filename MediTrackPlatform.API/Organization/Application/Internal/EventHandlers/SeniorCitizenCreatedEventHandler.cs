using MediTrackPlatform.API.Devices.Interfaces.ACL;
using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

/// <summary>
/// Event handler for SeniorCitizenCreatedEvent
/// Automatically creates a device for the senior citizen using the Devices ACL
/// </summary>
/// <param name="devicesContextFacade">The Devices context facade (ACL)</param>
/// <param name="seniorCitizenRepository">The senior citizen repository</param>
/// <param name="unitOfWork">The unit of work</param>
public class SeniorCitizenCreatedEventHandler(
    IDevicesContextFacade devicesContextFacade,
    ISeniorCitizenRepository seniorCitizenRepository,
    IUnitOfWork unitOfWork
) : IEventHandler<SeniorCitizenCreatedEvent>
{
    public Task Handle(SeniorCitizenCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private async Task On(SeniorCitizenCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Senior Citizen: {0}", domainEvent.SeniorCitizenId);
        
        // Automatically create a device for the senior citizen via ACL
        try
        {
            var deviceModel = "MediTrack-SmartWatch-v1"; // Default device model
            var deviceId = await devicesContextFacade.CreateDevice(deviceModel, domainEvent.SeniorCitizenId);
            
            if (deviceId > 0)
            {
                Console.WriteLine("Automatically created Device {0} for Senior Citizen {1}", 
                    deviceId, domainEvent.SeniorCitizenId);
                
                // Update the SeniorCitizen with the new device ID
                var seniorCitizen = await seniorCitizenRepository.FindByIdAsync(domainEvent.SeniorCitizenId);
                if (seniorCitizen is not null)
                {
                    seniorCitizen.UpdateDeviceId(deviceId);
                    seniorCitizenRepository.Update(seniorCitizen);
                    await unitOfWork.CompleteAsync();
                    Console.WriteLine("Updated Senior Citizen {0} with Device ID {1}", 
                        domainEvent.SeniorCitizenId, deviceId);
                }
            }
            else
            {
                Console.WriteLine("Warning: Failed to create device for Senior Citizen {0}", 
                    domainEvent.SeniorCitizenId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating device for Senior Citizen {0}: {1}", 
                domainEvent.SeniorCitizenId, ex.Message);
            // Don't throw - allow the senior citizen creation to succeed even if device creation fails
        }
    }
}