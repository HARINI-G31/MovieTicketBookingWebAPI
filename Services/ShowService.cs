using MovieTicketBookingWebAPI.DTOs.Shows;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Services;

public class ShowService : IShowService
{
    private readonly IShowRepository _showRepository;
    private readonly IShowSeatService _showSeatService;

    public ShowService(
        IShowRepository showRepository,
        IShowSeatService showSeatService)
    {
        _showRepository = showRepository;
        _showSeatService = showSeatService;
    }

    public async Task<ShowResponseDto> CreateShowAsync(CreateShowDto dto)
    {
        var existingShows =
            await _showRepository.GetByScreenIdAsync(dto.ScreenId);

        bool hasOverlap = existingShows.Any(s =>
            dto.StartTime < s.EndTime &&
            dto.EndTime > s.StartTime);

        if (hasOverlap)
        {
            throw new InvalidOperationException(
                "The selected screen already has a show scheduled during this time.");
        }

        var show = new Show
        {
            MovieId = dto.MovieId,
            ScreenId = dto.ScreenId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            TicketPrice = dto.TicketPrice
        };

        var createdShow = await _showRepository.AddAsync(show);

        await _showSeatService.InitializeShowSeatsAsync(
            createdShow.ShowId,
            createdShow.ScreenId);

        return MapToResponseDto(createdShow);
    }

    

    public async Task<ShowResponseDto?> GetShowByIdAsync(int showId)
    {
        var show = await _showRepository.GetByIdAsync(showId);

        return show == null ? null : MapToResponseDto(show);
    }

    public async Task<PagedResponseDto<ShowResponseDto>> GetAllShowsAsync(
    DateTime? date,
    int? movieId,
    int? theatreId,
    PaginationParams paginationParams)
    {
        var pagedShows = await _showRepository.GetAllAsync(
            date, movieId, theatreId, paginationParams);

        return MapPagedShows(pagedShows);
    }

    public async Task<PagedResponseDto<ShowResponseDto>> GetShowsByMovieIdAsync(
        int movieId,
        PaginationParams paginationParams)
    {
        var pagedShows = await _showRepository.GetByMovieIdAsync(
            movieId, paginationParams);

        return MapPagedShows(pagedShows);
    }

    public async Task<PagedResponseDto<ShowResponseDto>> GetShowsByTheatreIdAsync(
        int theatreId,
        PaginationParams paginationParams)
    {
        var pagedShows = await _showRepository.GetByTheatreIdAsync(
            theatreId, paginationParams);

        return MapPagedShows(pagedShows);
    }

    public async Task<PagedResponseDto<ShowResponseDto>> GetShowsByDateAsync(
        DateTime date,
        PaginationParams paginationParams)
    {
        var pagedShows = await _showRepository.GetByDateAsync(
            date, paginationParams);

        return MapPagedShows(pagedShows);
    }

    public async Task<ShowResponseDto?> UpdateShowAsync(
        int showId,
        UpdateShowDto dto)
    {
        var show = await _showRepository.GetByIdAsync(showId);

        if (show == null)
        {
            return null;
        }

        var existingShows =
            await _showRepository.GetByScreenIdAsync(dto.ScreenId);

        bool hasOverlap = existingShows.Any(s =>
            s.ShowId != showId &&
            dto.StartTime < s.EndTime &&
            dto.EndTime > s.StartTime);

        if (hasOverlap)
        {
            throw new InvalidOperationException(
                "The selected screen already has a show scheduled during this time.");
        }

        show.MovieId = dto.MovieId;
        show.ScreenId = dto.ScreenId;
        show.StartTime = dto.StartTime;
        show.EndTime = dto.EndTime;
        show.TicketPrice = dto.TicketPrice;

        await _showRepository.UpdateAsync(show);

        return MapToResponseDto(show);
    }

    public async Task<bool> DeleteShowAsync(int showId)
    {
        var show = await _showRepository.GetByIdAsync(showId);

        if (show == null)
        {
            return false;
        }

        await _showRepository.DeleteAsync(show);

        return true;
    }

    private static ShowResponseDto MapToResponseDto(Show show)
    {
        return new ShowResponseDto
        {
            ShowId = show.ShowId,
            MovieId = show.MovieId,
            ScreenId = show.ScreenId,
            StartTime = show.StartTime,
            EndTime = show.EndTime,
            TicketPrice = show.TicketPrice
        };
    }
    private static PagedResponseDto<ShowResponseDto> MapPagedShows(
    PagedResponseDto<Show> pagedShows)
    {
        return new PagedResponseDto<ShowResponseDto>
        {
            Data = pagedShows.Data.Select(show => new ShowResponseDto
            {
                ShowId = show.ShowId,
                MovieId = show.MovieId,
                ScreenId = show.ScreenId,
                StartTime = show.StartTime,
                EndTime = show.EndTime,
                TicketPrice = show.TicketPrice
            }).ToList(),

            PageNumber = pagedShows.PageNumber,
            PageSize = pagedShows.PageSize,
            TotalRecords = pagedShows.TotalRecords,
            TotalPages = pagedShows.TotalPages
        };
    }
}