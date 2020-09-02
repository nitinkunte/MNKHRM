using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web.DTO.Data
{
    public class BaseEntity
    {
        public BaseEntity()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
