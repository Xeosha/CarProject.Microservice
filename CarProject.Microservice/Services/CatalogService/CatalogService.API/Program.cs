using CatalogService.Infrastructure;
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


// ��� ��� ���������� �������� � ��
// ���� ������ ������� ���� ��������:
// dotnet ef migrations add <�������� ��� ������� ����> -s src/TelegramBot.API/ -p src/TelegramBot.Data/
using (var scope = app.Services.CreateScope())
{   
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogServiceDbContext>();
    // ��������� ���� ������, ���� ���� ��������
    dbContext.Database.Migrate();
}



app.Run();
