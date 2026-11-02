
using MovieTicketBookingWebAPI.DTOs.Bookings;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(
        CreateBookingDto dto,
        int userId,
        string customerName,
        string customerEmail);

    Task<BookingResponseDto?> GetBookingByIdAsync(
        int bookingId,
        int userId,
        bool isAdmin);

    Task<PagedResponseDto<BookingResponseDto>> GetAllBookingsAsync(
        int? showId,
        string? status,
        PaginationParams paginationParams);

    Task<PagedResponseDto<BookingResponseDto>> GetBookingsByShowIdAsync(
        int showId,
        PaginationParams paginationParams);

    Task<BookingResponseDto?> CancelBookingAsync(
        int bookingId,
        int userId,
        bool isAdmin);
}
