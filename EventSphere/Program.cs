using EventSphere.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Avoid circular reference issues in JSON
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EventSphere API",
        Version = "v1"
    });
});

// SQL Server database context
builder.Services.AddDbContext<EventSphereDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:8080",  // Vite / React dev port
                "http://localhost:8081",  // (Opsiyonel) başka bir frontend portu varsa ekle
                "http://localhost:5173"   // (Opsiyonel) Vite default port
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Eğer cookie/JWT ile giriş varsa bu önemli
    });
});

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventSphere API V1");
    c.RoutePrefix = string.Empty; // Swagger at root
});

// ✅ Enable CORS
app.UseCors("AllowFrontend");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
    RequestPath = "/uploads"
});


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
