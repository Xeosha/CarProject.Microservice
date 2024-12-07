
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using ShareDTO;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogServices _catalogServices;
        private readonly IScheduleService _scheduleService;
        public CatalogController(ICatalogServices catalogServices, IScheduleService scheduleService) 
        {
            _catalogServices = catalogServices;
            _scheduleService = scheduleService; 
        }


        [HttpGet("getServices")]    
        public async Task<List<ServiceOrgDto>> GetServices()
        {
            return await _catalogServices.GetAllServices();
        }
        [HttpGet("getNameServices")]
        public async Task<List<ServiceModel>> GetNameServices()
        {
            return await _catalogServices.GetNameServices();
        }
        [HttpGet("getWorkingHours")]
        public async Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId)
        {
            return await _scheduleService.GetWorkingHours(organizationServiceId);
        }
    }
}
