using Lab01Ak.Models;
using LaboratoriumASPNET.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoriumASPNET.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    // Konstruktor z wstrzykiwaniem zależności
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // Lista kontaktów
    public IActionResult Index()
    {
        // Przekazujemy listę kontaktów do widoku
        return View(_contactService.GetAll());
    }
    
    // Formularz dodania kontaktu
    public IActionResult Add()
    {
        return View();
    }

    // Odebranie danych z formularza i zapisanie w kontaktach
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        _contactService.Add(model);

        // Przekierowanie do listy kontaktów po dodaniu nowego kontaktu
        return RedirectToAction("Index");
    }

    // Usunięcie kontaktu
    public IActionResult Delete(int id)
    {
        _contactService.Delete(id);

        // Przekierowanie do listy kontaktów po usunięciu kontaktu
        return RedirectToAction("Index");
    }

    // Szczegóły kontaktu
    public IActionResult Details(int id)
    {
        var contact = _contactService.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }
    
    // Formularz edycji kontaktu
    public IActionResult Edit(int id)
    {
        var contact = _contactService.GetById(id);
        if (contact == null)
        {
            return NotFound();
        }
        
        return View(contact);
    }

    // POST: Zapisz zmiany kontaktu
    [HttpPost]
    public IActionResult Edit(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        _contactService.Update(model);
        return RedirectToAction("Index");
    }
}
