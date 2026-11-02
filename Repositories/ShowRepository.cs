using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Extensions;

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

   

    public async Task<Show?> GetByIdAsync(int showId)
    {
        return await _context.Shows
            .FirstOrDefaultAsync(s => s.ShowId == showId);
    }



    public async Task<PagedResponseDto<Show>> GetAllAsync(
    DateTime? date,
    int? movieId,
    int? theatreId,
    PaginationParams paginationParams)
    {
        var query = _context.Shows
            .AsNoTracking()
            .AsQueryable();

        if (date.HasValue)
        {
            query = query.Where(s => s.StartTime.Date == date.Value.Date);
        }

        if (movieId.HasValue)
        {
            query = query.Where(s => s.MovieId == movieId.Value);
        }

        if (theatreId.HasValue)
        {
            query = query.Where(s => s.Screen.TheatreId == theatreId.Value);
        }

        query = query.OrderBy(s => s.StartTime);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<PagedResponseDto<Show>> GetByMovieIdAsync(
        int movieId,
        PaginationParams paginationParams)
    {
        var query = _context.Shows
            .AsNoTracking()
            .Where(s => s.MovieId == movieId)
            .OrderBy(s => s.StartTime);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<PagedResponseDto<Show>> GetByTheatreIdAsync(
        int theatreId,
        PaginationParams paginationParams)
    {
        var query = _context.Shows
            .AsNoTracking()
            .Where(s => s.Screen.TheatreId == theatreId)
            .OrderBy(s => s.StartTime);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<PagedResponseDto<Show>> GetByDateAsync(
        DateTime date,
        PaginationParams paginationParams)
    {
        var query = _context.Shows
            .AsNoTracking()
            .Where(s => s.StartTime.Date == date.Date)
            .OrderBy(s => s.StartTime);

        return await query.ToPagedResponseAsync(paginationParams);
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