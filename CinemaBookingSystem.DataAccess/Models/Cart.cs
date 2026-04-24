namespace CinemaBookingSystem.DataAccess.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Movie movie, int quantity)
        {
            CartLine? line = Lines
                .Where(p => p.Movie.MovieID == movie.MovieID)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Movie = movie,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(Movie movie) =>
            Lines.RemoveAll(l => l.Movie.MovieID ==  movie.MovieID);

        public decimal ComputeTotalValue() => 
            Lines.Sum(e => e.Movie.TicketPrice * e.Quantity);

        public virtual void Clear() => Lines.Clear(); 
    }
}
