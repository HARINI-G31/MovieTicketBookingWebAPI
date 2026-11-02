using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Extensions;

namespace MovieTicketBookingWebAPI.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<PagedResponseDto<Movie>> GetAllAsync(
    PaginationParams paginationParams)
    {
        var query = _context.Movies
            .AsNoTracking()
            .OrderBy(m => m.MovieId);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<Movie?> GetByIdAsync(int movieId)
    {
        return await _context.Movies
            .FirstOrDefaultAsync(m => m.MovieId == movieId);
    }

    public async Task UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Movie movie)
    {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Movie>> SearchByTitleAsync(string title)
    {
        return await _context.Movies
            .AsNoTracking()
            .Where(m => m.Title.Contains(title))
            .OrderBy(m => m.MovieId)
            .ToListAsync();
    }
}