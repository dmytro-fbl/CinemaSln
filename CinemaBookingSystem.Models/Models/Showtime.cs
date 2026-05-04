using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models.Models
{
    public class Showtime
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть дату виходу")]
        public DateTime SessionTime { get; set; }

        [Required(ErrorMessage = "Будь ласка, вкажіть номер залу")]
        [Range(1, 10, ErrorMessage ="Номер залу має бути від 1 до 10" )]
        public int HallNumber { get; set; }

        [Required(ErrorMessage = "Сеанс повинен належати певному фільму")]
        public long MovieId { get; set; }

        public Movie Movie { get; set; } = null!;
    }
}
