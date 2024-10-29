

namespace BookingService.Domain.Interfaces
{
    public interface IConnectionManager
    {
        Task ConnectUser(string userId, string connectionId);
        Task DisconnectUser(string userId, string connectionId);
        List<string> GetUserConnections(string userId);
    }
}
