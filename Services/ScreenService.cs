
using MovieTicketBookingWebAPI.DTOs.Screens;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Services;

public class ScreenService : IScreenService
{
    private readonly IScreenRepository _screenRepository;

    public ScreenService(IScreenRepository screenRepository)
    {
        _screenRepository = screenRepository;
    }

    public async Task<ScreenResponseDto> CreateScreenAsync(CreateScreenDto screenDto)
    {
        var screen = new Screen
        {
            TheatreId = screenDto.TheatreId,
            ScreenName = screenDto.ScreenName
        };

        var createdScreen = await _screenRepository.AddAsync(screen);

        return MapToResponseDto(createdScreen);
    }

    public async Task<PagedResponseDto<ScreenResponseDto>> GetAllScreensAsync(
    PaginationParams paginationParams)
    {
        var pagedScreens = await _screenRepository.GetAllAsync(paginationParams);

        return new PagedResponseDto<ScreenResponseDto>
        {
            Data = pagedScreens.Data.Select(screen => new ScreenResponseDto
            {
                ScreenId = screen.ScreenId,
                TheatreId = screen.TheatreId,
                ScreenName = screen.ScreenName
            }).ToList(),

            PageNumber = pagedScreens.PageNumber,
            PageSize = pagedScreens.PageSize,
            TotalRecords = pagedScreens.TotalRecords,
            TotalPages = pagedScreens.TotalPages
        };
    }

    public async Task<ScreenResponseDto?> GetScreenByIdAsync(int screenId)
    {
        var screen = await _screenRepository.GetByIdAsync(screenId);

        if (screen == null)
            return null;

        return MapToResponseDto(screen);
    }

    public async Task<PagedResponseDto<ScreenResponseDto>> GetScreensByTheatreIdAsync(
    int theatreId,
    PaginationParams paginationParams)
    {
        var pagedScreens = await _screenRepository.GetByTheatreIdAsync(
                           theatreId,paginationParams);

        return new PagedResponseDto<ScreenResponseDto>
        {
            Data = pagedScreens.Data.Select(screen => new ScreenResponseDto
            {
                ScreenId = screen.ScreenId,
                TheatreId = screen.TheatreId,
                ScreenName = screen.ScreenName
            }).ToList(),

            PageNumber = pagedScreens.PageNumber,
            PageSize = pagedScreens.PageSize,
            TotalRecords = pagedScreens.TotalRecords,
            TotalPages = pagedScreens.TotalPages
        };
    }

    public async Task<ScreenResponseDto?> UpdateScreenAsync(
        int screenId,
        UpdateScreenDto screenDto)
    {
        var screen = await _screenRepository.GetByIdAsync(screenId);

        if (screen == null)
            return null;

        screen.TheatreId = screenDto.TheatreId;
        screen.ScreenName = screenDto.ScreenName;

        await _screenRepository.UpdateAsync(screen);

        return MapToResponseDto(screen);
    }

    public async Task<bool> DeleteScreenAsync(int screenId)
    {
        var screen = await _screenRepository.GetByIdAsync(screenId);

        if (screen == null)
            return false;

        await _screenRepository.DeleteAsync(screen);

        return true;
    }

    private static ScreenResponseDto MapToResponseDto(Screen screen)
    {
        return new ScreenResponseDto
        {
            ScreenId = screen.ScreenId,
            TheatreId = screen.TheatreId,
            ScreenName = screen.ScreenName
        };
    }
}
