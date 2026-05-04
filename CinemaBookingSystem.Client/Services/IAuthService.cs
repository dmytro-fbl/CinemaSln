using CinemaBookingSystem.Models.ViewModels;

namespace CinemaBookingSystem.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginModel loginModel);
        Task<bool> Register(RegisterModel registerModel);
        Task Logout();
    }
}