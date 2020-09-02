﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Web.DTO.Data
{
    public class Employee : BaseEntity
    {
        public Employee() { }

        [Required]
        [MinLength(2)]
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        [Required]
        [MinLength(1)]
        public string NameLast { get; set; }
        public string NamePrintOnCheck { get; set; }
        public string SSN { get; set; }
        public int Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = SqlDateTime.MinValue.Value;

        public int MaritalStatus { get; set; }
        public int Ethnicity { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        /// <summary>
        /// This relates to disability and not DB active status
        /// </summary>
        public bool IsDisabled { get; set; }

        public bool IsI9OnFile { get { return (ImmigrationId > 0); } private set { } }

        public int ImmigrationId { get; set; }
        [ForeignKey("ImmigrationId")]
        public virtual Immigration Immigration { get; set; }

        public int AddressHomeId { get; set; }
        [NotMapped]
        public virtual Address AddressHome { get; set; }

        public int AddressOtherId { get; set; }
        [NotMapped]
        public virtual Address AddressOther { get; set; }

        //public int EmergencyContact1Id { get; set; }
        //[ForeignKey("EmergencyContact1Id")]
        [NotMapped]
        public virtual EmergencyContact EmergencyContact1 { get; set; }

        //public int EmergencyContact2Id { get; set; }
        //[ForeignKey("EmergencyContact2Id")]
        [NotMapped]
        public virtual EmergencyContact EmergencyContact2 { get; set; }

        public int EmploymentInfoId { get; set; }
        public virtual EmploymentInfo EmploymentInfo { get; set; }
    }


}