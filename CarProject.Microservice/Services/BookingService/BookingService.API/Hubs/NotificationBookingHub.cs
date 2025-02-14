using BookingService.Domain.Interfaces;
using BookingService.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace BookingService.API.Hubs
{
    public interface IBookingClient
    {
        Task Notify(Booking booking);
    }

    public class NotificationBookingHub : Hub<IBookingClient>
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<NotificationBookingHub> _logger;

        public NotificationBookingHub(IBookingService bookingService, ILogger<NotificationBookingHub> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }
            
        public override async Task OnConnectedAsync()
        {

            var userId = Context.UserIdentifier;

            _logger.LogInformation("userId: " + userId);

            if (!string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation("User connection established: " + Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;

            if (!string.IsNullOrEmpty(userId))
            {
                _logger.LogInformation("User connection removed: " + Context.ConnectionId);       
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task RequestBooking(string orgId, string serviceOrganizationId, DateTime bookingTime, string notes)
        {
            var userId = Context.UserIdentifier;

            _logger.LogInformation("RequestBooking");

            try
            {
                var booking = await _bookingService.CreateBooking(Guid.Parse(userId), Guid.Parse(serviceOrganizationId), bookingTime, notes);

                await Clients.User(orgId).Notify(booking);
                await Clients.User(userId).Notify(booking);

                _logger.LogInformation($"Organization notified {booking.BookingStatus}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create booking for user {UserId}", userId);

                throw;
            }
        }
        public async Task ConfirmBooking(string bookingId, bool isConfirmed)
        {

            _logger.LogInformation("ConfirmBooking for");

            try
            {
                var booking = await _bookingService.ConfirmBooking(Guid.Parse(bookingId), isConfirmed);

                await Clients.User(booking.UserId.ToString()).Notify(booking);
                _logger.LogInformation($"User notified {booking.BookingStatus}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to confirm booking");

                throw;
            }
        }

    }
}
