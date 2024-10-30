using BookingService.Domain.Interfaces;
using BookingService.Domain.Models.Dto;
using System.Net.Http.Json;

namespace BookingService.Application.Services
{
    public class CatalogServiceClient : ICatalogServiceClient
    {
        private readonly HttpClient _httpClient;

        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WorkingHoursDto> GetWorkingHours(Guid organizationServiceId)
        {
            var response = await _httpClient.GetAsync($"/workingHours/{organizationServiceId}");

            if (!response.IsSuccessStatusCode)
            {
                // Обработка ошибки
                throw new Exception("Не удалось получить рабочие часы из CatalogService");
            }

            var workingHours = await response.Content.ReadFromJsonAsync<WorkingHoursDto>();
            return workingHours!;
        }
    }
}
