
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Extensions;

namespace MovieTicketBookingWebAPI.Repositories;

public class ScreenRepository : IScreenRepository
{
    private readonly ApplicationDbContext _context;

    public ScreenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Screen> AddAsync(Screen screen)
    {
        await _context.Screens.AddAsync(screen);
        await _context.SaveChangesAsync();

        return screen;
    }

    public async Task<PagedResponseDto<Screen>> GetAllAsync(
    PaginationParams paginationParams)
    {
        var query = _context.Screens
            .AsNoTracking()
            .OrderBy(s => s.ScreenId);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<PagedResponseDto<Screen>> GetByTheatreIdAsync(
        int theatreId,
        PaginationParams paginationParams)
    {
        var query = _context.Screens
            .AsNoTracking()
            .Where(s => s.TheatreId == theatreId)
            .OrderBy(s => s.ScreenId);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<Screen?> GetByIdAsync(int screenId)
    {
        return await _context.Screens
            .FirstOrDefaultAsync(s => s.ScreenId == screenId);
    }

    public async Task UpdateAsync(Screen screen)
    {
        _context.Screens.Update(screen);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Screen screen)
    {
        _context.Screens.Remove(screen);
        await _context.SaveChangesAsync();
    }
}
