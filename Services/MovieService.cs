using MovieTicketBookingWebAPI.DTOs.Movies;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieResponseDto> CreateMovieAsync(CreateMovieDto movieDto)
    {
        var movie = new Movie
        {
            Title = movieDto.Title,
            Description = movieDto.Description,
            DurationMinutes = movieDto.DurationMinutes,
            ReleaseDate = movieDto.ReleaseDate,
            Language = movieDto.Language,
            Genre = movieDto.Genre
        };

        var createdMovie = await _movieRepository.AddAsync(movie);

        return MapToResponseDto(createdMovie);
    }

    public async Task<List<MovieResponseDto>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllAsync();

        return movies.Select(MapToResponseDto).ToList();
    }

    public async Task<MovieResponseDto?> GetMovieByIdAsync(int movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        return movie == null ? null : MapToResponseDto(movie);
    }

    public async Task<MovieResponseDto?> UpdateMovieAsync(
        int movieId,
        UpdateMovieDto movieDto)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        if (movie == null)
            return null;

        movie.Title = movieDto.Title;
        movie.Description = movieDto.Description;
        movie.DurationMinutes = movieDto.DurationMinutes;
        movie.ReleaseDate = movieDto.ReleaseDate;
        movie.Language = movieDto.Language;
        movie.Genre = movieDto.Genre;

        await _movieRepository.UpdateAsync(movie);

        return MapToResponseDto(movie);
    }

    public async Task<bool> DeleteMovieAsync(int movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        if (movie == null)
            return false;

        await _movieRepository.DeleteAsync(movie);

        return true;
    }

    public async Task<List<MovieResponseDto>> SearchMoviesByTitleAsync(string title)
    {
        var movies = await _movieRepository.GetAllAsync();

        return movies
            .Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .Select(MapToResponseDto)
            .ToList();
    }

    private static MovieResponseDto MapToResponseDto(Movie movie)
    {
        return new MovieResponseDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Description = movie.Description,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            Language = movie.Language,
            Genre = movie.Genre
        };
    }
}