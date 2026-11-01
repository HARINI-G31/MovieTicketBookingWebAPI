using MovieTicketBookingWebAPI.DTOs.Bookings;
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
        CreateBookingDto dto)
    {
        if (dto.SeatIds == null || dto.SeatIds.Count == 0)
        {
            throw new InvalidOperationException(
                "At least one seat must be selected.");
        }

        if (dto.SeatIds.Distinct().Count() != dto.SeatIds.Count)
        {
            throw new InvalidOperationException(
                "Duplicate seat selections are not allowed.");
        }

        var show = await _bookingRepository.GetShowByIdAsync(dto.ShowId);

        if (show == null)
        {
            throw new KeyNotFoundException("Show not found.");
        }

        var showSeats = await _bookingRepository.GetShowSeatsByIdsAsync(
            dto.ShowId,
            dto.SeatIds);

        if (showSeats.Count != dto.SeatIds.Count)
        {
            throw new InvalidOperationException(
                "One or more selected seats do not belong to this show.");
        }

        if (showSeats.Any(ss => ss.Status != "Available"))
        {
            throw new InvalidOperationException(
                "One or more selected seats are already booked.");
        }

        var booking = new Booking
        {
            BookingReference = GenerateBookingReference(),
            ShowId = dto.ShowId,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
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
            booking,
            showSeats);

        return MapToResponseDto(createdBooking);
    }

    public async Task<List<BookingResponseDto>> GetAllBookingsAsync(
        int? showId,
        string? status)
    {
        var bookings = await _bookingRepository.GetAllAsync(showId, status);

        return bookings.Select(MapToResponseDto).ToList();
    }

    public async Task<BookingResponseDto?> GetBookingByIdAsync(
        int bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        return booking == null ? null : MapToResponseDto(booking);
    }

    public async Task<List<BookingResponseDto>> GetBookingsByShowIdAsync(
        int showId)
    {
        var bookings = await _bookingRepository.GetByShowIdAsync(showId);

        return bookings.Select(MapToResponseDto).ToList();
    }

    public async Task<BookingResponseDto?> CancelBookingAsync(
        int bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking == null)
        {
            return null;
        }

        if (booking.Status != "Confirmed")
        {
            throw new InvalidOperationException(
                "Only confirmed bookings can be cancelled.");
        }

        var seatIds = booking.BookingSeats
            .Select(bs => bs.SeatId)
            .ToList();

        var showSeats = await _bookingRepository.GetShowSeatsByIdsAsync(
            booking.ShowId,
            seatIds);

        if (showSeats.Count != seatIds.Count)
        {
            throw new InvalidOperationException(
                "The booking's show seats could not be found.");
        }

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
}