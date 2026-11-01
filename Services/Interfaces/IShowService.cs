using MovieTicketBookingWebAPI.DTOs.Shows;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IShowService
{
    Task<ShowResponseDto> CreateShowAsync(CreateShowDto dto);

    Task<List<ShowResponseDto>> GetAllShowsAsync(
        DateTime? date,
        int? movieId,
        int? theatreId);

    Task<ShowResponseDto?> GetShowByIdAsync(int showId);

    Task<List<ShowResponseDto>> GetShowsByMovieIdAsync(int movieId);

    Task<List<ShowResponseDto>> GetShowsByTheatreIdAsync(int theatreId);

    Task<List<ShowResponseDto>> GetShowsByDateAsync(DateTime date);

    Task<ShowResponseDto?> UpdateShowAsync(int showId, UpdateShowDto dto);

    Task<bool> DeleteShowAsync(int showId);
}