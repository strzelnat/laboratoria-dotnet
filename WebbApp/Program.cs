using Gravity.Model.Books;
using Microsoft.EntityFrameworkCore;
using WebbApp.Model.Movies;
using WebbApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Dodanie MVC
builder.Services.AddControllersWithViews();

// Połączenie z bazą danych SQLite dla filmów
builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["MoviesDatabase:ConnectionString"]);
});

// Połączenie z bazą danych SQLite dla książek
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlite(builder.Configuration["BooksDatabase:ConnectionString"]);
});

// Obsługa sesji
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Konfiguracja uwierzytelniania opartego na cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Lista testowych użytkowników
var users = new List<AppUser>
{
    new AppUser { Email = "adam@wsei.edu.pl", PasswordHash = HashPassword("1234!"), Role = "admin" },
    new AppUser { Email = "kuba@wsei.edu.pl", PasswordHash = HashPassword("1234@"), Role = "user" }
};
builder.Services.AddSingleton(users);

var app = builder.Build();

// Obsługa błędów
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

// Metoda hashująca hasło
static string HashPassword(string password)
{
    using (var sha256 = System.Security.Cryptography.SHA256.Create())
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
