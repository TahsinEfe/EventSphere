using DayOff.Application.DependencyInjection;
using DayOff.Application.Features.DayOffTypes.Queries;
using DayOff.Application.Interfaces;
using DayOff.Application.Interfaces.Repositories;
using DayOff.Application.Interfaces.Services;
using DayOff.Infrastructure.Services;
using DayOff.Persistence.Context;
using DayOff.Persistence.Repositories;
using DayOff.Persistence.UnitOfWork;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);

// Oracle DbContext bağlantısı
builder.Services.AddDbContext<DayOffDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

// MediatR CQRS
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("DayOff.Application")));

// FluentValidation otomatik tarama
builder.Services.AddValidatorsFromAssembly(Assembly.Load("DayOff.Application"));

// AutoMapper otomatik tarama
builder.Services.AddAutoMapper(Assembly.Load("DayOff.Application"));


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("DayOff.Application")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDayOffRequestService, DayOffRequestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDayOffBalanceService, DayOffBalanceService>();
builder.Services.AddScoped<IDayOffTypeService, DayOffTypeService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IWeeklyDayOffStatService, WeeklyDayOffStatService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDayOffPolicyService, DayOffPolicyService>();
builder.Services.AddScoped<IStatsRepository, StatsRepository>();


builder.Services.AddApplicationServices();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
