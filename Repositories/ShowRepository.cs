using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;

namespace MovieTicketBookingWebAPI.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly ApplicationDbContext _context;

    public ShowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Show> AddAsync(Show show)
    {
        await _context.Shows.AddAsync(show);
        await _context.SaveChangesAsync();

        return show;
    }

    public async Task<List<Show>> GetAllAsync(
        DateTime? date,
        int? movieId,
        int? theatreId)
    {
        var query = _context.Shows.AsNoTracking().AsQueryable();

        if (date.HasValue)
        {
            var startDate = date.Value.Date;
            var endDate = startDate.AddDays(1);

            query = query.Where(s =>
                s.StartTime >= startDate &&
                s.StartTime < endDate);
        }

        if (movieId.HasValue)
        {
            query = query.Where(s => s.MovieId == movieId.Value);
        }

        if (theatreId.HasValue)
        {
            query = query.Where(s =>
                s.Screen.TheatreId == theatreId.Value);
        }

        return await query
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Show?> GetByIdAsync(int showId)
    {
        return await _context.Shows
            .FirstOrDefaultAsync(s => s.ShowId == showId);
    }

    public async Task<List<Show>> GetByMovieIdAsync(int movieId)
    {
        return await _context.Shows
            .AsNoTracking()
            .Where(s => s.MovieId == movieId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<List<Show>> GetByTheatreIdAsync(int theatreId)
    {
        return await _context.Shows
            .AsNoTracking()
            .Where(s => s.Screen.TheatreId == theatreId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<List<Show>> GetByDateAsync(DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1);

        return await _context.Shows
            .AsNoTracking()
            .Where(s =>
                s.StartTime >= startDate &&
                s.StartTime < endDate)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task UpdateAsync(Show show)
    {
        _context.Shows.Update(show);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Show show)
    {
        _context.Shows.Remove(show);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Show>> GetByScreenIdAsync(int screenId)
    {
        return await _context.Shows
            .AsNoTracking()
            .Where(s => s.ScreenId == screenId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }
}