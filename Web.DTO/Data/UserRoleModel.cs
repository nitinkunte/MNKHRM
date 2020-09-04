using System;
namespace Web.DTO.Data
{
    public class UserRoleModel : BaseEntity
    {
        public UserRoleModel()
        {
        }

        public string RoleName { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
