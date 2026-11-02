
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Extensions;

namespace MovieTicketBookingWebAPI.Repositories;

public class TheatreRepository : ITheatreRepository
{
    private readonly ApplicationDbContext _context;

    public TheatreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Theatre> AddAsync(Theatre theatre)
    {
        await _context.Theatres.AddAsync(theatre);
        await _context.SaveChangesAsync();

        return theatre;
    }

    public async Task<PagedResponseDto<Theatre>> GetAllAsync(
    PaginationParams paginationParams)
    {
        var query = _context.Theatres
            .AsNoTracking()
            .OrderBy(t => t.TheatreId);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<Theatre?> GetByIdAsync(int theatreId)
    {
        return await _context.Theatres
            .FirstOrDefaultAsync(t => t.TheatreId == theatreId);
    }

    public async Task UpdateAsync(Theatre theatre)
    {
        _context.Theatres.Update(theatre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Theatre theatre)
    {
        _context.Theatres.Remove(theatre);
        await _context.SaveChangesAsync();
    }
}
