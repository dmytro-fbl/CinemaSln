using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Будь ласка, введіть ім'я користувача")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, підтвердіть пароль")]
        [DataType(DataType.Password)]
        [Compare("Pasword", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
