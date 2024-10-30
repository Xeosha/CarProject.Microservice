

using CatalogService.Domain.Interfaces.Models.Dto;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IDailyWorkingHoursRepository
    {
        public Task<List<WorkingHoursDto>> GetAllTimes(Guid serviceOrgId);
    }
}
