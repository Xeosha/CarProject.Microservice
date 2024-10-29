using BookingService.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BookingService.API.Hubs
{
    public interface IBookingClient
    {
        Task NotifyOrganization(int userId, string service);
        Task NotifyUser(bool isConfirmed);
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

        public async Task RequestBooking(int organizationId, string service)
        {
            var userId = Context.UserIdentifier;

            _logger.LogInformation("RequestBooking");

            try
            {
                await _bookingService.CreateBooking(1, Int32.Parse(userId), organizationId, service);
                await Clients.User(organizationId.ToString()).NotifyOrganization(Int32.Parse(userId), service);
                _logger.LogInformation("Organization notified on multiple connections");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create booking for user {UserId}", userId);
                throw;
            }
        }

        public async Task ConfirmBooking(int userId, int bookingId, bool isConfirmed)
        {
            _logger.LogInformation("ConfirmBooking for: " + userId);

            try
            {
                var booking = await _bookingService.ConfirmBooking(bookingId, isConfirmed);
                await Clients.User(userId.ToString()).NotifyUser(isConfirmed);
                _logger.LogInformation("User notified.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to confirm booking for user {UserId}", userId);
                throw;
            }
        }

    }
}
