using CatalogService.Application.Servicess;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure;
using CatalogService.Infrastructure.Repositories;
using CatalogService.Infrastructure.Repositories.SQLQueries;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(options =>
{
    //Determine base path for the application.
    var basePath = AppContext.BaseDirectory;

    //Set the comments path for the swagger json and ui.
    var xmlPath = Path.Combine(basePath, "CatalogService.API.xml");
    options.IncludeXmlComments(xmlPath);
});

// подключение к биде, конектион строки хранятся в docker-compose environment (переменные среды)
builder.Services.AddDbContext<CatalogServiceDbContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("CatalogServiceDbContext")
                               ?? Environment.GetEnvironmentVariable("ConnectionStrings__CatalogServiceDbContext"); 

        options.UseNpgsql(connectionString);
    });

// тут типа DI, надо бы вынести в слоях в классы
builder.Services.AddScoped<IOrganizationsRepository, OrganizationRepository>();
builder.Services.AddScoped<IServiceOrgRepository, ServiceOrgRepository>();
builder.Services.AddScoped<IServiceRepository, SQLServiceRepository>();
builder.Services.AddScoped<IDailyWorkingHoursRepository, DailyWorkingHoursRepository>();
builder.Services.AddTransient<ICatalogServices, CatalogServices>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// сваггер 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors();


app.UseAuthorization();

app.MapControllers();


// Код для выполнения миграции в бд
// если хочешь создать файл миграции:
// dotnet ef migrations add Init -s Services/CatalogService/CatalogService.API/ -p Services/CatalogService/CatalogService.Infrastructure/
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogServiceDbContext>();
    // Обновляем базу данных, если есть миграции
    dbContext.Database.Migrate();
}



app.Run();
