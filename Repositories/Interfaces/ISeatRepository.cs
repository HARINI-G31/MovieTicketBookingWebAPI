
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<Seat> AddAsync(Seat seat);

    Task<List<Seat>> AddRangeAsync(List<Seat> seats);

    Task<PagedResponseDto<Seat>> GetByScreenIdAsync(
    int screenId,
    PaginationParams paginationParams);

    Task<Seat?> GetByIdAsync(int seatId);

    Task UpdateAsync(Seat seat);

    Task DeleteAsync(Seat seat);
    Task<List<Seat>> GetAllByScreenIdAsync(int screenId);
}
