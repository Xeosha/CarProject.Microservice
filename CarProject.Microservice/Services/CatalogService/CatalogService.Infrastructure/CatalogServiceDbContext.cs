using CatalogService.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure
{
    public class CatalogServiceDbContext : DbContext
    {
        public CatalogServiceDbContext(DbContextOptions<CatalogServiceDbContext> options)
           : base(options)
        {
        }

        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<ServiceOrganizationEntity> ServiceOrganizations { get; set; }
        public DbSet<ServiceOrgDailyEntity> ServiceOrgDailys { get; set; }
        public DbSet<DailyWorkingHoursEntity> DailyWorkingHours { get; set; }
        public DbSet<TimeOfDailyEntity> TimeOfDailys { get; set; }
    }
}
