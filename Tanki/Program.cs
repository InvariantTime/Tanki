using Microsoft.EntityFrameworkCore;
using Tanki.Persistence;
using Tanki;
using Tanki.Binders;
using Tanki.Infrastructure.Authentication;
using Tanki.Infrastructure.Hubs;

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

builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.RegisterAuthorization();

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(op =>
{
    op.ModelBinderProviders.Insert(0, new CustomBinderProvider());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(db =>
{
    db.UseSqlServer(connectionString);
});

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<RoomHub>("/ws/rooms");

app.MapHub<GameServer>("/ws/session")
    .RequireAuthorization(op => op.AddRequirements(
        new RoomAccessRequirement("sessionId")));

app.Run();