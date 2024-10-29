namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IServiceOrgRepository
    {
        public Task Add(ServiceOrg serviceModel);
        public Task Delete(long id);

        public Task Update(long id, ServiceOrg serviceModel);
        public Task<List<ServiceOrg>> GetAll();

    }
}
