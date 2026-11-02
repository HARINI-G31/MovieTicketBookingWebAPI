
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Theatres;
using MovieTicketBookingWebAPI.Services.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TheatresController : ControllerBase
{
    private readonly ITheatreService _theatreService;

    public TheatresController(ITheatreService theatreService)
    {
        _theatreService = theatreService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTheatre(CreateTheatreDto theatreDto)
    {
        var theatre = await _theatreService.CreateTheatreAsync(theatreDto);

        return CreatedAtAction(
            nameof(GetTheatreById),
            new { id = theatre.TheatreId },
            theatre);
    }

    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> GetAllTheatres(
    [FromQuery] PaginationParams paginationParams)
    {
        var theatres = await _theatreService.GetAllTheatresAsync(paginationParams);
        return Ok(theatres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTheatreById(int id)
    {
        var theatre = await _theatreService.GetTheatreByIdAsync(id);

        if (theatre == null)
            return NotFound(new { message = "Theatre not found." });

        return Ok(theatre);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTheatre(
        int id,
        UpdateTheatreDto theatreDto)
    {
        var theatre = await _theatreService.UpdateTheatreAsync(id, theatreDto);

        if (theatre == null)
            return NotFound(new { message = "Theatre not found." });

        return Ok(theatre);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTheatre(int id)
    {
        var deleted = await _theatreService.DeleteTheatreAsync(id);

        if (!deleted)
            return NotFound(new { message = "Theatre not found." });

        return NoContent();
    }
}

