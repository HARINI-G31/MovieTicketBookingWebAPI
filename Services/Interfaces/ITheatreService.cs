
using MovieTicketBookingWebAPI.DTOs.Theatres;

namespace MovieTicketBookingWebAPI.Services.Interfaces;

using MovieTicketBookingWebAPI.DTOs.Common;

public interface ITheatreService
{
    Task<TheatreResponseDto> CreateTheatreAsync(CreateTheatreDto theatreDto);

    Task<PagedResponseDto<TheatreResponseDto>> GetAllTheatresAsync(
    PaginationParams paginationParams);

    Task<TheatreResponseDto?> GetTheatreByIdAsync(int theatreId);

    Task<TheatreResponseDto?> UpdateTheatreAsync(int theatreId, UpdateTheatreDto theatreDto);

    Task<bool> DeleteTheatreAsync(int theatreId);
}
