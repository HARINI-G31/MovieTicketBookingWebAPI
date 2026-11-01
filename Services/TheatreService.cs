
using MovieTicketBookingWebAPI.DTOs.Theatres;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Services;

public class TheatreService : ITheatreService
{
    private readonly ITheatreRepository _theatreRepository;

    public TheatreService(ITheatreRepository theatreRepository)
    {
        _theatreRepository = theatreRepository;
    }

    public async Task<TheatreResponseDto> CreateTheatreAsync(CreateTheatreDto theatreDto)
    {
        var theatre = new Theatre
        {
            Name = theatreDto.Name,
            Location = theatreDto.Location
        };

        var createdTheatre = await _theatreRepository.AddAsync(theatre);

        return MapToResponseDto(createdTheatre);
    }

    public async Task<List<TheatreResponseDto>> GetAllTheatresAsync()
    {
        var theatres = await _theatreRepository.GetAllAsync();

        return theatres.Select(MapToResponseDto).ToList();
    }

    public async Task<TheatreResponseDto?> GetTheatreByIdAsync(int theatreId)
    {
        var theatre = await _theatreRepository.GetByIdAsync(theatreId);

        if (theatre == null)
            return null;

        return MapToResponseDto(theatre);
    }

    public async Task<TheatreResponseDto?> UpdateTheatreAsync(
        int theatreId,
        UpdateTheatreDto theatreDto)
    {
        var theatre = await _theatreRepository.GetByIdAsync(theatreId);

        if (theatre == null)
            return null;

        theatre.Name = theatreDto.Name;
        theatre.Location = theatreDto.Location;

        await _theatreRepository.UpdateAsync(theatre);

        return MapToResponseDto(theatre);
    }

    public async Task<bool> DeleteTheatreAsync(int theatreId)
    {
        var theatre = await _theatreRepository.GetByIdAsync(theatreId);

        if (theatre == null)
            return false;

        await _theatreRepository.DeleteAsync(theatre);

        return true;
    }

    private static TheatreResponseDto MapToResponseDto(Theatre theatre)
    {
        return new TheatreResponseDto
        {
            TheatreId = theatre.TheatreId,
            Name = theatre.Name,
            Location = theatre.Location
        };
    }
}
