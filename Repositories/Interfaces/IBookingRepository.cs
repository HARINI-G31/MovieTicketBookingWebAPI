using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.DTOs.Common;

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

   

    Task<Booking?> GetByIdAsync(int bookingId);

    Task<PagedResponseDto<Booking>> GetAllAsync(
    int? showId,
    string? status,
    PaginationParams paginationParams);

    Task<PagedResponseDto<Booking>> GetByShowIdAsync(
        int showId,
        PaginationParams paginationParams);

    Task CancelAsync(
        Booking booking,
        List<ShowSeat> showSeats);
}