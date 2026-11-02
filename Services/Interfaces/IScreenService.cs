
using MovieTicketBookingWebAPI.DTOs.Screens;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

using MovieTicketBookingWebAPI.DTOs.Common;

public interface IScreenService
{
    Task<ScreenResponseDto> CreateScreenAsync(CreateScreenDto screenDto);

    Task<PagedResponseDto<ScreenResponseDto>> GetAllScreensAsync(
    PaginationParams paginationParams);

    Task<PagedResponseDto<ScreenResponseDto>> GetScreensByTheatreIdAsync(
        int theatreId,
        PaginationParams paginationParams);

    Task<ScreenResponseDto?> GetScreenByIdAsync(int screenId);

    

    Task<ScreenResponseDto?> UpdateScreenAsync(
        int screenId,
        UpdateScreenDto screenDto);

    Task<bool> DeleteScreenAsync(int screenId);
}
