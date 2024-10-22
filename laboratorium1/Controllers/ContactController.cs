using laboratorium1.Models;
using Microsoft.AspNetCore.Mvc;

namespace laboratorium1.Controllers
{
    public class ContactController : Controller
    {
        // rozw tymczasowe 

        private static Dictionary<int, Contact> _contacts = new Dictionary<int, Contact>()
        {
           
        };

        //lista kontaktów
        public IActionResult Index()
        {
            return View(_contacts);
        }

        //dodawanie kontaktu / formularz

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] //tu zwracam dane z formularza i zapisanie  w kontaktach
        public IActionResult Add(Contact contact)
        {
            if(ModelState.IsValid)
            {

                int id = _contacts.Keys.Count != 0 ? _contacts.Keys.Max() : 0;
                contact.Id = id + 1;
                _contacts.Add(contact.Id, contact);

                return RedirectToAction("Index",_contacts);
            }

            return View(contact);
        }


        public IActionResult Delete(int id)
        {
            if(_contacts.ContainsKey(id))
            {
                _contacts.Remove(id);
                return RedirectToAction("Index",_contacts);
            }

            return View("Index");
        }

        public IActionResult Details(int id)
        {
            return View(_contacts[id]);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(_contacts.ContainsKey(id))
            {
                return View(_contacts[id]);
            }

            return NotFound();
        }


        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contacts[contact.Id] = contact;
                return RedirectToAction("Index", _contacts);
            }

            return View(contact);
        }


    }
}
