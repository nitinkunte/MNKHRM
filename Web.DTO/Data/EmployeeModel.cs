using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Web.DTO.Data
{
    [Table("Employees")]
    public class EmployeeModel : BaseEntity
    {
        public EmployeeModel() { }

        [Required(ErrorMessage = "First Name is mandatory")]
        [MinLength(2)]
        public string NameFirst { get; set; }
        [DisplayName("Middle Initial")]
        public string NameMiddle { get; set; }
        [Required]
        [MinLength(1)]
        [DisplayName("Last Name")]
        public string NameLast { get; set; }
        public string NamePrintOnCheck { get; set; }
        [Required]
        public string SSN { get; set; }
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = SqlDateTime.MinValue.Value;

        public string MaritalStatus { get; set; }
        public string Ethnicity { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        /// <summary>
        /// This relates to disability and not DB active status
        /// </summary>
        public bool IsDisabled { get; set; }

        [StringLength(2000)]
        public string DisabilityDesc { get; set; }
        public bool IsI9OnFile { get { return (ImmigrationId > 0); } private set { } }

        public int ImmigrationId { get; set; }
        [ForeignKey("ImmigrationId")]
        public virtual ImmigrationModel Immigration { get; set; }

        public int AddressHomeId { get; set; }
        [NotMapped]
        public virtual AddressModel AddressHome { get; set; }

        public int AddressOtherId { get; set; }
        [NotMapped]
        public virtual AddressModel AddressOther { get; set; }

        //public int EmergencyContact1Id { get; set; }
        //[ForeignKey("EmergencyContact1Id")]
        [NotMapped]
        public virtual EmergencyContactModel EmergencyContact1 { get; set; }

        //public int EmergencyContact2Id { get; set; }
        //[ForeignKey("EmergencyContact2Id")]
        [NotMapped]
        public virtual EmergencyContactModel EmergencyContact2 { get; set; }

        public int EmploymentInfoId { get; set; }
        public virtual EmploymentInfoModel EmploymentInfo { get; set; }

        public int ProfitabilityId { get; set; }
        public virtual ProfitabilityModel Profitability { get; set; }
    }


}
