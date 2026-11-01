
using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<Seat> AddAsync(Seat seat);

    Task<List<Seat>> AddRangeAsync(List<Seat> seats);

    Task<List<Seat>> GetByScreenIdAsync(int screenId);

    Task<Seat?> GetByIdAsync(int seatId);

    Task UpdateAsync(Seat seat);

    Task DeleteAsync(Seat seat);
}
