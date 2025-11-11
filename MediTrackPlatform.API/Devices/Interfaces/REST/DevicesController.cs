using System.Net.Mime;
using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;
using MediTrackPlatform.API.Devices.Interfaces.REST.Transform; 
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediTrackPlatform.API.Devices.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Devices endpoints")]
public class DevicesController(
    IDeviceCommandService deviceCommandService,
    IDeviceQueryService deviceQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new device",
        Description = "Create a new device",
        OperationId = "CreateDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The device was created", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The device was not created")]
    public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceResource resource)
    {
        var command = CreateDeviceCommandFromResource.ToCommandFromResource(resource);
        var device = await deviceCommandService.Handle(command);
        
        if (device is null) return BadRequest();
        
        var deviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(device);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = deviceResource.DeviceId }, deviceResource);
    }

    [HttpGet("{deviceId:int}")]
    [SwaggerOperation(
        Summary = "Get a device by ID",
        Description = "Get a device by ID",
        OperationId = "GetDeviceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The device was found", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The device was not found")]
    public async Task<IActionResult> GetDeviceById(int deviceId)
    {
        var query = new GetDeviceByIdQuery(deviceId);
        var device = await deviceQueryService.Handle(query);

        if (device is null) return NotFound();

        var resource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(device);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all devices",
        Description = "Get all devices",
        OperationId = "GetAllDevices")]
    [SwaggerResponse(StatusCodes.Status200OK, "The devices were found", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetAllDevices()
    {
        var getAllDevicesQuery = new GetAllDevicesQuery();
        var devices = await deviceQueryService.Handle(getAllDevicesQuery);
        var deviceResources = devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(deviceResources);
    }

    [HttpPost("{deviceId:int}/measurements/blood-pressure")]
    [SwaggerOperation(
        Summary = "Add a Blood Pressure Measurement to a device",
        Description = "Add a Blood Pressure Measurement to a device",
        OperationId = "AddBloodPressureMeasurementToDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Blood Pressure Measurement was added to the device", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Blood Pressure Measurement was not added to the device")]
    public async Task<IActionResult> AddBloodPressureMeasurementToDevice(
        [FromBody] AddBloodPressureMeasurementToDeviceResource resource,
        [FromRoute] int deviceId)
    {
        var command = AddBloodPressureMeasurementToDeviceCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var device = await deviceCommandService.Handle(command);
        if (device is null) return BadRequest();
        var deviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(device);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = device.DeviceId }, deviceResource);
    }

    [HttpPost("{deviceId:int}/measurements/temperature")]
    [SwaggerOperation(
        Summary = "Add a Temperature Measurement to a device",
        Description = "Add a Temperature Measurement to a device",
        OperationId = "AddTemperatureMeasurementToDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Temperature Measurement was added to the device", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Temperature Measurement was not added to the device")]
    public async Task<IActionResult> AddTemperatureMeasurementToDevice(
        [FromBody] AddTemperatureMeasurementToDeviceResource resource,
        [FromRoute] int deviceId)
    {
        var command = AddTemperatureMeasurementToDeviceCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var device = await deviceCommandService.Handle(command);
        if (device is null) return BadRequest();
        var deviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(device);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = device.DeviceId }, deviceResource);
    }

    [HttpPost("{deviceId:int}/measurements/oxygen")]
    [SwaggerOperation(
        Summary = "Add an Oxygen Measurement to a device",
        Description = "Add an Oxygen Measurement to a device",
        OperationId = "AddOxygenMeasurementToDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Oxygen Measurement was added to the device", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Oxygen Measurement was not added to the device")]
    public async Task<IActionResult> AddOxygenMeasurementToDevice(
        [FromBody] AddOxygenMeasurementToDeviceResource resource,
        [FromRoute] int deviceId)
    {
        var command = AddOxygenMeasurementToDeviceCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var device = await deviceCommandService.Handle(command);
        if (device is null) return BadRequest();
        var deviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(device);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = device.DeviceId }, deviceResource);
    }

    [HttpPost("{deviceId:int}/measurements/heart-rate")]
    [SwaggerOperation(
        Summary = "Add a Heart Rate Measurement to a device",
        Description = "Add a Heart Rate Measurement to a device",
        OperationId = "AddHeartRateMeasurementToDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Heart Rate Measurement was added to the device", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Heart Rate Measurement was not added to the device")]
    public async Task<IActionResult> AddHeartRateMeasurementToDevice(
        [FromBody] AddHeartRateMeasurementToDeviceResource resource,
        [FromRoute] int deviceId)
    {
        var command = AddHeartRateMeasurementToDeviceCommandFromResourceAssembler.ToCommandFromResource(resource, deviceId);
        var device = await deviceCommandService.Handle(command);
        if (device is null) return BadRequest();
        var deviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(device);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = device.DeviceId }, deviceResource);
    }

    [HttpGet("{deviceId:int}/measurements/blood-pressure")]
    [SwaggerOperation(
        Summary = "Get all Blood Pressure Measurements by Device ID",
        Description = "Get all Blood Pressure Measurements for a specific device",
        OperationId = "GetAllBloodPressureMeasurementsByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Blood Pressure Measurements were found", typeof(IEnumerable<BloodPressureMeasurementResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The device was not found")]
    public async Task<IActionResult> GetAllBloodPressureMeasurementsByDeviceId([FromRoute] int deviceId)
    {
        var query = new GetAllBloodPressureMeasurementsByDeviceIdQuery(deviceId);
        var measurements = await deviceQueryService.Handle(query);
        
        var resources = measurements
            .Cast<BloodPressureMeasurement>()
            .Select(BloodPressureMeasurementResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }

    [HttpGet("{deviceId:int}/measurements/temperature")]
    [SwaggerOperation(
        Summary = "Get all Temperature Measurements by Device ID",
        Description = "Get all Temperature Measurements for a specific device",
        OperationId = "GetAllTemperatureMeasurementsByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Temperature Measurements were found", typeof(IEnumerable<TemperatureMeasurementResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The device was not found")]
    public async Task<IActionResult> GetAllTemperatureMeasurementsByDeviceId([FromRoute] int deviceId)
    {
        var query = new GetAllTemperatureMeasurementsByDeviceIdQuery(deviceId);
        var measurements = await deviceQueryService.Handle(query);
        
        var resources = measurements
            .Cast<TemperatureMeasurement>()
            .Select(TemperatureMeasurementResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }

    [HttpGet("{deviceId:int}/measurements/oxygen")]
    [SwaggerOperation(
        Summary = "Get all Oxygen Measurements by Device ID",
        Description = "Get all Oxygen Measurements for a specific device",
        OperationId = "GetAllOxygenMeasurementsByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Oxygen Measurements were found", typeof(IEnumerable<OxygenMeasurementResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The device was not found")]
    public async Task<IActionResult> GetAllOxygenMeasurementsByDeviceId([FromRoute] int deviceId)
    {
        var query = new GetAllOxygenMeasurementsByDeviceIdQuery(deviceId);
        var measurements = await deviceQueryService.Handle(query);
        
        var resources = measurements
            .Cast<OxygenMeasurement>()
            .Select(OxygenMeasurementResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }

    [HttpGet("{deviceId:int}/measurements/heart-rate")]
    [SwaggerOperation(
        Summary = "Get all Heart Rate Measurements by Device ID",
        Description = "Get all Heart Rate Measurements for a specific device",
        OperationId = "GetAllHeartRateMeasurementsByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Heart Rate Measurements were found", typeof(IEnumerable<HeartRateMeasurementResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The device was not found")]
    public async Task<IActionResult> GetAllHeartRateMeasurementsByDeviceId([FromRoute] int deviceId)
    {
        var query = new GetAllHeartRateMeasurementsByDeviceIdQuery(deviceId);
        var measurements = await deviceQueryService.Handle(query);
        
        var resources = measurements
            .Cast<HeartRateMeasurement>()
            .Select(HeartRateMeasurementResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }
}