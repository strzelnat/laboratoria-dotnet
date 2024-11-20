namespace Lab01Ak.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class Employee
{
    [HiddenInput]
    public int Id { get; set; }

    [Required(ErrorMessage = "Proszę podać imię!")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Proszę podać nazwisko!")]
    public string LastName { get; set; }

    [RegularExpression(@"^\d{11}$", ErrorMessage = "Proszę podać poprawny PESEL!")]
    [Required(ErrorMessage = "Proszę podać PESEL!")]
    public string PESEL { get; set; }

    [Required(ErrorMessage = "Proszę podać stanowisko!")]
    public string Position { get; set; }

    [Required(ErrorMessage = "Proszę podać oddział!")]
    public string Department { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Proszę podać datę zatrudnienia!")]
    public DateTime HireDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? TerminationDate { get; set; }
}