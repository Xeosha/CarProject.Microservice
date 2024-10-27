

namespace BookingService.Domain.Interfaces
{
    public interface IConnectionManager
    {
        Task ConnectUser(string userId, string connectionId);
        Task DisconnectUser(string userId, string connectionId);
        Task ConnectOrganization(string organizationId, string connectionId);
        Task DisconnectOrganization(string organizationId, string connectionId);
        List<string> GetUserConnections(string userId);
        List<string> GetOrganizationConnections(string organizationId);
    }
}
