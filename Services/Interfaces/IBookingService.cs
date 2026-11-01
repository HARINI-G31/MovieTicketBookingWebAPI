using MovieTicketBookingWebAPI.DTOs.Bookings;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(
        CreateBookingDto dto);

    Task<List<BookingResponseDto>> GetAllBookingsAsync(
        int? showId,
        string? status);

    Task<BookingResponseDto?> GetBookingByIdAsync(
        int bookingId);

    Task<List<BookingResponseDto>> GetBookingsByShowIdAsync(
        int showId);

    Task<BookingResponseDto?> CancelBookingAsync(
        int bookingId);
}