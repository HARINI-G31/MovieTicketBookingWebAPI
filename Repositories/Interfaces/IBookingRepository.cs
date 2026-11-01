using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<Show?> GetShowByIdAsync(int showId);

    Task<List<ShowSeat>> GetShowSeatsByIdsAsync(
        int showId,
        List<int> seatIds);

    Task<Booking> CreateAsync(
        Booking booking,
        List<ShowSeat> showSeats);

    Task<List<Booking>> GetAllAsync(
        int? showId,
        string? status);

    Task<Booking?> GetByIdAsync(int bookingId);

    Task<List<Booking>> GetByShowIdAsync(int showId);

    Task CancelAsync(
        Booking booking,
        List<ShowSeat> showSeats);
}