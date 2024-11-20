using System.ComponentModel.DataAnnotations.Schema;

namespace LaboratoriumASPNET.Models;

[Table("organizations")]
public class OrganizationEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Regon { get; set; }
    public string Nip { get; set; }
    public Address? Address { get; set; }              //klasa osadzona - brak osobnej tabeli
    public ISet<ContactEntity> Contacts { get; set; } // właściwość nawigacyjne - osobna tabela
}

public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}