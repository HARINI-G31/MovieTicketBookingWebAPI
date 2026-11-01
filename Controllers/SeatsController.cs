
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingWebAPI.DTOs.Seats;
using MovieTicketBookingWebAPI.Services.Interfaces;

namespace MovieTicketBookingWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatsController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeat(CreateSeatDto seatDto)
    {
        var seat = await _seatService.CreateSeatAsync(seatDto);

        return CreatedAtAction(
            nameof(GetSeatById),
            new { id = seat.SeatId },
            seat);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateSeatsBulk(
        List<CreateSeatDto> seatDtos)
    {
        var seats = await _seatService.CreateSeatsBulkAsync(seatDtos);

        return Ok(seats);
    }

    [HttpGet("screen/{screenId}")]
    public async Task<IActionResult> GetSeatsByScreenId(int screenId)
    {
        var seats = await _seatService.GetSeatsByScreenIdAsync(screenId);

        return Ok(seats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSeatById(int id)
    {
        var seat = await _seatService.GetSeatByIdAsync(id);

        if (seat == null)
            return NotFound(new { message = "Seat not found." });

        return Ok(seat);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSeat(
        int id,
        UpdateSeatDto seatDto)
    {
        var seat = await _seatService.UpdateSeatAsync(id, seatDto);

        if (seat == null)
            return NotFound(new { message = "Seat not found." });

        return Ok(seat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeat(int id)
    {
        var deleted = await _seatService.DeleteSeatAsync(id);

        if (!deleted)
            return NotFound(new { message = "Seat not found." });

        return NoContent();
    }
}
