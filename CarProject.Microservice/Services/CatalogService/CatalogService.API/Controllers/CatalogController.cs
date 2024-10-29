
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
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
    }
}
