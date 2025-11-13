using System.Net.Mime;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Services;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;
using MediTrackPlatform.API.Organization.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediTrackPlatform.API.Organization.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Caregivers Endpoints")]
public class CaregiversController(
    ICaregiverCommandService caregiverCommandService,
    ICaregiverQueryService caregiverQueryService
    ) : ControllerBase
{
    [HttpGet("/{caregiverId:int}")]
    [SwaggerOperation(
        Summary = "Get Caregiver By Id",
        Description = "Get Caregiver By Id",
        OperationId = "GetCaregiverById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Caregiver found", typeof(CaregiverResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caregiver not found")]
    public async Task<IActionResult> GetCaregiverById(int caregiverId)
    {
        var getCaregiverByIdQuery = new GetCaregiverByIdQuery(caregiverId);
        var caregiver = await caregiverQueryService.Handle(getCaregiverByIdQuery);
        if (caregiver is null) return NotFound();
        var resource = CaregiverResourceFromEntityAssembler.ToResourceFromEntity(caregiver);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new caregiver",
        Description = "Create a new caregiver",
        OperationId = "CreateCaregiver")]
    [SwaggerResponse(StatusCodes.Status201Created, "Caregiver was created", typeof(CaregiverResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Caregiver could not be created")]
    public async Task<IActionResult> CreateCaregiver([FromBody] CreateCaregiverResource resource)
    {
        var createCaregiverCommand = CreateCaregiverCommandFromResourceAssembler.ToCommandFromResource(resource);
        var caregiverId = await caregiverCommandService.Handle(createCaregiverCommand);
        if (caregiverId is null) return BadRequest();
        var getCaregiverByIdQuery = new GetCaregiverByIdQuery((int)caregiverId);
        var caregiver = caregiverQueryService.Handle(getCaregiverByIdQuery);
        if (caregiver.Result is null) return NotFound();
        var createdCaregiver = caregiver.Result;
        var caregiverResource = CaregiverResourceFromEntityAssembler.ToResourceFromEntity(createdCaregiver);
        return CreatedAtAction(nameof(GetCaregiverById), new { caregiverId = caregiver.Id }, caregiverResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Caregivers",
        Description = "Get All Caregivers",
        OperationId = "GetAllCaregivers")]
    [SwaggerResponse(StatusCodes.Status200OK, "Caregivers retrieved successfully", typeof(IEnumerable<CaregiverResource>))]
    public async Task<IActionResult> GetAllCaregivers()
    {
        var caregivers = await caregiverQueryService.Handle(new GetAllCaregiversQuery());
        var caregiverResources = caregivers.Select(CaregiverResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(caregiverResources);
    }
    
    [HttpGet("/organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get All Caregivers By Organization Id",
        Description = "Get All Caregivers By Organization Id",
        OperationId = "GetAllCaregiversByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Caregivers retrieved successfully", typeof(IEnumerable<CaregiverResource>))]
    public async Task<IActionResult> GetAllCaregiversByOrganizationId(int organizationId)
    {
        var caregivers = await caregiverQueryService.Handle(new GetAllCaregiversByOrganizationIdQuery(organizationId));
        var caregiverResources = caregivers.Select(CaregiverResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(caregiverResources);
    }
    
    [HttpGet("/user/{userId:int}/organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get Caregiver By User Id And Organization Id",
        Description = "Get Caregiver By User Id And Organization Id",
        OperationId = "GetCaregiverByUserIdAndOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Caregiver found", typeof(CaregiverResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caregiver not found")]
    public async Task<IActionResult> GetCaregiverByUserIdAndOrganizationId(int userId, int organizationId)
    {
        var getCaregiverByUserIdAndOrganizationIdQuery = new GetCaregiverByUserIdAndOrganizationIdQuery(userId, organizationId);
        var caregiver = await caregiverQueryService.Handle(getCaregiverByUserIdAndOrganizationIdQuery);
        if (caregiver is null) return NotFound();
        var resource = CaregiverResourceFromEntityAssembler.ToResourceFromEntity(caregiver);
        return Ok(resource);
    }
    
    [HttpPut("/{caregiverId:int}")]
    [SwaggerOperation(
        Summary = "Update A Caregiver",
        Description = "Update A Caregiver",
        OperationId = "UpdateCaregiver")]
    [SwaggerResponse(StatusCodes.Status200OK, "Caregiver updated successfully", typeof(CaregiverResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Caregiver could not be updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caregiver not found")]
    public async Task<IActionResult> UpdateCaregiver([FromBody] UpdateCaregiverResource resource, int caregiverId)
    {
        var updateCaregiverCommand = UpdateCaregiverCommandFromResourceAssembler.ToCommandFromResource(resource, caregiverId);
        var updatedCaregiver = await caregiverCommandService.Handle(updateCaregiverCommand);
        if (updatedCaregiver is null) return NotFound();
        var caregiverResource = CaregiverResourceFromEntityAssembler.ToResourceFromEntity(updatedCaregiver);
        return Ok(caregiverResource);
    }

    [HttpDelete("/{caregiverId:int}")]
    [SwaggerOperation(
        Summary = "Delete A Caregiver",
        Description = "Delete A Caregiver",
        OperationId = "DeleteCaregiver")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Caregiver deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caregiver not found")]
    public async Task<IActionResult> DeleteCaregiver(int caregiverId)
    {
        var deleteCaregiverCommand = new DeleteCaregiverCommand(caregiverId);
        try
        {
            await caregiverCommandService.Handle(deleteCaregiverCommand);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}