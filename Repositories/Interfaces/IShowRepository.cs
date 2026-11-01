using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Repositories.Interfaces;

public interface IShowRepository
{
    Task<Show> AddAsync(Show show);

    Task<List<Show>> GetAllAsync(
        DateTime? date,
        int? movieId,
        int? theatreId);

    Task<Show?> GetByIdAsync(int showId);

    Task<List<Show>> GetByMovieIdAsync(int movieId);

    Task<List<Show>> GetByTheatreIdAsync(int theatreId);

    Task<List<Show>> GetByDateAsync(DateTime date);
    Task<List<Show>> GetByScreenIdAsync(int screenId);

    Task UpdateAsync(Show show);

    Task DeleteAsync(Show show);
}