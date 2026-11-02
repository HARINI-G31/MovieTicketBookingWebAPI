
using MovieTicketBookingWebAPI.DTOs.Bookings;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingResponseDto> CreateBookingAsync(
        CreateBookingDto dto,
        int userId,
        string customerName,
        string customerEmail)
    {
        if (dto.SeatIds == null || dto.SeatIds.Count == 0)
            throw new InvalidOperationException(
                "At least one seat must be selected.");

        if (dto.SeatIds.Distinct().Count() != dto.SeatIds.Count)
            throw new InvalidOperationException(
                "Duplicate seat selections are not allowed.");

        var show = await _bookingRepository.GetShowByIdAsync(dto.ShowId);

        if (show == null)
            throw new KeyNotFoundException("Show not found.");

        var showSeats = await _bookingRepository.GetShowSeatsByIdsAsync(
            dto.ShowId, dto.SeatIds);

        if (showSeats.Count != dto.SeatIds.Count)
            throw new InvalidOperationException(
                "One or more selected seats do not belong to this show.");

        if (showSeats.Any(ss => ss.Status != "Available"))
            throw new InvalidOperationException(
                "One or more selected seats are already booked.");

        var booking = new Booking
        {
            BookingReference = GenerateBookingReference(),
            ShowId = dto.ShowId,
            UserId = userId,
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            CustomerPhone = dto.CustomerPhone,
            BookingDate = DateTime.UtcNow,
            TotalAmount = show.TicketPrice * showSeats.Count,
            Status = "Confirmed",
            BookingSeats = showSeats.Select(ss => new BookingSeat
            {
                SeatId = ss.SeatId,
                Price = show.TicketPrice
            }).ToList()
        };

        var createdBooking = await _bookingRepository.CreateAsync(
            booking, showSeats);

        return MapToResponseDto(createdBooking);
    }

    public async Task<BookingResponseDto?> GetBookingByIdAsync(
        int bookingId,
        int userId,
        bool isAdmin)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            return null;

        if (!isAdmin && booking.UserId != userId)
            return null;

        return MapToResponseDto(booking);
    }

    public async Task<PagedResponseDto<BookingResponseDto>> GetAllBookingsAsync(
        int? showId,
        string? status,
        PaginationParams paginationParams)
    {
        var pagedBookings = await _bookingRepository.GetAllAsync(
            showId, status, paginationParams);

        return MapPagedBookings(pagedBookings);
    }

    public async Task<PagedResponseDto<BookingResponseDto>> GetBookingsByShowIdAsync(
        int showId,
        PaginationParams paginationParams)
    {
        var pagedBookings = await _bookingRepository.GetByShowIdAsync(
            showId, paginationParams);

        return MapPagedBookings(pagedBookings);
    }

    public async Task<BookingResponseDto?> CancelBookingAsync(
        int bookingId,
        int userId,
        bool isAdmin)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
            return null;

        if (!isAdmin && booking.UserId != userId)
            return null;

        if (booking.Status != "Confirmed")
            throw new InvalidOperationException(
                "Only confirmed bookings can be cancelled.");

        var seatIds = booking.BookingSeats
            .Select(bs => bs.SeatId)
            .ToList();

        var showSeats = await _bookingRepository.GetShowSeatsByIdsAsync(
            booking.ShowId, seatIds);

        if (showSeats.Count != seatIds.Count)
            throw new InvalidOperationException(
                "The booking's show seats could not be found.");

        await _bookingRepository.CancelAsync(booking, showSeats);

        return MapToResponseDto(booking);
    }

    private static string GenerateBookingReference()
    {
        return "MTB" + Guid.NewGuid()
            .ToString("N")
            .Substring(0, 12)
            .ToUpperInvariant();
    }

    private static BookingResponseDto MapToResponseDto(Booking booking)
    {
        return new BookingResponseDto
        {
            BookingId = booking.BookingId,
            BookingReference = booking.BookingReference,
            ShowId = booking.ShowId,
            CustomerName = booking.CustomerName,
            CustomerEmail = booking.CustomerEmail,
            CustomerPhone = booking.CustomerPhone,
            BookingDate = booking.BookingDate,
            TotalAmount = booking.TotalAmount,
            Status = booking.Status,
            Seats = booking.BookingSeats.Select(bs => new BookingSeatResponseDto
            {
                BookingSeatId = bs.BookingSeatId,
                SeatId = bs.SeatId,
                RowName = bs.Seat?.RowName ?? string.Empty,
                SeatNumber = bs.Seat?.SeatNumber ?? 0,
                SeatType = bs.Seat?.SeatType ?? string.Empty,
                Price = bs.Price
            }).ToList()
        };
    }

    private static PagedResponseDto<BookingResponseDto> MapPagedBookings(
        PagedResponseDto<Booking> pagedBookings)
    {
        return new PagedResponseDto<BookingResponseDto>
        {
            Data = pagedBookings.Data.Select(MapToResponseDto).ToList(),
            PageNumber = pagedBookings.PageNumber,
            PageSize = pagedBookings.PageSize,
            TotalRecords = pagedBookings.TotalRecords,
            TotalPages = pagedBookings.TotalPages
        };
    }
}
