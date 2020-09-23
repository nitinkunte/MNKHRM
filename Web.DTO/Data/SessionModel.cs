using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web.DTO.Data
{
    [Table("UserSessions")]
    public class SessionModel : BaseEntity
    {
        public SessionModel()
        {
        }

        [Required]
        public string UserName { get; set; }

        public string IPAddress { get; set; }

        public string UserOID { get; set; }
    }
}
