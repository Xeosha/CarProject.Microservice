using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CatalogService.Infrastructure.Repositories.SQLQueries
{
    public class SQLServiceRepository : IServiceRepository
    {
        private readonly CatalogServiceDbContext _context;

        public SQLServiceRepository(CatalogServiceDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceModel serviceModel)
        {
            var sql = """INSERT INTO public."Services" ("Name", "Description") VALUES (@Name, @Description) """;
            var parameters = new[]
            {
                new NpgsqlParameter("@Name", serviceModel.Name),
                new NpgsqlParameter("@Description", serviceModel.Description)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task Delete(Guid id)
        {
            var sql = """DELETE FROM public.\"Services\" WHERE "Id" = @Id """;
            var parameters = new[]
            {
                new NpgsqlParameter("@Id", id)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task Update(Guid id, ServiceModel serviceModel)
        {
            var sql = """UPDATE public."Services" SET "Name" = @Name, "Description" = @Description WHERE "Id" = @Id """;
            var parameters = new[]
            {
                new NpgsqlParameter("@Name", serviceModel.Name),
                new NpgsqlParameter("@Description", serviceModel.Description),
                new NpgsqlParameter("@Id", id)
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<List<ServiceModel>> GetAll()
        {
            var sql = """SELECT "Id", "Name", "Description" FROM public."Services" """;

            return await _context.Services
                .FromSqlRaw(sql)
                .Select(e => new ServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                })
                .ToListAsync();
        }

        public async Task<ServiceModel> GetById(Guid id)
        {
            var sql = """SELECT "Id", "Name", "Description" FROM public."Services" WHERE "Id" = @Id""";
            var parameters = new[]
            {
                new NpgsqlParameter("@Id", id)
            };

            var entity = await _context.Services
                .FromSqlRaw(sql, parameters)
                .FirstOrDefaultAsync();

            if (entity == null) return null;

            return new ServiceModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }
    }
}
== null) return null;

                return new ServiceModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                };
            }
        }
    }

}
