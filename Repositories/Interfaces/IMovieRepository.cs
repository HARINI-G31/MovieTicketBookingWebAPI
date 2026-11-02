using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

using MovieTicketBookingWebAPI.DTOs.Common;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie movie);

    Task<PagedResponseDto<Movie>> GetAllAsync(
    PaginationParams paginationParams);

    Task<Movie?> GetByIdAsync(int movieId);

    Task UpdateAsync(Movie movie);

    Task DeleteAsync(Movie movie);
    Task<List<Movie>> SearchByTitleAsync(string title);

    //Task<bool> HasShowsAsync(int movieId);
}