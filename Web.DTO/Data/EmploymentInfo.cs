using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace Web.DTO.Data
{
    public class EmploymentInfo : BaseEntity
    {
        public EmploymentInfo()
        {
        }

        [Required]
        public int EmployeeId { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = SqlDateTime.MinValue.Value;

        [DataType(DataType.Date)]
        public DateTime OriginalHireDate { get; set; } = SqlDateTime.MinValue.Value;

        [DataType(DataType.Date)]
        public DateTime AdjustedServiceDate { get; set; } = SqlDateTime.MinValue.Value;

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = SqlDateTime.MinValue.Value;

        [Required]
        public int EmploymentType { get; set; }

        public bool IsFullTime { get; set; } = true;
        public bool IsSeasonal { get; set; } = false;
        public bool IsExempt { get; set; } = true;
        public bool IsKeyEmployee { get; set; } = false;

        public string Title { get; set; }

        public string Supervisor { get; set; }
        public string Department { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [DataType(DataType.Date)]
        public DateTime LastDayWorked { get; set; } = SqlDateTime.MinValue.Value;
        [DataType(DataType.Date)]
        public DateTime LastDayOnBenefits { get; set; } = SqlDateTime.MinValue.Value;

        public bool SeverancePaid { get; set; } = false;

        [DataType(DataType.MultilineText)]
        public string SeverancePayNotes { get; set; }

        public string TerminationDetail { get; set; }
        [DataType(DataType.MultilineText)]
        public string TerminationNotes { get; set; }

        public bool IsReHireRecommended { get; set; } = true;

        public bool ProtestUnemployment { get; set; }

        [JsonIgnore]
        [ForeignKey("EmployeeId")]
        public virtual EmployeeModel Employee { get; set; }

    }
}
