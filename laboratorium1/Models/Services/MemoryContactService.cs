using System;
using System.Collections.Generic;
using System.Linq;

namespace laboratorium1.Models.Services
{
    public class MemoryContactService : IContactService
    {
        private Dictionary<int, Contact> _contacts = new Dictionary<int, Contact>()
        {
            {1, new Contact {Id = 1, Email = "email@wsei.com", FirstName = "Jakub", LastName = "Putowski", BirthDate = new DateTime(1990, 11, 05), Telephone = "111 111 111"}}, 
            {2, new Contact {Id = 2, Email = "email1@wsei.com", FirstName = "Karol", LastName = "Kowal", BirthDate = new DateTime(1950, 03, 17), Telephone = "222 222 222"}}
        };

        private int _currentId = 2;

        public void Add(Contact model)
        {
            model.Id = ++_currentId;
            _contacts.Add(model.Id, model);
        }

        public void Update(Contact contact)
        {
            if (_contacts.ContainsKey(contact.Id))
            {
                _contacts[contact.Id] = contact;
            }
        }

        public void Delete(int id)
        {
            _contacts.Remove(id);
        }

        // Change this method to return List<Contact>
        public List<Contact> GetAll() // Updated return type
        {
            return _contacts.Values.ToList(); // Returns a List<Contact>
        }

        public Contact? GetById(int id)
        {
            return _contacts.TryGetValue(id, out var contact) ? contact : null;
        }
    }
}
