using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController
    {
        private readonly ICatalogServices _catalogServices;
        public OrganizationController(ICatalogServices catalogServices)
        {
            _catalogServices = catalogServices;
        }

        /// <summary>
        /// Создать новую услугу организации
        /// </summary>
        /// <param name="orgId">айди организации</param>
        /// <param name="serviceId">айди услуги</param>
        /// <param name="Price">цена</param>
        /// <param name="Description">комментарий</param>
        /// <returns></returns>
        [HttpPost("createService")]
        public async Task CreateService(Guid orgId, Guid serviceId, int Price, string Description)
        { 
            await _catalogServices.AddServiceToOrg(orgId, serviceId, Price, Description); 
        }

        /// <summary>
        /// Обновить услугу организации
        /// </summary>
        /// <param name="serviceOrgId">айди услуги (с конкретной организацией)</param>
        /// <param name="orgId">айди организации</param>
        /// <param name="serviceId">айди услуги</param>
        /// <param name="Price">цена</param>
        /// <param name="Description">комментарий</param>
        /// <returns></returns>
        [HttpPost("updateService")]
        public async Task UpdateService(Guid serviceOrgId, Guid orgId, Guid serviceId, int Price, string Description)
        {
            await _catalogServices.UpdateService(serviceOrgId, orgId, serviceId, Price, Description);
        }

        /// <summary>
        /// Удалить услугу организации
        /// </summary>
        /// <param name="serviceOrgId">айди услуги</param>
        /// <returns></returns>
        [HttpPost("deleteService")]
        public async Task DeleteService(Guid serviceOrgId)
        {
            await _catalogServices.DeleteService(serviceOrgId);
        }

        /// <summary>
        /// Получение услуг организации
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpPost("showServices")]
        public async Task<List<ServiceOrgDto>> ShowServices(Guid orgId)
        {

            var serviceOrgs = await _catalogServices.GetAllServices();
            return serviceOrgs.Where(s => s.IdOrganization == orgId).ToList();
        }


        /// <summary>
        /// Для связи между микросервисами
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("getServiceOrgIds")]
        public async Task<List<Guid>> GetServiceOrgIds(Guid organizationId)
        {
            var serviceOrgs = await _catalogServices.GetOrgServices(organizationId);
            return serviceOrgs.Select(s => s.Id).ToList();
        }
    }
}
