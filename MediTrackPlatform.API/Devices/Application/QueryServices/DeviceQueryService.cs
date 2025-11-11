using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Devices.Domain.Services;

namespace MediTrackPlatform.API.Devices.Application.QueryServices;

public class DeviceQueryService(IDeviceRepository deviceRepository) : IDeviceQueryService
{
    public async Task<Device?> Handle(GetDeviceByIdQuery query)
    {
        return await deviceRepository.FindByIdAsync(query.DeviceId);
    }

    public async Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query)
    {
        return await deviceRepository.ListAsync();
    }

    public async Task<IEnumerable<Measurement>> Handle(GetAllBloodPressureMeasurementsByDeviceIdQuery query)
    {
        var device = await deviceRepository.FindByIdAsync(query.DeviceId);
        return device?.GetBloodPressureMeasurements() ?? new List<Measurement>();
    }

    public async Task<IEnumerable<Measurement>> Handle(GetAllTemperatureMeasurementsByDeviceIdQuery query)
    {
        var device = await deviceRepository.FindByIdAsync(query.DeviceId);
        return device?.GetTemperatureMeasurements() ?? new List<Measurement>();
    }

    public async Task<IEnumerable<Measurement>> Handle(GetAllOxygenMeasurementsByDeviceIdQuery query)
    {
        var device = await deviceRepository.FindByIdAsync(query.DeviceId);
        return device?.GetOxygenMeasurements() ?? new List<Measurement>();
    }

    public async Task<IEnumerable<Measurement>> Handle(GetAllHeartRateMeasurementsByDeviceIdQuery query)
    {
        var device = await deviceRepository.FindByIdAsync(query.DeviceId);
        return device?.GetHeartRateMeasurements() ?? new List<Measurement>();
    }
}