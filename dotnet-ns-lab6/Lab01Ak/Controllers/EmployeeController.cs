using Lab01Ak.Models;
using Microsoft.AspNetCore.Mvc;

public class EmployeeController : Controller
{
    static Dictionary<int, Employee> _employees = new Dictionary<int, Employee>();

    public IActionResult Index()
    {
        return View(_employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee model)
    {
        if (ModelState.IsValid)
        {
            int id = _employees.Count > 0 ? _employees.Keys.Max() + 1 : 1;
            model.Id = id;
            _employees.Add(model.Id, model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (_employees.ContainsKey(id))
        {
            return View(_employees[id]);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(Employee model)
    {
        if (ModelState.IsValid && _employees.ContainsKey(model.Id))
        {
            _employees[model.Id] = model;
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if (_employees.ContainsKey(id))
        {
            return View(_employees[id]);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Delete(Employee model)
    {
        if (_employees.ContainsKey(model.Id))
        {
            _employees.Remove(model.Id);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    public IActionResult Details(int id)
    {
        if (_employees.ContainsKey(id))
        {
            return View(_employees[id]);
        }
        return NotFound();
    }
}
