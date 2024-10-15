using laboratorium1.Models;
using Microsoft.AspNetCore.Mvc;
using static laboratorium1.Controllers.HomeController;

namespace laboratorium1.Controllers
{
    public class CalculatorController : Controller
    {

        
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Result([FromForm]Calculator calculator)
        {
            if(!calculator.IsValid())
            {
                return View("Error", calculator);
            }
         
            return View(calculator);
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
