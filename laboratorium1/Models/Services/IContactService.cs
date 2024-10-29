namespace laboratorium1.Models.Services
{
    public interface IContactService
    {
        void Add(Contact contact);
        void Delete(int id);
        void Update(Contact contact);

        List<Contact> GetAll();
        Contact? GetById(int id);

    }
}
