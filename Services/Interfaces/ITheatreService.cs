
using MovieTicketBookingWebAPI.DTOs.Theatres;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

public interface ITheatreService
{
    Task<TheatreResponseDto> CreateTheatreAsync(CreateTheatreDto theatreDto);

    Task<List<TheatreResponseDto>> GetAllTheatresAsync();

    Task<TheatreResponseDto?> GetTheatreByIdAsync(int theatreId);

    Task<TheatreResponseDto?> UpdateTheatreAsync(int theatreId, UpdateTheatreDto theatreDto);

    Task<bool> DeleteTheatreAsync(int theatreId);
}
