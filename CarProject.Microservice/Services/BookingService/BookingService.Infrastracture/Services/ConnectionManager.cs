using BookingService.Domain.Interfaces;
using System.Collections.Concurrent;

namespace BookingService.Infrastracture.Services
{
    public class ConnectionManager : IConnectionManager
    {
        private static ConcurrentDictionary<string, List<string>> _userConnections = new();
        private static ConcurrentDictionary<string, List<string>> _organizationConnections = new();

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

        // Метод для подключения организации, поддерживает несколько соединений на одну организацию
        public Task ConnectOrganization(string organizationId, string connectionId)
        {
            var connections = _organizationConnections.GetOrAdd(organizationId, _ => new List<string>());

            lock (connections)
            {
                if (!connections.Contains(connectionId))
                {
                    connections.Add(connectionId);
                }
            }
            return Task.CompletedTask;
        }

        // Метод для отключения организации, удаляет соединение и организацию при отсутствии соединений
        public Task DisconnectOrganization(string organizationId, string connectionId)
        {
            if (_organizationConnections.TryGetValue(organizationId, out var connections))
            {
                lock (connections)
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        _organizationConnections.TryRemove(organizationId, out _);
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

        // Получить все соединения организации
        public List<string> GetOrganizationConnections(string organizationId)
        {
            return _organizationConnections.TryGetValue(organizationId, out var connections) ? connections : new List<string>();
        }
    }
}
