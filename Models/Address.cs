using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set;}
        public string Pincode { get; set;}
        public string State { get; set;}
        public string Country { get; set;}
    }
}
