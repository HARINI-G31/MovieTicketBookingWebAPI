
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieTicketBookingWebAPI.Constants;
using MovieTicketBookingWebAPI.DTOs.Bookings;
using MovieTicketBookingWebAPI.DTOs.Common;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    // POST: api/bookings
    [HttpPost]
    [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
    public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var customerName = User.FindFirstValue(ClaimTypes.Name)!;
        var customerEmail = User.FindFirstValue(ClaimTypes.Email)!;

        var booking = await _bookingService.CreateBookingAsync(
            dto, userId, customerName, customerEmail);

        return CreatedAtAction(
            nameof(GetBookingById),
            new { id = booking.BookingId },
            booking);
    }

    // GET: api/bookings
    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetAllBookings(
        [FromQuery] int? showId,
        [FromQuery] string? status,
        [FromQuery] PaginationParams paginationParams)
    {
        var bookings = await _bookingService.GetAllBookingsAsync(
            showId, status, paginationParams);

        return Ok(bookings);
    }

    // GET: api/bookings/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var isAdmin = User.IsInRole(UserRoles.Admin);

        var booking = await _bookingService.GetBookingByIdAsync(
            id, userId, isAdmin);

        if (booking == null)
        {
            return NotFound(new { message = "Booking not found." });
        }

        return Ok(booking);
    }

    // GET: api/bookings/show/5
    [HttpGet("show/{showId:int}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetBookingsByShowId(
        int showId,
        [FromQuery] PaginationParams paginationParams)
    {
        var bookings = await _bookingService.GetBookingsByShowIdAsync(
            showId, paginationParams);

        return Ok(bookings);
    }

    // PUT: api/bookings/5/cancel
    [HttpPut("{id:int}/cancel")]
    [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var isAdmin = User.IsInRole(UserRoles.Admin);

        var booking = await _bookingService.CancelBookingAsync(
            id, userId, isAdmin);

        if (booking == null)
        {
            return NotFound(new { message = "Booking not found." });
        }

        return Ok(booking);
    }
}
