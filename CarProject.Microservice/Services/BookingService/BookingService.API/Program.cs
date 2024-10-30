using BookingService.API.Hubs;
using BookingService.API.Providers;
using BookingService.Application.Services;
using BookingService.Domain.Interfaces;
using BookingService.Infrastracture;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();


builder.Services.AddScoped<IBookingService, BookingsService>();

// подключение к биде, конектион строки хранятся в docker-compose environment (переменные среды)
builder.Services.AddDbContext<BookingServiceDbContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("BookingServiceDbContext")
                               ?? Environment.GetEnvironmentVariable("ConnectionStrings__BookingServiceDbContext");

        options.UseNpgsql(connectionString);
    });

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

// ссылка на микросервис
builder.Services.AddHttpClient<ICatalogServiceClient, CatalogServiceClient>(client =>
{
    client.BaseAddress = new Uri("https://catalogservice.api:6061/api/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationBookingHub>("/notificationHub"); // Настройка маршрута для BookingHub

// Код для выполнения миграции в бд
// если хочешь создать файл миграции:
// dotnet ef migrations add Init -s Services/BookingService/BookingService.API/ -p Services/BookingService/BookingService.Infrastracture/
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookingServiceDbContext>();
    // Обновляем базу данных, если есть миграции
    dbContext.Database.Migrate();
}

app.Run();
