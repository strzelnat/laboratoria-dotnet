using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravity.Models.Books
{
    public partial class CustomerAddress
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Address")]
        public int AddressId { get; set; }

        [ForeignKey("AddressStatus")]
        public int StatusId { get; set; }

       
        public virtual Address Address { get; set; }

        
        public virtual Customer Customer { get; set; }


        public virtual AddressStatus Status { get; set; }
    }
}