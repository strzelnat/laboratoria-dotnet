using Lab01Ak.Models;

namespace LaboratoriumASPNET.Models.Services;

public interface IContactService
{
    void Add(ContactModel model);
    void Update(ContactModel model);
    void Delete(int id);
    List<ContactModel> GetAll();
    ContactModel? GetById(int id);
}