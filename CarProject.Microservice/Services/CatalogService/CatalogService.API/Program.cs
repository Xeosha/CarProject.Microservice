using CatalogService.Application.Servicess;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure;
using CatalogService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����������� � ����, ��������� ������ �������� � docker-compose environment (���������� �����)
builder.Services.AddDbContext<CatalogServiceDbContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("CatalogServiceDbContext")
                               ?? Environment.GetEnvironmentVariable("ConnectionStrings__CatalogServiceDbContext"); 

        options.UseNpgsql(connectionString);
    });

// ��� ���� DI, ���� �� ������� � ����� � ������
builder.Services.AddScoped<IOrganizationsRepository, OrganizationRepository>();
builder.Services.AddScoped<IServiceOrgRepository, ServiceOrgRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IDailyWorkingHoursRepository, DailyWorkingHoursRepository>();
builder.Services.AddTransient<ICatalogServices, CatalogServices>();

var app = builder.Build();

// ������� 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// ��� ��� ���������� �������� � ��
// ���� ������ ������� ���� ��������:
// dotnet ef migrations add Init -s Services/CatalogService/CatalogService.API/ -p Services/CatalogService/CatalogService.Infrastructure/
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogServiceDbContext>();
    // ��������� ���� ������, ���� ���� ��������
    dbContext.Database.Migrate();
}



app.Run();
