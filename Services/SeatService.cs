
using MovieTicketBookingWebAPI.DTOs.Seats;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;

    public SeatService(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    public async Task<SeatResponseDto> CreateSeatAsync(CreateSeatDto seatDto)
    {
        var seat = new Seat
        {
            ScreenId = seatDto.ScreenId,
            RowName = seatDto.RowName,
            SeatNumber = seatDto.SeatNumber,
            SeatType = seatDto.SeatType
        };

        var createdSeat = await _seatRepository.AddAsync(seat);

        return MapToResponseDto(createdSeat);
    }

    public async Task<List<SeatResponseDto>> CreateSeatsBulkAsync(
        List<CreateSeatDto> seatDtos)
    {
        var seats = seatDtos.Select(dto => new Seat
        {
            ScreenId = dto.ScreenId,
            RowName = dto.RowName,
            SeatNumber = dto.SeatNumber,
            SeatType = dto.SeatType
        }).ToList();

        var createdSeats = await _seatRepository.AddRangeAsync(seats);

        return createdSeats.Select(MapToResponseDto).ToList();
    }

    public async Task<List<SeatResponseDto>> GetSeatsByScreenIdAsync(int screenId)
    {
        var seats = await _seatRepository.GetByScreenIdAsync(screenId);

        return seats.Select(MapToResponseDto).ToList();
    }

    public async Task<SeatResponseDto?> GetSeatByIdAsync(int seatId)
    {
        var seat = await _seatRepository.GetByIdAsync(seatId);

        if (seat == null)
            return null;

        return MapToResponseDto(seat);
    }

    public async Task<SeatResponseDto?> UpdateSeatAsync(
        int seatId,
        UpdateSeatDto seatDto)
    {
        var seat = await _seatRepository.GetByIdAsync(seatId);

        if (seat == null)
            return null;

        seat.ScreenId = seatDto.ScreenId;
        seat.RowName = seatDto.RowName;
        seat.SeatNumber = seatDto.SeatNumber;
        seat.SeatType = seatDto.SeatType;

        await _seatRepository.UpdateAsync(seat);

        return MapToResponseDto(seat);
    }

    public async Task<bool> DeleteSeatAsync(int seatId)
    {
        var seat = await _seatRepository.GetByIdAsync(seatId);

        if (seat == null)
            return false;

        await _seatRepository.DeleteAsync(seat);

        return true;
    }

    private static SeatResponseDto MapToResponseDto(Seat seat)
    {
        return new SeatResponseDto
        {
            SeatId = seat.SeatId,
            ScreenId = seat.ScreenId,
            RowName = seat.RowName,
            SeatNumber = seat.SeatNumber,
            SeatType = seat.SeatType
        };
    }
}

