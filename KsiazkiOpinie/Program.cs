using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KsiazkiOpinie.Data;
var builder = WebApplication.CreateBuilder(args);

// Dodanie kontekstu bazy danych
builder.Services.AddDbContext<BookAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'RecipeAppContext' not found.")));

// Dodanie us?ug do kontenera
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfiguracja potoku ??da? HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Domy?lna warto?? HSTS to 30 dni. Mo?esz to zmieni? dla scenariuszy produkcyjnych, zobacz https://aka.ms/aspnetcore-hsts.
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