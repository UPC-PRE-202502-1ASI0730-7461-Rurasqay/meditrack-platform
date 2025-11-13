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
[SwaggerTag("Available Doctors Endpoints")]
public class DoctorsController(
    IDoctorCommandService doctorCommandService,
    IDoctorQueryService doctorQueryService
    ) : ControllerBase
{
    [HttpGet("{doctorId:int}")]
    [SwaggerOperation(
        Summary = "Get Doctor By Id",
        Description = "Get Doctor By Id",
        OperationId = "GetDoctorById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Doctor Found", typeof(DoctorResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Doctor Not Found")]
    public async Task<IActionResult> GetDoctorById(int doctorId)
    {
        var getDoctorByIdQuery = new GetDoctorByIdQuery(doctorId);
        var doctor = await doctorQueryService.Handle(getDoctorByIdQuery);
        if (doctor is null) return NotFound();
        var resource = DoctorResourceFromEntityAssembler.ToResourceFromEntity(doctor);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new doctor",
        Description = "Create a new doctor",
        OperationId = "CreateDoctor")]
    [SwaggerResponse(StatusCodes.Status201Created, "Doctor was created", typeof(DoctorResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Doctor could not be created")]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorResource resource)
    {
        var createDoctorCommand = CreateDoctorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var doctorId = await doctorCommandService.Handle(createDoctorCommand);
        if (doctorId is null) return BadRequest();
        var getDoctorByIdQuery = new GetDoctorByIdQuery((int)doctorId);
        var doctor = doctorQueryService.Handle(getDoctorByIdQuery);
        if (doctor.Result is null) return NotFound();
        var createdDoctor = doctor.Result;
        var doctorResource = DoctorResourceFromEntityAssembler.ToResourceFromEntity(createdDoctor);
        return CreatedAtAction(nameof(GetDoctorById), new { doctorId = doctor.Id }, doctorResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Doctors",
        Description = "Get All Doctors",
        OperationId = "GetAllDoctors")]
    [SwaggerResponse(StatusCodes.Status200OK, "Doctors retrieved successfully", typeof(IEnumerable<DoctorResource>))]
    public async Task<IActionResult> GetAllDoctors()
    {
        var doctors = await doctorQueryService.Handle(new GetAllDoctorsQuery());
        var doctorResources = doctors.Select(DoctorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(doctorResources);
    }

    [HttpGet("/organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get All Doctors By Organization Id",
        Description = "Get All Doctors By Organization Id",
        OperationId = "GetAllDoctorsByOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Doctors retrieved successfully", typeof(IEnumerable<DoctorResource>))]
    public async Task<IActionResult> GetAllDoctorsByOrganizationId(int organizationId)
    {
        var doctors = await doctorQueryService.Handle(new GetAllDoctorsByOrganizationIdQuery(organizationId));
        var doctorResources = doctors.Select(DoctorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(doctorResources);
    }

    [HttpGet("/user/{userId:int}/organization/{organizationId:int}")]
    [SwaggerOperation(
        Summary = "Get Doctor By User Id And Organization Id",
        Description = "Get Doctor By User Id And Organization Id",
        OperationId = "GetDoctorByUserIdAndOrganizationId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Doctor found", typeof(DoctorResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Doctor not found")]
    public async Task<IActionResult> GetDoctorByUserIdAndOrganizationId(int userId, int organizationId)
    {
        var getDoctorByUserIdAndOrganizationIdQuery = new GetDoctorByUserIdAndOrganizationIdQuery(userId, organizationId);
        var doctor = await doctorQueryService.Handle(getDoctorByUserIdAndOrganizationIdQuery);
        if (doctor is null) return NotFound();
        var resource = DoctorResourceFromEntityAssembler.ToResourceFromEntity(doctor);
        return Ok(resource);
    }

    [HttpPut("/{doctorId:int}")]
    [SwaggerOperation(
        Summary = "Update A Doctor",
        Description = "Update A Doctor",
        OperationId = "UpdateDoctor")]
    [SwaggerResponse(StatusCodes.Status200OK, "Doctor updated successfully", typeof(DoctorResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Doctor could not be updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Doctor not found")]
    public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorResource resource, int doctorId)
    {
        var updateDoctorCommand = UpdateDoctorCommandFromResourceAssembler.ToCommandFromResource(resource, doctorId);
        var updatedDoctor = await doctorCommandService.Handle(updateDoctorCommand);
        if (updatedDoctor is null) return NotFound();
        var doctorResource = DoctorResourceFromEntityAssembler.ToResourceFromEntity(updatedDoctor);
        return Ok(doctorResource);
    }

    [HttpDelete("/{doctorId:int}")]
    [SwaggerOperation(
        Summary = "Delete A Doctor",
        Description = "Delete A Doctor",
        OperationId = "DeleteDoctor")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Doctor deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Doctor not found")]
    public async Task<IActionResult> DeleteDoctor(int doctorId)
    {
        var deleteDoctorCommand = new DeleteDoctorCommand(doctorId);
        try
        {
            await doctorCommandService.Handle(deleteDoctorCommand);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
}