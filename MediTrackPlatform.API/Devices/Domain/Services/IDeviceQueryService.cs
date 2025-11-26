using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;

namespace MediTrackPlatform.API.Devices.Domain.Services;

public interface IDeviceQueryService
{
    Task<Device?> Handle(GetDeviceByIdQuery query);
    
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query);
    
    Task<IEnumerable<Measurement>> Handle(GetAllBloodPressureMeasurementsByDeviceIdQuery query);
    
    Task<IEnumerable<Measurement>> Handle(GetAllTemperatureMeasurementsByDeviceIdQuery query);
    
    Task<IEnumerable<Measurement>> Handle(GetAllOxygenMeasurementsByDeviceIdQuery query);
    
    Task<IEnumerable<Measurement>> Handle(GetAllHeartRateMeasurementsByDeviceIdQuery query);
}