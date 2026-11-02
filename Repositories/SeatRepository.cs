
using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Extensions;

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

    public async Task<PagedResponseDto<Seat>> GetByScreenIdAsync(
    int screenId,
    PaginationParams paginationParams)
    {
        var query = _context.Seats
            .AsNoTracking()
            .Where(seat => seat.ScreenId == screenId)
            .OrderBy(seat => seat.RowName)
            .ThenBy(seat => seat.SeatNumber);

        return await query.ToPagedResponseAsync(paginationParams);
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
    public async Task<List<Seat>> GetAllByScreenIdAsync(int screenId)
    {
        return await _context.Seats
            .AsNoTracking()
            .Where(seat => seat.ScreenId == screenId)
            .OrderBy(seat => seat.RowName)
            .ThenBy(seat => seat.SeatNumber)
            .ToListAsync();
    }
}
