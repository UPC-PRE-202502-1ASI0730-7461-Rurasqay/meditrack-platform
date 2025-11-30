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
        
        // Generate demo measurements after device is saved and has an ID
        await GenerateDemoMeasurements(device);
        await unitOfWork.CompleteAsync();
        
        return device;
    }
    
    /// <summary>
    /// Generate 7 demo measurements for each measurement type
    /// Some values will be inside safe ranges, some slightly outside to demonstrate alerts
    /// </summary>
    /// <param name="device">The device to add measurements to</param>
    private async Task GenerateDemoMeasurements(Device device)
    {
        var random = new Random();
        
        // Temperature: Safe range 35.0-38.0°C
        // Generate 5 safe values and 2 values outside range
        var temperatures = new[]
        {
            36.0 + random.NextDouble() * 1.5,  // Safe: 36.0-37.5
            36.2 + random.NextDouble() * 1.3,  // Safe: 36.2-37.5
            36.5 + random.NextDouble() * 1.0,  // Safe: 36.5-37.5
            35.8 + random.NextDouble() * 1.8,  // Safe: 35.8-37.6
            36.3 + random.NextDouble() * 1.2,  // Safe: 36.3-37.5
            38.2 + random.NextDouble() * 0.8,  // Unsafe: 38.2-39.0 (high)
            34.2 + random.NextDouble() * 0.6   // Unsafe: 34.2-34.8 (low)
        };
        
        foreach (var temp in temperatures)
        {
            var measurement = new TemperatureMeasurement(temp);
            if (measurement.SurpassesThreshold())
            {
                await domainEventPublisher.PublishAsync(
                    new AlertCreatedEvent(device.DeviceId, temp, "TEMPERATURE"));
            }
            device.AddTemperature(temp);
        }
        
        // Heart Rate: Safe range 50-120 bpm
        // Generate 5 safe values and 2 values outside range
        var heartRates = new[]
        {
            60 + random.Next(40),   // Safe: 60-100
            65 + random.Next(35),   // Safe: 65-100
            70 + random.Next(30),   // Safe: 70-100
            75 + random.Next(25),   // Safe: 75-100
            55 + random.Next(45),   // Safe: 55-100
            125 + random.Next(20),  // Unsafe: 125-145 (high)
            40 + random.Next(8)     // Unsafe: 40-48 (low)
        };
        
        foreach (var hr in heartRates)
        {
            var measurement = new HeartRateMeasurement(hr);
            if (measurement.SurpassesThreshold())
            {
                await domainEventPublisher.PublishAsync(
                    new AlertCreatedEvent(device.DeviceId, hr, "HEART_RATE"));
            }
            device.AddHeartRate(hr);
        }
        
        // Oxygen: Safe minimum 90%
        // Generate 5 safe values and 2 values below threshold
        var oxygenLevels = new[]
        {
            95 + random.Next(5),    // Safe: 95-100
            93 + random.Next(6),    // Safe: 93-99
            94 + random.Next(5),    // Safe: 94-99
            96 + random.Next(4),    // Safe: 96-100
            92 + random.Next(7),    // Safe: 92-99
            85 + random.Next(4),    // Unsafe: 85-89
            82 + random.Next(5)     // Unsafe: 82-87
        };
        
        foreach (var spo2 in oxygenLevels)
        {
            var measurement = new OxygenMeasurement(spo2);
            if (measurement.SurpassesThreshold())
            {
                await domainEventPublisher.PublishAsync(
                    new AlertCreatedEvent(device.DeviceId, spo2, "OXYGEN"));
            }
            device.AddOxygen(spo2);
        }
        
        // Blood Pressure: Safe range 90-180 for both diastolic and systolic
        // Generate 5 safe pairs and 2 pairs with at least one value outside range
        var bloodPressures = new[]
        {
            new { Diastolic = 70 + random.Next(15), Systolic = 110 + random.Next(25) },  // Safe: 70-85/110-135
            new { Diastolic = 72 + random.Next(13), Systolic = 115 + random.Next(20) },  // Safe: 72-85/115-135
            new { Diastolic = 75 + random.Next(10), Systolic = 120 + random.Next(15) },  // Safe: 75-85/120-135
            new { Diastolic = 68 + random.Next(17), Systolic = 112 + random.Next(23) },  // Safe: 68-85/112-135
            new { Diastolic = 74 + random.Next(11), Systolic = 118 + random.Next(17) },  // Safe: 74-85/118-135
            new { Diastolic = 85 + random.Next(10), Systolic = 185 + random.Next(15) },  // Unsafe: systolic high
            new { Diastolic = 55 + random.Next(10), Systolic = 95 + random.Next(20) }    // Unsafe: diastolic low
        };
        
        foreach (var bp in bloodPressures)
        {
            var measurement = new BloodPressureMeasurement(bp.Diastolic, bp.Systolic);
            if (measurement.DiastolicSurpassesThreshold())
            {
                await domainEventPublisher.PublishAsync(
                    new AlertCreatedEvent(device.DeviceId, bp.Diastolic, "BLOOD_PRESSURE"));
            }
            if (measurement.SystolicSurpassesThreshold())
            {
                await domainEventPublisher.PublishAsync(
                    new AlertCreatedEvent(device.DeviceId, bp.Systolic, "BLOOD_PRESSURE"));
            }
            device.AddBloodPressure(bp.Diastolic, bp.Systolic);
        }
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