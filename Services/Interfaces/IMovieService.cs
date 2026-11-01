using MovieTicketBookingWebAPI.DTOs.Movies;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IMovieService
{
    Task<MovieResponseDto> CreateMovieAsync(CreateMovieDto movieDto);

    Task<List<MovieResponseDto>> GetAllMoviesAsync();

    Task<MovieResponseDto?> GetMovieByIdAsync(int movieId);

    Task<MovieResponseDto?> UpdateMovieAsync(int movieId, UpdateMovieDto movieDto);

    Task<bool> DeleteMovieAsync(int movieId);

    Task<List<MovieResponseDto>> SearchMoviesByTitleAsync(string title);
}