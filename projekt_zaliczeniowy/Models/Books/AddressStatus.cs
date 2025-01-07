using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gravity.Models.Books
{
    public partial class AddressStatus
    {
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Status adresu jest wymagany.")]
        [StringLength(50, ErrorMessage = "Status adresu nie może przekraczać 50 znaków.")]
        public string AddressStatus1 { get; set; }


        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
    }
}