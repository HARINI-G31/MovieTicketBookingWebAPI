using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Shows;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowsController : ControllerBase
{
    private readonly IShowService _showService;

    public ShowsController(IShowService showService)
    {
        _showService = showService;
    }

    // POST: api/shows
    [HttpPost]
    public async Task<IActionResult> CreateShow(CreateShowDto dto)
    {
        var show = await _showService.CreateShowAsync(dto);

        return CreatedAtAction(
            nameof(GetShowById),
            new { id = show.ShowId },
            show);
    }

    // GET: api/shows
    [HttpGet]
    public async Task<IActionResult> GetAllShows(
        [FromQuery] DateTime? date,
        [FromQuery] int? movieId,
        [FromQuery] int? theatreId)
    {
        var shows = await _showService.GetAllShowsAsync(
            date, movieId, theatreId);

        return Ok(shows);
    }

    // GET: api/shows/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetShowById(int id)
    {
        var show = await _showService.GetShowByIdAsync(id);

        if (show == null)
        {
            return NotFound();
        }

        return Ok(show);
    }

    // GET: api/shows/movie/5
    [HttpGet("movie/{movieId:int}")]
    public async Task<IActionResult> GetShowsByMovie(int movieId)
    {
        var shows = await _showService.GetShowsByMovieIdAsync(movieId);

        return Ok(shows);
    }

    // GET: api/shows/theatre/5
    [HttpGet("theatre/{theatreId:int}")]
    public async Task<IActionResult> GetShowsByTheatre(int theatreId)
    {
        var shows = await _showService.GetShowsByTheatreIdAsync(theatreId);

        return Ok(shows);
    }

    // GET: api/shows/search?date=2026-11-01
    [HttpGet("search")]
    public async Task<IActionResult> GetShowsByDate(
        [FromQuery] DateTime date)
    {
        var shows = await _showService.GetShowsByDateAsync(date);

        return Ok(shows);
    }

    // PUT: api/shows/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateShow(
        int id,
        UpdateShowDto dto)
    {
        var show = await _showService.UpdateShowAsync(id, dto);

        if (show == null)
        {
            return NotFound();
        }

        return Ok(show);
    }

    // DELETE: api/shows/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteShow(int id)
    {
        var deleted = await _showService.DeleteShowAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}