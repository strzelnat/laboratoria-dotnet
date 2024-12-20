using Microsoft.AspNetCore.Mvc;

namespace Lab01Ak.Models.Controllers;

public class CalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public enum Operator
    {
        Unknown, Add, Mul, Sub, Div
    }
    [HttpPost]
    public IActionResult Result([FromForm]Calculator model)
    {
        if (!model.IsValid())
        {
            return View("Error");
        }
        return View(model);
    }
            public IActionResult Form()
            {
                return View();
            }
        }

