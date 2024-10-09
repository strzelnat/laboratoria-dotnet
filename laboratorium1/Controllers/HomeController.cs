using laboratorium1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace laboratorium1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public enum Operator
        {
            Unknown, Add, Mul, Sub, Div
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult About(Operator op)
        {

            ViewBag.Op = op;
            return View();
        }

        public IActionResult Calculator(Operator? op, double? a, double? b)
        {

            ViewBag.A = a;
            ViewBag.B = b;

            double? result = 0;

            try
            {
                if (a is null)
                {
                    throw new ArgumentException(nameof(a), "Parametr a jest wymagany.");
                }

                if (b is null)
                {
                    throw new ArgumentException(nameof(b), "Parametr b jest wymagany");
                }

                if (op is null)
                {
                    throw new ArgumentException(nameof(op), "Parametr op jest wymagany, musi byæ z listy: Add, Mul, Sub, Div ");
                }

                switch (op)
                {
                    case Operator.Add:
                        result = a + b;
                        ViewBag.Op = "+";
                        break;

                    case Operator.Sub:
                        result = a - b;
                        ViewBag.Op = "-";
                        break;

                    case Operator.Div:

                        if (b != 0)
                        {
                            result = a / b;
                            ViewBag.Op = "/";
                        }
                        else
                        {
                            ViewBag.Error = "Nie wolno dzieliæ przez zero!";
                            return View("Error");
                        }
                        break;

                    case Operator.Mul:
                        result = a * b;
                        ViewBag.Op = "*";
                        break;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");

            }

            ViewBag.result = result;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
