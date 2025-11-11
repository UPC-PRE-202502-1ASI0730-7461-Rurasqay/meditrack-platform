using MediTrackPlatform.API.Devices.Domain.Model.Queries;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;
using MediTrackPlatform.API.Devices.Interfaces.REST.Transform; 
using Microsoft.AspNetCore.Mvc;

namespace MediTrackPlatform.API.Devices.Interfaces.REST;

[ApiController]
[Route("api/v1/devices")]
public class DevicesController(
    IDeviceCommandService deviceCommandService,
    IDeviceQueryService deviceQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceResource resource)
    {
        var command = CreateDeviceCommandFromResource.ToCommandFromResource(resource);
        var device = await deviceCommandService.Handle(command);
        
        if (device is null) return BadRequest();
        
        var deviceResource = DeviceResourceFromEntityResource.ToResourceFromEntity(device);
        return CreatedAtAction(nameof(GetDeviceById), new { deviceId = deviceResource.DeviceId }, deviceResource);
    }

    [HttpGet("{deviceId:int}")]
    public async Task<IActionResult> GetDeviceById(int deviceId)
    {
        var query = new GetDeviceByIdQuery(deviceId);
        var device = await deviceQueryService.Handle(query);

        if (device is null) return NotFound();

        var resource = DeviceResourceFromEntityResource.ToResourceFromEntity(device);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetDevicesByHolderId([FromQuery] int holderId)
    {
        var query = new GetAllDevicesQuery(holderId);
        var devices = await deviceQueryService.Handle(query);
        
        var resources = devices.Select(DeviceResourceFromEntityResource.ToResourceFromEntity);
        return Ok(resources);
    }
}