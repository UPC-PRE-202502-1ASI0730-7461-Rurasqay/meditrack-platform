using Cortex.Mediator;
using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Domain.Model.Events;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Devices.Application.CommandServices;

public class DeviceCommandService(
    IDeviceRepository deviceRepository, 
    IUnitOfWork unitOfWork,
    IMediator domainEventPublisher) 
    : IDeviceCommandService
{
    public async Task<Device?> Handle(CreateDeviceCommand command)
    {
        var device = new Device(command);
        await deviceRepository.AddAsync(device);
        await unitOfWork.CompleteAsync();
        return device;
    }
    
    public async Task<Device?> Handle(AddBloodPressureMeasurementToDeviceCommand command)
    {
        var device = await deviceRepository.FindByIdAsync(command.DeviceId);
        if (device is null) throw new Exception("Device not found");
        if (device.ExistsMoreThanWeeklyMeasurementsOfType<HeartRateMeasurement>()) 
            device.RemoveLastMeasurementOfType<HeartRateMeasurement>();
        var measurement = new BloodPressureMeasurement(command.Diastolic, command.Systolic);
        if (measurement.DiastolicSurpassesThreshold()) 
            await domainEventPublisher.PublishAsync(
                new AlertCreatedEvent(device.DeviceId, command.Diastolic, measurement.Type.ToString()));
        if (measurement.SystolicSurpassesThreshold()) 
            await domainEventPublisher.PublishAsync(
                new AlertCreatedEvent(device.DeviceId, command.Systolic, measurement.Type.ToString()));
        device.AddBloodPressure(command.Diastolic, command.Systolic);
        await unitOfWork.CompleteAsync();
        return device;
    }
    
    public async Task<Device?> Handle(AddHeartRateMeasurementToDeviceCommand command)
    {
        var device = await deviceRepository.FindByIdAsync(command.DeviceId);
        if (device is null) throw new Exception("Device not found");
        if (device.ExistsMoreThanWeeklyMeasurementsOfType<HeartRateMeasurement>()) 
            device.RemoveLastMeasurementOfType<HeartRateMeasurement>();
        var measurement = new HeartRateMeasurement(command.Bpm);
        if (measurement.SurpassesThreshold()) 
            await domainEventPublisher.PublishAsync(
                new AlertCreatedEvent(device.DeviceId, command.Bpm, measurement.Type.ToString()));
        device.AddHeartRate(command.Bpm);
        await unitOfWork.CompleteAsync();
        return device;
    }
    
    public async Task<Device?> Handle(AddOxygenMeasurementToDeviceCommand command)
    {
        var device = await deviceRepository.FindByIdAsync(command.DeviceId);
        if (device is null) throw new Exception("Device not found");
        if (device.ExistsMoreThanWeeklyMeasurementsOfType<OxygenMeasurement>()) 
            device.RemoveLastMeasurementOfType<OxygenMeasurement>();
        var measurement = new OxygenMeasurement(command.Spo2);
        if (measurement.SurpassesThreshold()) 
            await domainEventPublisher.PublishAsync(
                new AlertCreatedEvent(device.DeviceId, command.Spo2, measurement.Type.ToString()));
        device.AddOxygen(command.Spo2);
        await unitOfWork.CompleteAsync();
        return device;
    }
    
    public async Task<Device?> Handle(AddTemperatureMeasurementToDeviceCommand command)
    {
        var device = await deviceRepository.FindByIdAsync(command.DeviceId);
        if (device is null) throw new Exception("Device not found");
        if (device.ExistsMoreThanWeeklyMeasurementsOfType<TemperatureMeasurement>()) 
            device.RemoveLastMeasurementOfType<TemperatureMeasurement>();
        var measurement = new TemperatureMeasurement(command.Celsius);
        if (measurement.SurpassesThreshold()) 
            await domainEventPublisher.PublishAsync(
                new AlertCreatedEvent(device.DeviceId, command.Celsius, measurement.Type.ToString()));
        device.AddTemperature(command.Celsius);
        await unitOfWork.CompleteAsync();
        return device;
    }
    
}