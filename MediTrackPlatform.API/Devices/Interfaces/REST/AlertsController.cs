using System.Net.Mime;
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
[SwaggerTag("Available Alerts endpoints")]
public class AlertsController(IAlertQueryService alertQueryService): ControllerBase
{
    [HttpGet("{alertId:int}")]
    [SwaggerOperation(
        Summary = "Get a alert by ID",
        Description = "Get a alert by ID",
        OperationId = "GetAlertById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The alert was found", typeof(AlertResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The alert was not found")]
    public async Task<IActionResult> GetAlertById(int alertId)
    {
        var query = new GetAlertByIdQuery(alertId);
        var alert = await alertQueryService.Handle(query);

        if (alert is null) return NotFound();

        var resource = AlertResourceFromEntityAssembler.ToResourceFromEntity(alert);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all alerts",
        Description = "Get all alerts",
        OperationId = "GetAllAlerts")]
    [SwaggerResponse(StatusCodes.Status200OK, "The alerts were found", typeof(IEnumerable<AlertResource>))]
    public async Task<IActionResult> GetAllAlerts()
    {
        var getAllAlertsQuery = new GetAllAlertsQuery();
        var alerts = await alertQueryService.Handle(getAllAlertsQuery);
        var alertResources = alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(alertResources);
    }
}
