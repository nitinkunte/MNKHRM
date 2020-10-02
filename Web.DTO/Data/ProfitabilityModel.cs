using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace Web.DTO.Data
{
    [Table("Profitability")]
    public class ProfitabilityModel : BaseEntity
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public double PayRate { get; set; }

        [Required]
        public string PayRatePer { get; set; }

        public double PayRateHourly { get; set; }

        public double PayRateYearly { get; set; }

        public double IncomeRate { get; set; } = 0.0;

        public string IncomeRatePer { get; set; }

        [JsonIgnore]
        [ForeignKey("EmployeeId")]
        public virtual EmployeeModel Employee { get; set; }

    }
}