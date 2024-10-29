using laboratorium1.Models.Services;
using laboratorium1.Models;
using Microsoft.AspNetCore.Mvc;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // Lista kontaktów
    public IActionResult Index()
    {
        var contacts = _contactService.GetAll();
        return View(contacts);
    }

    // Dodawanie kontaktu / formularz
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Contact contact)
    {
        if (ModelState.IsValid)
        {
            _contactService.Add(contact);
            return RedirectToAction("Index");
        }

        return View(contact);
    }

    // Widok potwierdzenia usunięcia
    public IActionResult Delete(int id)
    {
        var contact = _contactService.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    // Usunięcie kontaktu
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _contactService.Delete(id);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var contact = _contactService.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        return View(_contactService.GetById(id));
    }

    [HttpPost]
    public IActionResult Edit(Contact contact)
    {
        if (ModelState.IsValid)
        {
            _contactService.Update(contact);
            return RedirectToAction("Index");
        }

        return View(contact);
    }
}
