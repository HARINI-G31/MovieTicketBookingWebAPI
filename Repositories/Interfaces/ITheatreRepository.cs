
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.DTOs.Common;
namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface ITheatreRepository
{
    Task<Theatre> AddAsync(Theatre theatre);

    Task<PagedResponseDto<Theatre>> GetAllAsync(
    PaginationParams paginationParams);

    Task<Theatre?> GetByIdAsync(int theatreId);

    Task UpdateAsync(Theatre theatre);

    Task DeleteAsync(Theatre theatre);
}

