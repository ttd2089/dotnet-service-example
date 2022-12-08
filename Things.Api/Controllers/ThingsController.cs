using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Things.Domain.Models;
using Things.Domain.Services;

namespace Things.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ThingsController : ControllerBase
{
    private readonly IThingsService _thingsService;
    private readonly ILogger<ThingsController> _logger;

    public ThingsController(IThingsService thingsService, ILogger<ThingsController> logger)
    {
        _thingsService = thingsService;
        _logger = logger;
    }

    /// <summary>
    /// Get a thing by it's ID.
    /// </summary>
    /// <response code="200">The thing was found.</response>
    /// <response code="404">The thing was not found.</response>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThingResponseBody))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null!)]
    public async Task<ActionResult<ThingResponseBody>> GetThing([FromRoute] Guid id)
    {
        var thing = await _thingsService.GetThingAsync(id);
        return thing == null
            ? NotFound()
            : Ok(new ThingResponseBody(thing));
    }

    /// <summary>
    /// Creates a new thing.
    /// </summary>
    /// <response code="201">The thing was created.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThingResponseBody))]
    public async Task<ActionResult<ThingResponseBody>> CreateThing([FromBody] ThingRequestBody createThingRequest)
    {
        var thing = await _thingsService.CreateThingAsync(createThingRequest.Into());
        return CreatedAtAction(nameof(GetThing), new { id = thing.Id }, new ThingResponseBody(thing));
    }

    /// <summary>
    /// Updates an existing thing.
    /// </summary>
    /// <response code="200">The thing was updated.</response>
    /// <response code="404">The thing does not exist.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThingResponseBody))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null!)]
    public async Task<ActionResult> UpdateThing(
        [FromRoute] Guid id,
        [FromBody] ThingRequestBody updateThingRequest)
    {
        var thing = await _thingsService.UpdateThingAsync(id, updateThingRequest.Into());
        return thing == null
            ? NotFound()
            : Ok(new ThingResponseBody(thing));
    }

    /// <summary>
    /// Deletes a thing by it's ID.
    /// </summary>
    /// <response code="204">The thing was deleted.</response>
    /// <response code="404">The thing does not exist.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = null!)]
    public async Task<ActionResult> DeleteThing([FromRoute] Guid id)
    {
        return await _thingsService.DeleteThingAsync(id)
            ? NoContent()
            : NotFound();
    }

    /// <summary>
    /// A request to create or update a thing.
    /// </summary>
    public class ThingRequestBody
    {
        /// <summary>
        /// The name of the thing.
        /// </summary>
        [Required]
        [StringLength(Thing.MaxNameLength, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// An optional description of the thing.
        /// </summary>
        [StringLength(Thing.MaxDescriptionLength)]
        public string? Description { get; set; }

        internal ThingFields Into() => new(Name, Description);
    }

    /// <summary>
    /// A thing.
    /// </summary>
    public class ThingResponseBody
    {
        /// <summary>
        /// The ID of the thing.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the thing.
        /// </summary>
        [Required]
        [StringLength(Thing.MaxNameLength, MinimumLength = 1)]
        public string Name { get; set; }

        /// <summary>
        /// An optional description of the thing.
        /// </summary>
        [StringLength(Thing.MaxDescriptionLength)]
        public string? Description { get; set; }

        internal ThingResponseBody(Thing thing)
        {
            Id = thing.Id;
            Name = thing.Name;
            Description = thing.Description;
        }
    }
}
