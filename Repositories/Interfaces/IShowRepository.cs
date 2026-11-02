using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface IShowRepository
{
    Task<Show> AddAsync(Show show);

    
    Task<Show?> GetByIdAsync(int showId);


    Task<PagedResponseDto<Show>> GetAllAsync(
    DateTime? date,
    int? movieId,
    int? theatreId,
    PaginationParams paginationParams);

    Task<PagedResponseDto<Show>> GetByMovieIdAsync(
        int movieId,
        PaginationParams paginationParams);

    Task<PagedResponseDto<Show>> GetByTheatreIdAsync(
        int theatreId,
        PaginationParams paginationParams);

    Task<PagedResponseDto<Show>> GetByDateAsync(
        DateTime date,
        PaginationParams paginationParams);
    Task<List<Show>> GetByScreenIdAsync(int screenId);

    Task UpdateAsync(Show show);

    Task DeleteAsync(Show show);
}