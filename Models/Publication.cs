using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }
        public string PublishingCompanyName { get; set;}

        //Foreign Key
        [Display(Name = "Address")]
        public virtual int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Addresses { get; set; }
    }
}
