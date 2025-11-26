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
[SwaggerTag("Available Admin Endpoints")]
public class SeniorCitizensController(
    ISeniorCitizenCommandService seniorCitizenCommandService,
    ISeniorCitizenQueryService seniorCitizenQueryService
    ) : ControllerBase
{
    [HttpGet("{seniorCitizenId:int}")]
    [SwaggerOperation(
        Summary = "Get Senior Citizen By Id",
        Description = "Get Senior Citizen By Id",
        OperationId = "GetSeniorCitizenById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Senior Citizen Found", typeof(SeniorCitizenResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen Not Found")]
    public async Task<IActionResult> GetSeniorCitizenById(int seniorCitizenId)
    {
        var getSeniorCitizenByIdQuery = new GetSeniorCitizenByIdQuery(seniorCitizenId);
        var seniorCitizen = await seniorCitizenQueryService.Handle(getSeniorCitizenByIdQuery);
        if (seniorCitizen is null) return NotFound();
        var resource = SeniorCitizenResourceFromEntityAssembler.ToResourceFromEntity(seniorCitizen);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new senior citizen",
        Description = "Create a new senior citizen",
        OperationId = "CreateSeniorCitizen")]
    [SwaggerResponse(StatusCodes.Status201Created, "Senior Citizen was created", typeof(SeniorCitizenResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Senior Citizen could not be created")]
    public async Task<IActionResult> CreateSeniorCitizen([FromBody] CreateSeniorCitizenResource resource)
    {
        var createSeniorCitizenCommand = CreateSeniorCitizenCommandFromResourceAssembler.ToCommandFromResource(resource);
        var seniorCitizenId = await seniorCitizenCommandService.Handle(createSeniorCitizenCommand);
        if (seniorCitizenId is null) return BadRequest();
        var getSeniorCitizenByIdQuery = new GetSeniorCitizenByIdQuery((int)seniorCitizenId);
        var seniorCitizen = seniorCitizenQueryService.Handle(getSeniorCitizenByIdQuery);
        if (seniorCitizen.Result is null) return NotFound();
        var createdSeniorCitizen = seniorCitizen.Result;
        var seniorCitizenResource = SeniorCitizenResourceFromEntityAssembler.ToResourceFromEntity(createdSeniorCitizen);
        return CreatedAtAction(nameof(GetSeniorCitizenById), new { seniorCitizenId = seniorCitizen.Id }, seniorCitizenResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Senior Citizen",
        Description = "Get All Senior Citizen",
        OperationId = "GetAllSeniorCitizen")]
    [SwaggerResponse(StatusCodes.Status200OK, "Senior Citizen retrieved successfully", typeof(IEnumerable<SeniorCitizenResource>))]
    public async Task<IActionResult> GetAllSeniorCitizens()
    {
        var seniorCitizens = await seniorCitizenQueryService.Handle(new GetAllSeniorCitizensQuery());
        var seniorCitizenResources = seniorCitizens.Select(SeniorCitizenResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(seniorCitizenResources);
    }

    [HttpGet("organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get All Senior Citizen By Organization Id",
        Description = "Get All Senior Citizen By Organization Id",
        OperationId = "GetAllSeniorCitizenByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Senior Citizen retrieved successfully", typeof(IEnumerable<SeniorCitizenResource>))]
    public async Task<IActionResult> GetAllSeniorCitizensByOrganizationId(int organizationId)
    {
        var seniorCitizens = await seniorCitizenQueryService.Handle(new GetAllSeniorCitizensByOrganizationIdQuery(organizationId));
        var seniorCitizensResources = seniorCitizens.Select(SeniorCitizenResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(seniorCitizensResources);
    }
    
    [HttpPut("{seniorCitizenId:int}")]
    [SwaggerOperation(
        Summary = "Update A Senior Citizen",
        Description = "Update A Senior Citizen",
        OperationId = "UpdateSeniorCitizen")]
    [SwaggerResponse(StatusCodes.Status200OK, "Senior Citizen updated successfully", typeof(AdminResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Senior Citizen could not be updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen not found")]
    public async Task<IActionResult> UpdateSeniorCitizen([FromBody] UpdateSeniorCitizenResource resource, int seniorCitizenId)
    {
        var updateSeniorCitizenCommand = UpdateSeniorCitizenCommandFromResourceAssembler.ToCommandFromResource(resource, seniorCitizenId);
        var updatedSeniorCitizen = await seniorCitizenCommandService.Handle(updateSeniorCitizenCommand);
        if (updatedSeniorCitizen is null) return NotFound();
        var seniorCitizenResource = SeniorCitizenResourceFromEntityAssembler.ToResourceFromEntity(updatedSeniorCitizen);
        return Ok(seniorCitizenResource);
    }

    [HttpDelete("{seniorCitizenId:int}")]
    [SwaggerOperation(
        Summary = "Delete A Senior Citizen",
        Description = "Delete A Senior Citizen",
        OperationId = "DeleteSeniorCitizen")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Senior Citizen deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Senior Citizen not found")]
    public async Task<IActionResult> DeleteSeniorCitizen(int seniorCitizenId)
    {
        var deleteSeniorCitizenCommand = new DeleteSeniorCitizenCommand(seniorCitizenId);
        try
        {
            await seniorCitizenCommandService.Handle(deleteSeniorCitizenCommand);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}