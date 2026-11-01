namespace MovieTicketBookingWebAPI.DTOs.Movies;

public class UpdateMovieDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
}