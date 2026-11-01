
using MovieTicketBookingWebAPI.DTOs.Screens;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IScreenService
{
    Task<ScreenResponseDto> CreateScreenAsync(CreateScreenDto screenDto);

    Task<List<ScreenResponseDto>> GetAllScreensAsync();

    Task<ScreenResponseDto?> GetScreenByIdAsync(int screenId);

    Task<List<ScreenResponseDto>> GetScreensByTheatreIdAsync(int theatreId);

    Task<ScreenResponseDto?> UpdateScreenAsync(
        int screenId,
        UpdateScreenDto screenDto);

    Task<bool> DeleteScreenAsync(int screenId);
}
