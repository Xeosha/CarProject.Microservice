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

builder.Services.AddDbContext<CatalogServiceDbContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("CatalogServiceDbContext")
                               ?? Environment.GetEnvironmentVariable("ConnectionStrings__CatalogServiceDbContext"); 

        options.UseNpgsql(connectionString);
    });

builder.Services.AddScoped<IOrganizationsRepository, OrganizationRepository>();
builder.Services.AddScoped<IServiceOrgRepository, ServiceOrgRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<ICatalogServices, CatalogServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// Код для выполнения миграции в бд
// если хочешь создать файл миграции:
// dotnet ef migrations add <Название без кавычек этих> -s src/TelegramBot.API/ -p src/TelegramBot.Data/
using (var scope = app.Services.CreateScope())
{   
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogServiceDbContext>();
    // Обновляем базу данных, если есть миграции
    dbContext.Database.Migrate();
}



app.Run();
