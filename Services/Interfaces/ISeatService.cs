
using MovieTicketBookingWebAPI.DTOs.Seats;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

using MovieTicketBookingWebAPI.DTOs.Common;

public interface ISeatService
{
    Task<SeatResponseDto> CreateSeatAsync(CreateSeatDto seatDto);

    Task<List<SeatResponseDto>> CreateSeatsBulkAsync(
        List<CreateSeatDto> seatDtos);

    Task<PagedResponseDto<SeatResponseDto>> GetSeatsByScreenIdAsync(
    int screenId,
    PaginationParams paginationParams);

    Task<SeatResponseDto?> GetSeatByIdAsync(int seatId);

    Task<SeatResponseDto?> UpdateSeatAsync(
        int seatId,
        UpdateSeatDto seatDto);

    Task<bool> DeleteSeatAsync(int seatId);
}
