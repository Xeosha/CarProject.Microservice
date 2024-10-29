using Microsoft.AspNetCore.SignalR;

namespace BookingService.API.Providers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Получаем userId из заголовков запроса
             return connection.GetHttpContext()?.Request.Query["userId"].ToString();
        }
    }
}
