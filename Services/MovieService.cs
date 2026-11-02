using MovieTicketBookingWebAPI.DTOs.Movies;
using MovieTicketBookingWebAPI.Models;
using MovieTicketBookingWebAPI.Repositories.Interfaces;
using MovieTicketBookingWebAPI.Services.Interfaces;
using MovieTicketBookingWebAPI.DTOs.Common;


namespace MovieTicketBookingWebAPI.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieResponseDto> CreateMovieAsync(CreateMovieDto movieDto)
    {
        var movie = new Movie
        {
            Title = movieDto.Title,
            Description = movieDto.Description,
            DurationMinutes = movieDto.DurationMinutes,
            ReleaseDate = movieDto.ReleaseDate,
            Language = movieDto.Language,
            Genre = movieDto.Genre
        };

        var createdMovie = await _movieRepository.AddAsync(movie);

        return MapToResponseDto(createdMovie);
    }

    public async Task<PagedResponseDto<MovieResponseDto>> GetAllMoviesAsync(
    PaginationParams paginationParams)
    {
        var pagedMovies = await _movieRepository.GetAllAsync(paginationParams);

        return new PagedResponseDto<MovieResponseDto>
        {
            Data = pagedMovies.Data.Select(movie => new MovieResponseDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate,
                Language = movie.Language,
                Genre = movie.Genre
            }).ToList(),

            PageNumber = pagedMovies.PageNumber,
            PageSize = pagedMovies.PageSize,
            TotalRecords = pagedMovies.TotalRecords,
            TotalPages = pagedMovies.TotalPages
        };
    }

    public async Task<MovieResponseDto?> GetMovieByIdAsync(int movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        return movie == null ? null : MapToResponseDto(movie);
    }

    public async Task<MovieResponseDto?> UpdateMovieAsync(
        int movieId,
        UpdateMovieDto movieDto)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        if (movie == null)
            return null;

        movie.Title = movieDto.Title;
        movie.Description = movieDto.Description;
        movie.DurationMinutes = movieDto.DurationMinutes;
        movie.ReleaseDate = movieDto.ReleaseDate;
        movie.Language = movieDto.Language;
        movie.Genre = movieDto.Genre;

        await _movieRepository.UpdateAsync(movie);

        return MapToResponseDto(movie);
    }

    public async Task<bool> DeleteMovieAsync(int movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        if (movie == null)
            return false;

        await _movieRepository.DeleteAsync(movie);

        return true;
    }

    public async Task<List<MovieResponseDto>> SearchMoviesByTitleAsync(string title)
    {
        var movies = await _movieRepository.SearchByTitleAsync(title);

        return movies.Select(MapToResponseDto).ToList();
    }

    private static MovieResponseDto MapToResponseDto(Movie movie)
    {
        return new MovieResponseDto
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Description = movie.Description,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            Language = movie.Language,
            Genre = movie.Genre
        };
    }
}