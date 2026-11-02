
using MovieTicketBookingWebAPI.DTOs.Auth;
using MovieTicketBookingWebAPI.Models;

namespace MovieTicketBookingWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto dto);

        Task<string?> LoginAsync(LoginDto dto);
    }
}
