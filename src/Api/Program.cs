using System.Reflection;
using Api.Middleware;
using Api.Security;
using Infrastructure;               // << เพิ่มบรรทัดนี้เพื่อเรียก AddInfrastructure(...)
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddInfrastructure(cfg);  // << แทนที่ AddDbContext แบบเดิม

builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblies(
    typeof(Application.Goals.Commands.CreateGoal.CreateGoalCommand).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<DevUserHeaderMiddleware>();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapControllers();
app.Run();
