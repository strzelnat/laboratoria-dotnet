using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace laboratorium1.Models
{
    public class Contact
    {
        [HiddenInput]
        public int Id { get; set; }

        [MaxLength(20,ErrorMessage = "Name cannot be longer than 50 charachters.")]
        [Required(ErrorMessage = "You are obligated to write you're name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(20, ErrorMessage = "Surname cannot be longer than 50 charachters.")]
        [Required(ErrorMessage = "You are obligated to write you're surname.")]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [RegularExpression(".+\\@.+.[a-z]{2,3}")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        
        [Phone(ErrorMessage = "Please write phone number correctly.")]
        [Display(Name = "Phone number")]
        public string Telephone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Category")]
        public Category  Category { get; set; }

    }
}
