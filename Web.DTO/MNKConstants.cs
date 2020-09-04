using System;
using System.Collections.Generic;

namespace Web.DTO
{
    public class MNKConstants
    {
        public static class ROLES
        {
            public static string SUPER_ADMIN = "super-admin";
            public static string ADMIN = "admin";
            public static string HR = "hr";
            public static string USER = "user";

            public static List<string> ToList()
            {
                return new List<string>
                        {
                            MNKConstants.ROLES.SUPER_ADMIN.ToLower(),
                            MNKConstants.ROLES.ADMIN.ToLower(),
                            MNKConstants.ROLES.HR.ToLower(),
                            MNKConstants.ROLES.USER.ToLower()
                        };
            }

            public static string GetFirstOrDefault(string role)
            {
                string ret = MNKConstants.ROLES.USER;
                if ((!string.IsNullOrWhiteSpace(role))
                    && (MNKConstants.ROLES.ToList().Contains(role.ToLower())))
                {
                    ret = role;
                }
                return ret;
            }
        }
    }
}
