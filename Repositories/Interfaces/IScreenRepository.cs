
using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

using MovieTicketBookingWebAPI.DTOs.Common;

public interface IScreenRepository
{
    Task<Screen> AddAsync(Screen screen);

    Task<PagedResponseDto<Screen>> GetAllAsync(PaginationParams paginationParams);

    Task<Screen?> GetByIdAsync(int screenId);

    Task<PagedResponseDto<Screen>> GetByTheatreIdAsync(
    int theatreId,
    PaginationParams paginationParams);

    Task UpdateAsync(Screen screen);

    Task DeleteAsync(Screen screen);
}
