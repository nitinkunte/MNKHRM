using System;
using System.ComponentModel.DataAnnotations;

namespace Web.DTO.Data
{
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
