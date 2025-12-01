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
[SwaggerTag("Available Caregiver Assignments Endpoints")]
public class CaregiverAssignmentsController(
    ISeniorCitizenCommandService seniorCitizenCommandService,
    ISeniorCitizenQueryService seniorCitizenQueryService
    ) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Assign a senior citizen to a caregiver",
        Description = "Assign a senior citizen to a caregiver",
        OperationId = "AssignSeniorCitizenToCaregiver")]
    [SwaggerResponse(StatusCodes.Status200OK, "Senior Citizen assigned to caregiver successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Senior Citizen could not be assigned to caregiver")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen or caregiver not found")]
    public async Task<IActionResult> AssignSeniorCitizenToCaregiver([FromBody] AssignSeniorCitizenToCaregiverResource resource)
    {
        try
        {
            var assignCommand = AssignSeniorCitizenToCaregiverCommandFromResourceAssembler.ToCommandFromResource(resource);
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

    [HttpDelete("caregivers/{caregiverId:int}/senior-citizens/{seniorCitizenId:int}")]
    [SwaggerOperation(
        Summary = "Unassign a senior citizen from a caregiver",
        Description = "Unassign a senior citizen from a caregiver",
        OperationId = "UnassignSeniorCitizenFromCaregiver")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Senior Citizen unassigned from caregiver successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Senior Citizen could not be unassigned from caregiver")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen or caregiver not found")]
    public async Task<IActionResult> UnassignSeniorCitizenFromCaregiver(int caregiverId, int seniorCitizenId)
    {
        try
        {
            var unassignCommand = new UnassignSeniorCitizenFromCaregiverCommand(seniorCitizenId, caregiverId);
            _ = await seniorCitizenCommandService.Handle(unassignCommand);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}