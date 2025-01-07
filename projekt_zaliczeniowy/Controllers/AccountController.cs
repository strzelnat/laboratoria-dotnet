using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Gravity.Models;
using Microsoft.AspNetCore.Authorization;

public class AccountController : Controller
{
    private readonly List<AppUser> _users;

    public AccountController(List<AppUser> users)
    {
        _users = users;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
    {
        var hashedPassword = HashPassword(password);
        var user = _users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Jeśli returnUrl jest ustawiony i lokalny -> przekieruj tam
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            // Jeśli returnUrl nie jest ustawiony -> przekieruj do profilu
            return RedirectToAction("Profile");
        }

        ViewBag.Error = "Nieprawidłowy email lub hasło.";
        return View();
    }
    
    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ResetPassword(string email)
    {
        // Tymczasowa obsługa (dostosuj logikę resetowania hasła)
        ViewBag.Message = "Funkcja resetowania hasła nie jest jeszcze dostępna.";
        return View();
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string registerName, string registerEmail, string registerPassword)
    {
        ViewBag.Message = "Rejestracja jest obecnie w fazie testowej.";
        return View();
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Profile()
    {
        var email = User.Identity.Name;
        var user = _users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return RedirectToAction("Login");
        }
        return View(user);
    }

}
