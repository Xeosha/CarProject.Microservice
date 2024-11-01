using BookingService.Domain.Interfaces;
using ShareDTO;
using System.Net.Http.Json;

namespace BookingService.Application.Services
{
    // По сути нужно перенести в слой Infrastructure как внешний сервис
    public class CatalogServiceClient : ICatalogServiceClient
    {
        private readonly HttpClient _httpClient;

        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId)
        {
            var response = await _httpClient.GetAsync($"/api/Catalog/getWorkingHours?organizationServiceId={organizationServiceId}");

            if (!response.IsSuccessStatusCode)
            {
                // Обработка ошибки
                throw new Exception("Не удалось получить рабочие часы из CatalogService");
            }

            var workingHours = await response.Content.ReadFromJsonAsync<List<WorkingHoursDto>>();
            return workingHours;
        }

        public async Task<List<Guid>> GetServiceOrgIdsForOrganization(Guid organizationId)
        {
            // Запрос к CatalogService, чтобы получить список ServiceOrgId для данной организации
            var response = await _httpClient.GetAsync($"/api/Organization/getServiceOrgIds?organizationId={organizationId}");

            if (!response.IsSuccessStatusCode)
            {
                // Обработка ошибки
                throw new Exception("Не удалось получить ServiceOrgId из CatalogService");
            }

            var serviceOrgIds = await response.Content.ReadFromJsonAsync<List<Guid>>();
            return serviceOrgIds ?? new List<Guid>();
        }
    }
}
