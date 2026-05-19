using System.Collections.Concurrent;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace CinemaBookingSystem.Hubs
{
    public class SeatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> LockedSeats = new();

        public async Task JoinShowtime(string showtimeId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, showtimeId);

            if(LockedSeats.TryGetValue(showtimeId, out var seatsForShowtime))
            {
                await Clients.Caller.SendAsync("LoadLockedSeats", seatsForShowtime.Keys.ToList());
            }
        }

        public async Task LockSeat(string showtimeId, string seatId)
        {
            var seatsForShowtime = LockedSeats.GetOrAdd(showtimeId, _ => new ConcurrentDictionary<string, string>());

            if(seatsForShowtime.TryAdd(seatId, Context.ConnectionId))
            {
                await Clients.OthersInGroup(showtimeId).SendAsync("SeatLocked", seatId);
            }
        }

        public async Task UnLockSeat(string showtimeId, string seatId)
        {
            if (LockedSeats.TryGetValue(showtimeId, out var seatsForShowtime))
            {
                if (seatsForShowtime.TryGetValue(seatId, out var ownerId) && ownerId == Context.ConnectionId)
                {
                    seatsForShowtime.TryRemove(seatId, out _);
                    await Clients.OthersInGroup(showtimeId).SendAsync("SeatUnlocked", seatId);
                }
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            foreach (var showtime in LockedSeats)
            {
                var showtimeId = showtime.Key;
                var seats = showtime.Value;

                var userSeats = seats.Where(s => s.Value == Context.ConnectionId).Select(s => s.Key).ToList();

                foreach (var seatId in userSeats)
                {
                    seats.TryRemove(seatId, out _);
                    await Clients.Group(showtimeId).SendAsync("SeatUnlocked", seatId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task MoveToCart(string showtimeId, List<string> seatIds)
        {
            if (LockedSeats.TryGetValue(showtimeId, out var seatsForShowtime))
            {
                foreach (var seatId in seatIds)
                {
                    if (seatsForShowtime.TryGetValue(seatId, out var ownerId) && ownerId == Context.ConnectionId)
                    {
                        seatsForShowtime[seatId] = "InCart";
                    }
                }
            }
        }

    }
}
