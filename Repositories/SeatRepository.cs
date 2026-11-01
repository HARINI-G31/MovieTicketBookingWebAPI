
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;

namespace MovieTicketBookingWebAPI.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly ApplicationDbContext _context;

    public SeatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Seat> AddAsync(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
        await _context.SaveChangesAsync();

        return seat;
    }

    public async Task<List<Seat>> AddRangeAsync(List<Seat> seats)
    {
        await _context.Seats.AddRangeAsync(seats);
        await _context.SaveChangesAsync();

        return seats;
    }

    public async Task<List<Seat>> GetByScreenIdAsync(int screenId)
    {
        return await _context.Seats
            .AsNoTracking()
            .Where(s => s.ScreenId == screenId)
            .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(int seatId)
    {
        return await _context.Seats
            .FirstOrDefaultAsync(s => s.SeatId == seatId);
    }

    public async Task UpdateAsync(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Seat seat)
    {
        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync();
    }
}
