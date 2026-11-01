using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Bookings;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // POST: api/bookings
    [HttpPost]
    public async Task<IActionResult> CreateBooking(
        CreateBookingDto dto)
    {
        var booking = await _bookingService.CreateBookingAsync(dto);

        return CreatedAtAction(
            nameof(GetBookingById),
            new { id = booking.BookingId },
            booking);
    }

    // GET: api/bookings
    [HttpGet]
    public async Task<IActionResult> GetAllBookings(
        [FromQuery] int? showId,
        [FromQuery] string? status)
    {
        var bookings = await _bookingService.GetAllBookingsAsync(
            showId,
            status);

        return Ok(bookings);
    }

    // GET: api/bookings/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);

        if (booking == null)
        {
            return NotFound(new
            {
                message = "Booking not found."
            });
        }

        return Ok(booking);
    }

    // GET: api/bookings/show/5
    [HttpGet("show/{showId:int}")]
    public async Task<IActionResult> GetBookingsByShowId(int showId)
    {
        var bookings = await _bookingService.GetBookingsByShowIdAsync(
            showId);

        return Ok(bookings);
    }

    // PUT: api/bookings/5/cancel
    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var booking = await _bookingService.CancelBookingAsync(id);

        if (booking == null)
        {
            return NotFound(new
            {
                message = "Booking not found."
            });
        }

        return Ok(booking);
    }
}