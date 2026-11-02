
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Screens;
using MovieTicketBookingWebAPI.Services.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ScreensController : ControllerBase
{
    private readonly IScreenService _screenService;

    public ScreensController(IScreenService screenService)
    {
        _screenService = screenService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateScreen(CreateScreenDto screenDto)
    {
        var screen = await _screenService.CreateScreenAsync(screenDto);

        return CreatedAtAction(
            nameof(GetScreenById),
            new { id = screen.ScreenId },
            screen);
    }

    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> GetAllScreens(
    [FromQuery] PaginationParams paginationParams)
    {
        var screens = await _screenService.GetAllScreensAsync(paginationParams);
        return Ok(screens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetScreenById(int id)
    {
        var screen = await _screenService.GetScreenByIdAsync(id);

        if (screen == null)
            return NotFound(new { message = "Screen not found." });

        return Ok(screen);
    }

    [HttpGet("theatre/{theatreId:int}")]
    public async Task<IActionResult> GetScreensByTheatreId(
    int theatreId,
    [FromQuery] PaginationParams paginationParams)
    {
        var screens = await _screenService.GetScreensByTheatreIdAsync(
            theatreId, paginationParams);

        return Ok(screens);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScreen(
        int id,
        UpdateScreenDto screenDto)
    {
        var screen = await _screenService.UpdateScreenAsync(id, screenDto);

        if (screen == null)
            return NotFound(new { message = "Screen not found." });

        return Ok(screen);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScreen(int id)
    {
        var deleted = await _screenService.DeleteScreenAsync(id);

        if (!deleted)
            return NotFound(new { message = "Screen not found." });

        return NoContent();
    }
}
