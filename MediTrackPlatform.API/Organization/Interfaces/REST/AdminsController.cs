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
public class AdminsController(
    IAdminCommandService adminCommandService,
    IAdminQueryService adminQueryService
    ) : ControllerBase
{
    [HttpGet("{adminId:int}")]
    [SwaggerOperation(
        Summary = "Get Admin By Id",
        Description = "Get Admin By Id",
        OperationId = "GetAdminById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Admin Found", typeof(AdminResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Admin Not Found")]
    public async Task<IActionResult> GetAdminById(int adminId)
    {
        var getAdminByIdQuery = new GetAdminByIdQuery(adminId);
        var admin = await adminQueryService.Handle(getAdminByIdQuery);
        if (admin is null) return NotFound();
        var resource = AdminResourceFromEntityAssembler.ToResourceFromEntity(admin);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new admin",
        Description = "Create a new admin",
        OperationId = "CreateAdmin")]
    [SwaggerResponse(StatusCodes.Status201Created, "Admin was created", typeof(AdminResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Admin could not be created")]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminResource resource)
    {
        var createAdminCommand = CreateAdminCommandFromResourceAssembler.ToCommandFromResource(resource);
        var adminId = await adminCommandService.Handle(createAdminCommand);
        if (adminId is null) return BadRequest();
        var getAdminByIdQuery = new GetAdminByIdQuery((int)adminId);
        var admin = adminQueryService.Handle(getAdminByIdQuery);
        if (admin.Result is null) return NotFound();
        var createdAdmin = admin.Result;
        var adminResource = AdminResourceFromEntityAssembler.ToResourceFromEntity(createdAdmin);
        return CreatedAtAction(nameof(GetAdminById), new { adminId = admin.Id }, adminResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Admins",
        Description = "Get All Admins",
        OperationId = "GetAllAdmins")]
    [SwaggerResponse(StatusCodes.Status200OK, "Admins retrieved successfully", typeof(IEnumerable<AdminResource>))]
    public async Task<IActionResult> GetAllAdmins()
    {
        var admins = await adminQueryService.Handle(new GetAllAdminsQuery());
        var adminResources = admins.Select(AdminResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(adminResources);
    }

    [HttpGet("organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get All Admins By Organization Id",
        Description = "Get All Admins By Organization Id",
        OperationId = "GetAllAdminsByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Admins retrieved successfully", typeof(IEnumerable<AdminResource>))]
    public async Task<IActionResult> GetAllAdminsByOrganizationId(int organizationId)
    {
        var admins = await adminQueryService.Handle(new GetAllAdminsByOrganizationIdQuery(organizationId));
        var adminsResources = admins.Select(AdminResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(adminsResources);
    }

    [HttpGet("user/{userId:int}/organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get Admin By User Id And Organization Id",
        Description = "Get Admin By User Id And Organization Id",
        OperationId = "GetAdminByUserIdAndOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Admin found", typeof(AdminResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Admin not found")]
    public async Task<IActionResult> GetAdminByUserIdAndOrganizationId(int userId, int organizationId)
    {
        var getAdminByUserIdAndOrganizationIdQuery = new GetAdminByUserIdAndOrganizationIdQuery(userId, organizationId);
        var admin = await adminQueryService.Handle(getAdminByUserIdAndOrganizationIdQuery);
        if (admin is null) return NotFound();
        var resource = AdminResourceFromEntityAssembler.ToResourceFromEntity(admin);
        return Ok(resource);
    }

    [HttpPut("{adminId:int}")]
    [SwaggerOperation(
        Summary = "Update An Admin",
        Description = "Update An Admin",
        OperationId = "UpdateAdmin")]
    [SwaggerResponse(StatusCodes.Status200OK, "Admin updated successfully", typeof(AdminResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Admin could not be updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Admin not found")]
    public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminResource resource, int adminId)
    {
        var updateAdminCommand = UpdateAdminCommandFromResourceAssembler.ToCommandFromResource(resource, adminId);
        var updatedAdmin = await adminCommandService.Handle(updateAdminCommand);
        if (updatedAdmin is null) return NotFound();
        var adminResource = AdminResourceFromEntityAssembler.ToResourceFromEntity(updatedAdmin);
        return Ok(adminResource);
    }

    [HttpDelete("{adminId:int}")]
    [SwaggerOperation(
        Summary = "Delete An Admin",
        Description = "Delete An Admin",
        OperationId = "DeleteAdmin")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Admin deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Admin not found")]
    public async Task<IActionResult> DeleteAdmin(int adminId)
    {
        var deleteAdminCommand = new DeleteAdminCommand(adminId);
        try
        {
            await adminCommandService.Handle(deleteAdminCommand);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}