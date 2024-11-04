using CatalogService.Domain.DTOs;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly ICatalogServices _catalogServices;
        public OrganizationController(ICatalogServices catalogServices)
        {
            _catalogServices = catalogServices;
        }

        /// <summary>
        /// Создать новую услугу организации
        /// </summary>
        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto dto)
        {
            await _catalogServices.AddServiceToOrg(dto.OrgId, dto.ServiceId, dto.Price, dto.Description);
            return Ok();
        }

        /// <summary>
        /// Обновить услугу организации
        /// </summary>
        [HttpPost("updateService")]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceDto dto)
        {
            await _catalogServices.UpdateService(dto.ServiceOrgId, dto.OrgId, dto.ServiceId, dto.Price, dto.Description);
            return Ok();
        }

        /// <summary>
        /// Удалить услугу организации
        /// </summary>
        /// <param name="serviceOrgId">айди услуги</param>
        /// <returns></returns>
        [HttpPost("deleteService")]
        public async Task<IActionResult> DeleteService([FromBody] Guid serviceOrgId)
        {
            await _catalogServices.DeleteService(serviceOrgId);
            return Ok();
        }

        /// <summary>
        /// Получение услуг организации
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpGet("showServices/{orgId}")]
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
