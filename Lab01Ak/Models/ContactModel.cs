using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab01Ak.Models;

public class ContactModel
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please enter your first name")]
    [MaxLength(length: 20, ErrorMessage = "Name too long")]
    [Display(Name = "Imie")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Please enter your last name")]
    [MaxLength(length: 50, ErrorMessage = "LastName too long")]
    [Display(Name = "Nazwisko")]
    public string LastName { get; set; }
    
    [EmailAddress]
    [Display(Name = "Adres Email")]

    public string Email { get; set; }
    
    [Phone]
    [Display(Name = "Numer Telefonu")]

    [RegularExpression("\\d\\d\\d \\d\\d\\d \\d\\d\\d")]
    public string PhoneNumber { get; set; }
    
    [DataType(DataType.Date)]
    [Display(Name = "Data urodzenia")]

    public DateTime BirthDate { get; set; }
    [Display(Name = "Kategoria")]

    public Category Category { get; set; }
}