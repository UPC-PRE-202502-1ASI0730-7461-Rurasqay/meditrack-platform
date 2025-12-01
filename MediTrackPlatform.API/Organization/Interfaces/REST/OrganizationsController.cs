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
[SwaggerTag("Available Organizations Endpoints")]
public class OrganizationsController(
    IOrganizationCommandService organizationCommandService,
    IOrganizationQueryService organizationQueryService
    ) : ControllerBase
{
    [HttpGet("{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get Organization By Id",
        Description = "Get Organization By Id",
        OperationId = "GetOrganizationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Organization Found", typeof(OrganizationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Organization Not Found")]
    public async Task<IActionResult> GetOrganizationById(int organizationId)
    {
        var getOrganizationByIdQuery = new GetOrganizationByIdQuery(organizationId);
        var organization = await organizationQueryService.Handle(getOrganizationByIdQuery);
        if (organization is null) return NotFound();
        var resource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new organization",
        Description = "Create a new organization",
        OperationId = "CreateOrganization")]
    [SwaggerResponse(StatusCodes.Status201Created, "Organization was created", typeof(OrganizationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Organization could not be created")]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationResource resource)
    {
        var createOrganizationCommand = CreateOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var organizationId = await organizationCommandService.Handle(createOrganizationCommand);
        if (organizationId is null) return BadRequest();
        var getOrganizationByIdQuery = new GetOrganizationByIdQuery((int)organizationId);
        var organization = organizationQueryService.Handle(getOrganizationByIdQuery);
        if (organization.Result is null) return NotFound();
        var createdOrganization = organization.Result;
        var organizationResource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(createdOrganization);
        return CreatedAtAction(nameof(GetOrganizationById), new { organizationId = organization.Id }, organizationResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Organizations",
        Description = "Get All Organizations",
        OperationId = "GetAllOrganizations")]
    [SwaggerResponse(StatusCodes.Status200OK, "Organization retrieved successfully", typeof(IEnumerable<OrganizationResource>))]
    public async Task<IActionResult> GetAllOrganizations()
    {
        var organizations = await organizationQueryService.Handle(new GetAllOrganizationsQuery());
        var organizationResources = organizations.Select(OrganizationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(organizationResources);
    }
    
    [HttpPut("{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Update An Organization",
        Description = "Update An Organization",
        OperationId = "UpdateOrganization")]
    [SwaggerResponse(StatusCodes.Status200OK, "Organization updated successfully", typeof(OrganizationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Organization could not be updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Organization not found")]
    public async Task<IActionResult> UpdateOrganization([FromBody] UpdateOrganizationResource resource, int organizationId)
    {
        var updateOrganizationCommand = UpdateOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource, organizationId);
        var updatedOrganization = await organizationCommandService.Handle(updateOrganizationCommand);
        if (updatedOrganization is null) return NotFound();
        var organizationResource = OrganizationResourceFromEntityAssembler.ToResourceFromEntity(updatedOrganization);
        return Ok(organizationResource);
    }

    [HttpDelete("{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Delete An Organization",
        Description = "Delete An Organization",
        OperationId = "DeleteOrganization")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Organization deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Organization not found")]
    public async Task<IActionResult> DeleteOrganization(int organizationId)
    {
        var deleteOrganizationCommand = new DeleteOrganizationCommand(organizationId);
        try
        {
            await organizationCommandService.Handle(deleteOrganizationCommand);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}