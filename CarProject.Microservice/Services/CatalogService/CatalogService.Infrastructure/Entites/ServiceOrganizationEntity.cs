
namespace CatalogService.Infrastructure.Entites
{
    public class ServiceOrganizationEntity
    {
        public Guid Id { get; set; }
        public Guid IdOrganization { get; set; }
        public Guid IdService { get; set; }
        public int Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty; 
    }
}
