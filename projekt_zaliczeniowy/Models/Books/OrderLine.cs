
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravity.Models.Books;

public partial class OrderLine
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LineId { get; set; }

    public int? OrderId { get; set; }

    public int? BookId { get; set; }

    public double? Price { get; set; }

    public virtual Book? Book { get; set; }

    public virtual CustOrder? Order { get; set; }
}