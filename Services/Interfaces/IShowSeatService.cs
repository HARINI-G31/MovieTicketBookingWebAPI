using MovieTicketBookingWebAPI.DTOs.Shows;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface IShowSeatService
{
    Task InitializeShowSeatsAsync(int showId, int screenId);

    Task<List<ShowSeatResponseDto>> GetShowSeatsAsync(int showId);

    Task<List<ShowSeatResponseDto>> GetAvailableSeatsAsync(int showId);

    Task<List<ShowSeatResponseDto>> GetBookedSeatsAsync(int showId);
}