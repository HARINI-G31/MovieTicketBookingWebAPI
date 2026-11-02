using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Shows;
using MovieTicketBookingWebAPI.Services.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowsController : ControllerBase
{
    private readonly IShowService _showService;
    private readonly IShowSeatService _showSeatService;

    public ShowsController(
        IShowService showService,
        IShowSeatService showSeatService)
    {
        _showService = showService;
        _showSeatService = showSeatService;
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
    [HttpGet]
    public async Task<IActionResult> GetAllShows(
    [FromQuery] DateTime? date,
    [FromQuery] int? movieId,
    [FromQuery] int? theatreId,
    [FromQuery] PaginationParams paginationParams)
    {
        var shows = await _showService.GetAllShowsAsync(
            date, movieId, theatreId, paginationParams);

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
    public async Task<IActionResult> GetShowsByMovieId(
    int movieId,
    [FromQuery] PaginationParams paginationParams)
    {
        var shows = await _showService.GetShowsByMovieIdAsync(
            movieId, paginationParams);

        return Ok(shows);
    }

    // GET: api/shows/theatre/5
    [HttpGet("theatre/{theatreId:int}")]
    public async Task<IActionResult> GetShowsByTheatreId(
    int theatreId,
    [FromQuery] PaginationParams paginationParams)
    {
        var shows = await _showService.GetShowsByTheatreIdAsync(
            theatreId, paginationParams);

        return Ok(shows);
    }

    // GET: api/shows/search?date=2026-11-01
    [HttpGet("search")]
    public async Task<IActionResult> GetShowsByDate(
    [FromQuery] DateTime date,
    [FromQuery] PaginationParams paginationParams)
    {
        var shows = await _showService.GetShowsByDateAsync(
            date, paginationParams);

        return Ok(shows);
    }

    // GET: api/shows/5/seats
    [HttpGet("{showId:int}/seats")]
    public async Task<IActionResult> GetShowSeats(int showId)
    {
        var seats = await _showSeatService.GetShowSeatsAsync(showId);

        return Ok(seats);
    }

    // GET: api/shows/5/seats/available
    [HttpGet("{showId:int}/seats/available")]
    public async Task<IActionResult> GetAvailableSeats(int showId)
    {
        var seats = await _showSeatService.GetAvailableSeatsAsync(showId);

        return Ok(seats);
    }

    // GET: api/shows/5/seats/booked
    [HttpGet("{showId:int}/seats/booked")]
    public async Task<IActionResult> GetBookedSeats(int showId)
    {
        var seats = await _showSeatService.GetBookedSeatsAsync(showId);

        return Ok(seats);
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