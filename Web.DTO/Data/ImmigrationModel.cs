using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web.DTO.Data
{
    [Table("Immigrations")]
    public class ImmigrationModel : BaseEntity
    {
        [Required]
        public string ImmigrationStatus { get; set; }
        public bool IsListA { get; set; }

        public string ListADocTitle { get; set; }
        public string ListAIssuingAuthority { get; set; }
        public string ListADocNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime ListAExpiryDate { get; set; }

        public string ListBDocTitle { get; set; }
        public string ListBIssuingAuthority { get; set; }
        public string ListBDocNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime ListBExpiryDate { get; set; }

        public string ListCDocTitle { get; set; }
        public string ListCIssuingAuthority { get; set; }
        public string ListCDocNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime ListCExpiryDate { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public virtual EmployeeModel Employee { get; set; }

    }
}
