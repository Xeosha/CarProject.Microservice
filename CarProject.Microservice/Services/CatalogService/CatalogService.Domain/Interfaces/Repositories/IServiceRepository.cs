using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        public Task Add(ServiceModel serviceModel);
        public Task Delete(long id);

        public Task Update(long id, ServiceModel serviceModel);
        public Task<List<ServiceModel>> GetAll();




    }
}
