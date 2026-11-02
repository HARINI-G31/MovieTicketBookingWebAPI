using MovieTicketBookingWebAPI.DTOs.Bookings;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

using MovieTicketBookingWebAPI.DTOs.Common;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(
        CreateBookingDto dto);

    

    Task<BookingResponseDto?> GetBookingByIdAsync(
        int bookingId);

    Task<PagedResponseDto<BookingResponseDto>> GetAllBookingsAsync(
    int? showId,
    string? status,
    PaginationParams paginationParams);

    Task<PagedResponseDto<BookingResponseDto>> GetBookingsByShowIdAsync(
        int showId,
        PaginationParams paginationParams);

    Task<BookingResponseDto?> CancelBookingAsync(
        int bookingId);
}