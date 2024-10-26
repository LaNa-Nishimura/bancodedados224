using Microsoft.EntityFrameworkCore;
using EFDataBaseFirst.Models;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration["EFDataBaseFirst:ConnectionString"];

// Fazemos a configuração do DbContext com o banco de dados específico, neste caso o SQLServer
builder.Services.AddDbContext<MeuBlogContext>(o => o.UseSqlServer(connString));

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
