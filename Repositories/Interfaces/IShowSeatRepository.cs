using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface IShowSeatRepository
{
    Task AddRangeAsync(List<ShowSeat> showSeats);

    Task<List<ShowSeat>> GetByShowIdAsync(int showId);

    Task<List<ShowSeat>> GetAvailableByShowIdAsync(int showId);

    Task<List<ShowSeat>> GetBookedByShowIdAsync(int showId);
}