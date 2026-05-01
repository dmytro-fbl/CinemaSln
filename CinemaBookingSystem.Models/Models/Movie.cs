using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBookingSystem.Models.Models
{
    public class Movie
    {
        public long MovieID { get; set; }

        [Required(ErrorMessage ="Будь ласка, введіть назву фільму")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть опис фільму")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, вкажіть жанр")]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Будь ласка, введіть додатню ціну фільму")]
        [Column (TypeName ="decimal(8, 2)")]
        [Display(Name = "Будь ласка, введіть коректну ціну фільму")]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage ="Будь ласка, вкажіть дату виходу")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        public List<Showtime> Showtimes { get; set; } = new List<Showtime> ();
    }
}
