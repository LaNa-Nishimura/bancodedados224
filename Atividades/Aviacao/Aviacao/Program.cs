using Microsoft.EntityFrameworkCore;
using Aviacao.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration["AVIACAO:ConnectionString"];

// Fazemos a configura��o do DbContext com o banco de dados espec�fico, neste caso o SQLServer
builder.Services.AddDbContext<AviacaoContext>(o => o.UseSqlServer(connString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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