using System.Net.Mime;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;
using MediTrackPlatform.API.Organization.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediTrackPlatform.API.Organization.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Doctor Assignments Endpoints")]
public class DoctorAssignmentsController(
    ISeniorCitizenCommandService seniorCitizenCommandService,
    ISeniorCitizenQueryService seniorCitizenQueryService
    ) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Assign a senior citizen to a doctor",
        Description = "Assign a senior citizen to a doctor",
        OperationId = "AssignSeniorCitizenToDoctor")]
    [SwaggerResponse(StatusCodes.Status200OK, "Senior Citizen assigned to doctor successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Senior Citizen could not be assigned to doctor")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen or doctor not found")]
    public async Task<IActionResult> AssignSeniorCitizenToDoctor([FromBody] AssignSeniorCitizenToDoctorResource resource)
    {
        try
        {
            var assignCommand = AssignSeniorCitizenToDoctorCommandFromResourceAssembler.ToCommandFromResource(resource);
            var seniorCitizen = await seniorCitizenCommandService.Handle(assignCommand);
            if (seniorCitizen is null) return NotFound();
            var seniorCitizenResource = SeniorCitizenResourceFromEntityAssembler.ToResourceFromEntity(seniorCitizen);
            return Ok(seniorCitizenResource);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("/doctors/{doctorId:int}/senior-citizens/{seniorCitizenId:int}")]
    [SwaggerOperation(
        Summary = "Unassign a senior citizen from a doctor",
        Description = "Unassign a senior citizen from a doctor",
        OperationId = "UnassignSeniorCitizenFromDoctor")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Senior Citizen unassigned from doctor successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Senior Citizen could not be unassigned from doctor")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen or doctor not found")]
    public async Task<IActionResult> UnassignSeniorCitizenFromDoctor(int doctorId, int seniorCitizenId)
    {
        try
        {
            var unassignCommand = new UnassignSeniorCitizenFromDoctorCommand(seniorCitizenId, doctorId);
            _ = await seniorCitizenCommandService.Handle(unassignCommand);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}