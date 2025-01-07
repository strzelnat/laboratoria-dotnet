using System;
using System.Collections.Generic;

namespace Gravity.Models.Books;

public partial class ShippingMethod
{
    public int MethodId { get; set; }

    public string? MethodName { get; set; }

    public double? Cost { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();
}
