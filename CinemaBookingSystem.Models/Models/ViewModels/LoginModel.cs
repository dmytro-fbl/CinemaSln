using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть ім'я користувача")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; } = "/";
    }
}
