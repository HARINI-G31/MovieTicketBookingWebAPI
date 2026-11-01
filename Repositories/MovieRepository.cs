using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;

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

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _context.Movies
            .AsNoTracking()
            .ToListAsync();
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
}