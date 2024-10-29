using BookingService.Domain.Interfaces;
using System.Collections.Concurrent;

namespace BookingService.Infrastracture.Services
{
    public class ConnectionManager : IConnectionManager
    {
        private static ConcurrentDictionary<string, List<string>> _userConnections = new();

        // Метод для подключения пользователя, поддерживает несколько соединений на одного пользователя
        public Task ConnectUser(string userId, string connectionId)
        {
            var connections = _userConnections.GetOrAdd(userId, _ => new List<string>());

            lock (connections)
            {
                if (!connections.Contains(connectionId))
                {
                    connections.Add(connectionId);
                }
            }
            return Task.CompletedTask;
        }

        // Метод для отключения пользователя, удаляет соединение и пользователя при отсутствии соединений
        public Task DisconnectUser(string userId, string connectionId)
        {
            if (_userConnections.TryGetValue(userId, out var connections))
            {
                lock (connections)
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        _userConnections.TryRemove(userId, out _);
                    }
                }
            }
            return Task.CompletedTask;
        }
        // Получить все соединения пользователя
        public List<string> GetUserConnections(string userId)
        {
            return _userConnections.TryGetValue(userId, out var connections) ? connections : new List<string>();
        }
    }
}
