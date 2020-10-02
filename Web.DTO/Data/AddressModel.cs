using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.DTO.Data
{
    [Table("Addresses")]
    public class AddressModel : BaseEntity
    {
        [Required]
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneLand { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneMobile { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailOther { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }
        public string Fax { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        //[ForeignKey("EmployeeId")]
        //public virtual Employee Employee { get; set; }

    }
}
