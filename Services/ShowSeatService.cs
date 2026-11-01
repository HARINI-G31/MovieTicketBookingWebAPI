using MovieTicketBookingWebAPI.DTOs.Shows;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Services;

public class ShowSeatService : IShowSeatService
{
    private readonly IShowSeatRepository _showSeatRepository;
    private readonly ISeatRepository _seatRepository;

    public ShowSeatService(
        IShowSeatRepository showSeatRepository,
        ISeatRepository seatRepository)
    {
        _showSeatRepository = showSeatRepository;
        _seatRepository = seatRepository;
    }

    // Initialize all seats for a newly created show
    public async Task InitializeShowSeatsAsync(int showId, int screenId)
    {
        var seats = await _seatRepository.GetByScreenIdAsync(screenId);

        var showSeats = seats.Select(seat => new ShowSeat
        {
            ShowId = showId,
            SeatId = seat.SeatId,
            Status = "Available"
        }).ToList();

        if (showSeats.Count > 0)
        {
            await _showSeatRepository.AddRangeAsync(showSeats);
        }
    }

    // Get all seats for a show
    public async Task<List<ShowSeatResponseDto>> GetShowSeatsAsync(int showId)
    {
        var showSeats = await _showSeatRepository.GetByShowIdAsync(showId);

        return showSeats.Select(MapToResponseDto).ToList();
    }

    // Get available seats
    public async Task<List<ShowSeatResponseDto>> GetAvailableSeatsAsync(int showId)
    {
        var showSeats =
            await _showSeatRepository.GetAvailableByShowIdAsync(showId);

        return showSeats.Select(MapToResponseDto).ToList();
    }

    // Get booked seats
    public async Task<List<ShowSeatResponseDto>> GetBookedSeatsAsync(int showId)
    {
        var showSeats =
            await _showSeatRepository.GetBookedByShowIdAsync(showId);

        return showSeats.Select(MapToResponseDto).ToList();
    }

    // Map ShowSeat entity to response DTO
    private static ShowSeatResponseDto MapToResponseDto(ShowSeat showSeat)
    {
        return new ShowSeatResponseDto
        {
            ShowSeatId = showSeat.ShowSeatId,
            ShowId = showSeat.ShowId,
            SeatId = showSeat.SeatId,
            RowName = showSeat.Seat.RowName,
            SeatNumber = showSeat.Seat.SeatNumber,
            SeatType = showSeat.Seat.SeatType,
            Status = showSeat.Status
        };
    }
}