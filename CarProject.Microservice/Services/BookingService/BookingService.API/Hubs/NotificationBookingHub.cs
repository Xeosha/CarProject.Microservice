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
        private readonly IConnectionManager _connectionManager;
        private readonly ILogger<NotificationBookingHub> _logger;

        public NotificationBookingHub(IBookingService bookingService, IConnectionManager connectionManager, ILogger<NotificationBookingHub> logger)
        {
            _bookingService = bookingService;
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            _logger.LogInformation("userId: " + userId);
            var organizationId = Context.GetHttpContext().Request.Query["organizationId"];

            if (!string.IsNullOrEmpty(userId))
            {
                await _connectionManager.ConnectUser(userId, Context.ConnectionId);
                _logger.LogInformation("User connection established: " + Context.ConnectionId);

                _logger.LogInformation($"User {userId} has connections: {string.Join(", ", _connectionManager.GetUserConnections(userId))}");
            }

            if (!string.IsNullOrEmpty(organizationId))
            {
                await _connectionManager.ConnectOrganization(organizationId, Context.ConnectionId);
                _logger.LogInformation("Organization connection established: " + Context.ConnectionId);

                _logger.LogInformation($"Organization {organizationId} has connections: {string.Join(", ", _connectionManager.GetOrganizationConnections(organizationId))}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            var organizationId = Context.GetHttpContext().Request.Query["organizationId"];

            if (!string.IsNullOrEmpty(userId))
            {
                await _connectionManager.DisconnectUser(userId, Context.ConnectionId);
                _logger.LogInformation("User connection removed: " + Context.ConnectionId);       
            }

            if (!string.IsNullOrEmpty(organizationId))
            {
                await _connectionManager.DisconnectOrganization(organizationId, Context.ConnectionId);
                _logger.LogInformation("Organization connection removed: " + Context.ConnectionId);  
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task RequestBooking(int organizationId, int userId, string service)
        {
            _logger.LogInformation("RequestBooking");

            try
            {
                await _bookingService.CreateBooking(1, userId, organizationId, service);

                var organizationConnections = _connectionManager.GetOrganizationConnections(organizationId.ToString());
                if (organizationConnections != null)
                {
                    foreach (var connectionId in organizationConnections)
                    {
                        await Clients.Client(connectionId).NotifyOrganization(userId, service);
                    }
                    _logger.LogInformation("Organization notified on multiple connections");
                }
                else
                {
                    _logger.LogInformation("Organization connection not found.");
                }
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

                var userConnections = _connectionManager.GetUserConnections(userId.ToString());

                if (userConnections != null)
                {
                    foreach (var connectionId in userConnections)
                    {
                        await Clients.Client(connectionId).NotifyUser(isConfirmed);
                    }
                    _logger.LogInformation("User notified on multiple connections");
                }
                else
                {
                    _logger.LogInformation("User connection not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to confirm booking for user {UserId}", userId);
                throw;
            }
        }

    }
}
