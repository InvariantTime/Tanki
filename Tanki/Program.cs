using Microsoft.EntityFrameworkCore;
using Tanki.Persistence;
using Tanki;
using Tanki.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSignalR();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(db =>
{
    db.UseSqlServer(connectionString);
});

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseSession();

app.UseCors();

app.MapControllers();
app.MapHub<RoomHub>("/ws/rooms");


app.Run();