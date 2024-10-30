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

        [HttpPost("createService")]
        public async Task CreateService(Guid orgId, Guid serviceId, int Price, string Description)
        { 
            await _catalogServices.AddServiceToOrg(orgId, serviceId, Price, Description); 
        }
    }
}
