using MovieTicketBookingWebAPI.DTOs.Shows;
using MovieTicketBookingWebAPI.DTOs.Common;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IShowService
{
    Task<ShowResponseDto> CreateShowAsync(CreateShowDto dto);

    Task<PagedResponseDto<ShowResponseDto>> GetAllShowsAsync(
        DateTime? date,
        int? movieId,
        int? theatreId,
        PaginationParams paginationParams);

    Task<ShowResponseDto?> GetShowByIdAsync(int showId);

    Task<PagedResponseDto<ShowResponseDto>> GetShowsByMovieIdAsync(
        int movieId,
        PaginationParams paginationParams);

    Task<PagedResponseDto<ShowResponseDto>> GetShowsByTheatreIdAsync(
        int theatreId,
        PaginationParams paginationParams);

    Task<PagedResponseDto<ShowResponseDto>> GetShowsByDateAsync(
        DateTime date,
        PaginationParams paginationParams);

    Task<ShowResponseDto?> UpdateShowAsync(int showId, UpdateShowDto dto);

    Task<bool> DeleteShowAsync(int showId);
}