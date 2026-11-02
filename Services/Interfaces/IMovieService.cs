using MovieTicketBookingWebAPI.DTOs.Movies;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IMovieService
{
    Task<MovieResponseDto> CreateMovieAsync(CreateMovieDto movieDto);

    Task<PagedResponseDto<MovieResponseDto>> GetAllMoviesAsync(
    PaginationParams paginationParams);

    Task<MovieResponseDto?> GetMovieByIdAsync(int movieId);

    Task<MovieResponseDto?> UpdateMovieAsync(int movieId, UpdateMovieDto movieDto);

    Task<bool> DeleteMovieAsync(int movieId);

    Task<List<MovieResponseDto>> SearchMoviesByTitleAsync(string title);
}