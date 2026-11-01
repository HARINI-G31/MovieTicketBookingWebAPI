
using MovieTicketBookingWebAPI.DTOs.Seats;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface ISeatService
{
    Task<SeatResponseDto> CreateSeatAsync(CreateSeatDto seatDto);

    Task<List<SeatResponseDto>> CreateSeatsBulkAsync(
        List<CreateSeatDto> seatDtos);

    Task<List<SeatResponseDto>> GetSeatsByScreenIdAsync(int screenId);

    Task<SeatResponseDto?> GetSeatByIdAsync(int seatId);

    Task<SeatResponseDto?> UpdateSeatAsync(
        int seatId,
        UpdateSeatDto seatDto);

    Task<bool> DeleteSeatAsync(int seatId);
}
