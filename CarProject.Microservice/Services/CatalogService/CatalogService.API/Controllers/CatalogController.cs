
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using ShareDTO;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogServices _catalogServices;
        public CatalogController(ICatalogServices catalogServices) 
        {
            _catalogServices = catalogServices;
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
            return await _catalogServices.GetWorkingHours(organizationServiceId);
        }
    }
}
