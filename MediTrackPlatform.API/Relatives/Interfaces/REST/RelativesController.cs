using System.Net.Mime;
using MediTrackPlatform.API.Relatives.Domain.Model.Queries;
using MediTrackPlatform.API.Relatives.Domain.Services;
using MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;
using MediTrackPlatform.API.Relatives.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediTrackPlatform.API.Relatives.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Relatives")]
public class RelativesController(
    IRelativeQueryService relativeQueryService,
    IRelativeCommandService relativeCommandService
    ) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get Relative by Id",
        Description = "Retrieves a relative and its associated senior citizen by relative ID.",
        OperationId = "GetRelativeById")]
    [SwaggerResponse(200, "Relative retrieved successfully.", typeof(RelativeResource))]
    [SwaggerResponse(404, "Relative not found.")]
    public async Task<IActionResult> GetRelativeById(int id)
    {
        var query = new GetRelativesByIdQuery(id);
        var result = await relativeQueryService.Handle(query);

        if (result is null)
            return NotFound();

        var resource = RelativeResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get Relative by User Id",
        Description = "Retrieves a relative and its associated senior citizen by User ID.",
        OperationId = "GetRelativeByUserId")]
    [SwaggerResponse(200, "Relative retrieved successfully.", typeof(RelativeResource))]
    [SwaggerResponse(404, "Relative not found.")]
    public async Task<IActionResult> GetRelativeByUserId(int userId)
    {
        var query = new GetRelativeByUserIdQuery(userId);
        var result = await relativeQueryService.Handle(query);

        if (result is null)
            return NotFound();

        var resource = RelativeResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Relative",
        Description = "Creates a new relative with the provided information.",
        OperationId = "CreateRelative")]
    [SwaggerResponse(201, "Relative created successfully.", typeof(RelativeResource))]
    [SwaggerResponse(400, "Bad request.")]
    public async Task<IActionResult> CreateRelative([FromBody] CreateRelativeResource resource)
    {
        var command = CreateRelativeCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await relativeCommandService.Handle(command);

        if (result is null)
            return BadRequest();

        var relativeResource = RelativeResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetRelativeById), new { id = result.Id }, relativeResource);
    }
}