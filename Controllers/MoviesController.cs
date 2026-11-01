using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Movies;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(CreateMovieDto movieDto)
    {
        var movie = await _movieService.CreateMovieAsync(movieDto);

        return CreatedAtAction(
            nameof(GetMovieById),
            new { id = movie.MovieId },
            movie);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();

        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);

        if (movie == null)
            return NotFound(new { message = "Movie not found." });

        return Ok(movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(
        int id,
        UpdateMovieDto movieDto)
    {
        var movie = await _movieService.UpdateMovieAsync(id, movieDto);

        if (movie == null)
            return NotFound(new { message = "Movie not found." });

        return Ok(movie);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var deleted = await _movieService.DeleteMovieAsync(id);

        if (!deleted)
            return NotFound(new { message = "Movie not found." });

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMovies([FromQuery] string title)
    {
        var movies = await _movieService.SearchMoviesByTitleAsync(title);

        return Ok(movies);
    }
}