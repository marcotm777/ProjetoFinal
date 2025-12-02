using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Data;
using University.Application.Interfaces;
using University.Application.Services;
using University.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.RegisterMapster();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAlunoAppService, AlunoAppService>();
builder.Services.AddScoped<ICursoAppService, CursoAppService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
