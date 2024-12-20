using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lab01Ak.Models;
using Microsoft.VisualBasic.CompilerServices;
namespace LaboratoriumASPNET.Models;
[Table("contacts")]
public class ContactEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    
    public string Email { get; set; }
    [MaxLength(12)]
    [MinLength(9)]
    public string PhoneNumber { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public DateTime Created { get; set; }
    
    public Category Category { get; set; }

}