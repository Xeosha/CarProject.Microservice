using BookingService.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;

namespace BookingService.API.Hubs
{
    public interface IBookingClient
    {
        Task NotifyBookingStatus(Booking Booking);
    }

    public class NotificationBookingHub : Hub<IBookingClient>
    {
        private static ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();

        public async Task JoinBookingGroup(Booking connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.BookingId.ToString());

            var stringConnection = JsonSerializer.Serialize(connection);

            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

            await Clients
                .Group(connection.BookingId.ToString())
                .NotifyBookingStatus(connection);
        }

        public async Task Notificate(Booking booking)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);

            var connection = JsonSerializer.Deserialize<Booking>(stringConnection);

            if (connection is not null)
            {
                await Clients
                    .Group(connection.BookingId.ToString())
                    .NotifyBookingStatus(connection);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<Booking>(stringConnection);

            if (connection is not null)
            {
                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.BookingId.ToString());

                await Clients
                    .Group(connection.BookingId.ToString())
                    .NotifyBookingStatus(connection);
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
