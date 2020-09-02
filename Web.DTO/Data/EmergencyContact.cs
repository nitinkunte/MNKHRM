using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.DTO.Data
{
    public class EmergencyContact : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public int RelationshipStatus { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        //[ForeignKey("EmployeeId")]
        //public virtual Employee Employee { get; set; }

    }
}
