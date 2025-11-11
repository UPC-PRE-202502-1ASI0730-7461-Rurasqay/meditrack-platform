using MediTrackPlatform.API.Devices.Domain.Model.Queries; 
using MediTrackPlatform.API.Devices.Domain.Services;     
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;


namespace MediTrackPlatform.API.Devices.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertQueryService _alertQueryService;

        public AlertsController(IAlertQueryService alertQueryService)
        {
            _alertQueryService = alertQueryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlertResource>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllAlerts([FromQuery] int seniorCitizenId)
        {
            var query = new GetAllAlertsQuery(seniorCitizenId);
            
            var alerts = await _alertQueryService.Handle(query);
            
            var resources = alerts.Select(alert => 
                new AlertResource(
                    alert.AlertId,
                    alert.DeviceId,
                    alert.EAlertType.ToString(),
                    alert.Message,
                    alert.DataRegistered,
                    alert.RegisteredAt
                ));
            
            return Ok(resources);
        }


        [HttpGet("{alertId}")]
        [ProducesResponseType(typeof(AlertResource), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAlertById(int alertId)
        {
            var query = new GetAlertByIdQuery(alertId);
            
            var alert = await _alertQueryService.Handle(query);

            if (alert == null)
            {
                return NotFound();
            }

            var resource = new AlertResource(
                alert.AlertId,
                alert.DeviceId,
                alert.EAlertType.ToString(),
                alert.Message,
                alert.DataRegistered,
                alert.RegisteredAt
            );

            return Ok(resource);
        }
    }
}