using Microsoft.EntityFrameworkCore;
using MovieTicketBookingWebAPI.Data;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Extensions;

namespace MovieTicketBookingWebAPI.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Show?> GetShowByIdAsync(int showId)
    {
        return await _context.Shows
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ShowId == showId);
    }

    public async Task<List<ShowSeat>> GetShowSeatsByIdsAsync(
        int showId,
        List<int> seatIds)
    {
        return await _context.ShowSeats
            .Include(ss => ss.Seat)
            .Where(ss => ss.ShowId == showId &&
                         seatIds.Contains(ss.SeatId))
            .ToListAsync();
    }

    public async Task<Booking> CreateAsync(
    Booking booking,
    List<ShowSeat> showSeats)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Bookings.Add(booking);

            foreach (var showSeat in showSeats)
            {
                showSeat.Status = "Booked";
            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            // Reload the booking with its seat details.
            return await _context.Bookings
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .FirstAsync(b => b.BookingId == booking.BookingId);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    

    public async Task<Booking?> GetByIdAsync(int bookingId)
    {
        return await _context.Bookings
            .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
            .FirstOrDefaultAsync(b => b.BookingId == bookingId);
    }

    public async Task<PagedResponseDto<Booking>> GetAllAsync(
    int? showId,
    string? status,
    PaginationParams paginationParams)
    {
        var query = _context.Bookings
            .AsNoTracking()
            .AsQueryable();

        if (showId.HasValue)
        {
            query = query.Where(b => b.ShowId == showId.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(b => b.Status == status);
        }

        query = query
            .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
            .OrderByDescending(b => b.BookingDate)
            .ThenByDescending(b => b.BookingId);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task<PagedResponseDto<Booking>> GetByShowIdAsync(
        int showId,
        PaginationParams paginationParams)
    {
        var query = _context.Bookings
            .AsNoTracking()
            .Where(b => b.ShowId == showId)
            .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
            .OrderByDescending(b => b.BookingDate)
            .ThenByDescending(b => b.BookingId);

        return await query.ToPagedResponseAsync(paginationParams);
    }

    public async Task CancelAsync(
        Booking booking,
        List<ShowSeat> showSeats)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            booking.Status = "Cancelled";

            foreach (var showSeat in showSeats)
            {
                showSeat.Status = "Available";
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}