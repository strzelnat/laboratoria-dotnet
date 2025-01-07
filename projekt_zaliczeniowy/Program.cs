using Gravity.Model.Books;
using Gravity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args); // Tworzenie aplikacji

// Dodanie MVC
builder.Services.AddControllersWithViews();

// Połączenie z bazą SQLite
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlite(builder.Configuration["BooksDatabase:ConnectionString"]);
});

// Obsługa sesji
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sesja ważna 30 minut
    options.Cookie.HttpOnly = true; // Tylko dostęp z serwera
    options.Cookie.IsEssential = true; // Niezbędne dla działania
});

// Konfiguracja uwierzytelniania opartego na cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Lista użytkowników (testowa baza)
var users = new List<AppUser>
{
    new AppUser { Email = "adam@wsei.edu.pl", PasswordHash = HashPassword("1234!"), Role = "admin" },
    new AppUser { Email = "kuba@wsei.edu.pl", PasswordHash = HashPassword("1234@"), Role = "user" }
};
builder.Services.AddSingleton(users); // Dodanie użytkowników

var app = builder.Build();

// Obsługa błędów i bezpieczeństwa
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Podstawowa konfiguracja aplikacji
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Domyślna trasa URL
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Hashowanie hasła
static string HashPassword(string password)
{
    using (var sha256 = System.Security.Cryptography.SHA256.Create())
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
