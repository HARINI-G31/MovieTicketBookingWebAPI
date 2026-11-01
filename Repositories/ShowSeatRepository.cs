using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;

namespace MovieTicketBookingWebAPI.Repositories;

public class ShowSeatRepository : IShowSeatRepository
{
    private readonly ApplicationDbContext _context;

    public ShowSeatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(List<ShowSeat> showSeats)
    {
        await _context.ShowSeats.AddRangeAsync(showSeats);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ShowSeat>> GetByShowIdAsync(int showId)
    {
        return await _context.ShowSeats
            .AsNoTracking()
            .Include(ss => ss.Seat)
            .Where(ss => ss.ShowId == showId)
            .OrderBy(ss => ss.Seat.RowName)
            .ThenBy(ss => ss.Seat.SeatNumber)
            .ToListAsync();
    }

    public async Task<List<ShowSeat>> GetAvailableByShowIdAsync(int showId)
    {
        return await _context.ShowSeats
            .AsNoTracking()
            .Include(ss => ss.Seat)
            .Where(ss => ss.ShowId == showId && ss.Status == "Available")
            .OrderBy(ss => ss.Seat.RowName)
            .ThenBy(ss => ss.Seat.SeatNumber)
            .ToListAsync();
    }

    public async Task<List<ShowSeat>> GetBookedByShowIdAsync(int showId)
    {
        return await _context.ShowSeats
            .AsNoTracking()
            .Include(ss => ss.Seat)
            .Where(ss => ss.ShowId == showId && ss.Status == "Booked")
            .OrderBy(ss => ss.Seat.RowName)
            .ThenBy(ss => ss.Seat.SeatNumber)
            .ToListAsync();
    }
}