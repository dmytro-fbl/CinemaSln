namespace CinemaBookingSystem.Models.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Movie Movie { get; set; } = new();
        public int Quantity { get; set; }
    }
}
