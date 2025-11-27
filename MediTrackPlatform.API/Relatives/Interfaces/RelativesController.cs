using System.Net.Mime;
using MediTrackPlatform.API.Relatives.Domain.Model.Queries;
using MediTrackPlatform.API.Relatives.Domain.Services;
using MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;
using MediTrackPlatform.API.Relatives.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediTrackPlatform.API.Relatives.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Relatives")]
public class RelativesController(IRelativeQueryService relativeQueryService) : ControllerBase
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
}