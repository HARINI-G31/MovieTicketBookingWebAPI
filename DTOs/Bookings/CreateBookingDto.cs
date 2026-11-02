
namespace MovieTicketBookingWebAPI.DTOs.Bookings;

public class CreateBookingDto
{
    public int ShowId { get; set; }

    public string CustomerPhone { get; set; } = string.Empty;

    public List<int> SeatIds { get; set; } = new();
}
