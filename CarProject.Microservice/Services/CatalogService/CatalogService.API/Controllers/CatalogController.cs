
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Models.Dto;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<List<ServiceOrg>> GetServices()
        {
            return await _catalogServices.GetAllServices();
        }

        [HttpGet("getWorkingHours")]
        public async Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId)
        {
            return await _catalogServices.GetWorkingHours(organizationServiceId);
        }
    }
}
