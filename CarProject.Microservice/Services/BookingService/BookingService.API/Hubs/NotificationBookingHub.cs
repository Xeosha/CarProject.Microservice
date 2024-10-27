using BookingService.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace BookingService.API.Hubs
{
    public interface IBookingClient
    {
        Task NotifyOrganization(string service);

        Task NotifyUser(bool isConfirmed);
    }

    public class NotificationBookingHub : Hub<IBookingClient>
    {
        // Словарь для хранения соответствия между userId и connectionId
        private static ConcurrentDictionary<string, string> _connections = new ConcurrentDictionary<string, string>();
        private readonly IBookingService _bookingService;
        private readonly ILogger<NotificationBookingHub> _logger;

        public NotificationBookingHub(IBookingService bookingService, ILogger<NotificationBookingHub> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            // Получаем userId из параметров запроса при подключении
            var userId = Context.GetHttpContext().Request.Query["userId"];

            // Сохраняем соответствие между userId и connectionId
            if (!string.IsNullOrEmpty(userId))
            {
                // Сохраняем соответствие между userId и connectionId
                _connections[userId] = Context.ConnectionId;
                _logger.LogInformation("соединение установлено: " + Context.ConnectionId);
            } else
            {
                _logger.LogInformation("соединение не установлено");
            }

            

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Удаляем соединение из словаря при отключении
            var userId = Context.GetHttpContext().Request.Query["userId"];
            _connections.TryRemove(userId, out _);

            _logger.LogInformation("соединение разорвано: " + Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task RequestBooking(int organizationId, int userId, string service)
        {
            

            _logger.LogInformation("RequestBooking");

            try
            {

                await _bookingService.CreateBooking(1, userId, organizationId, service);
                if (_connections.TryGetValue(organizationId.ToString(), out var connectionId))
                {
                    await Clients.Client(connectionId).NotifyOrganization(service);

                    _logger.LogInformation("connectionId " + Context.ConnectionId);
                } else
                {
                    _logger.LogInformation("Не смог достать");
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                _logger.LogError(ex, "Failed to create booking for user {UserId}", userId);
                throw; // Пробрасываем исключение, чтобы SignalR мог обработать его
            }
        }

        // Метод для подтверждения или отклонения бронирования менеджером
        public async Task ConfirmBooking(int userId, int bookingId, bool isConfirmed)
        {
            _logger.LogInformation("ConfirmBooking ");

            try
            {
                var booking = await _bookingService.ConfirmBooking(bookingId, isConfirmed);
                if (_connections.TryGetValue(userId.ToString(), out var connectionId))
                {
                    await Clients.Client(connectionId).NotifyUser(isConfirmed);

                    _logger.LogInformation("connectionId " + Context.ConnectionId);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                _logger.LogError(ex, "Failed to create booking for user {UserId}", userId);
                throw; // Пробрасываем исключение, чтобы SignalR мог обработать его
            }
            
        }




    }
}
