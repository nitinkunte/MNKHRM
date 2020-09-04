using System;
using System.ComponentModel.DataAnnotations;

namespace Web.DTO.Data
{
    public class UserRoleModel : BaseEntity
    {
        public UserRoleModel()
        {
        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
