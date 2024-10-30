

namespace CatalogService.Infrastructure.Entites
{
    public class OrganizationEntity
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
