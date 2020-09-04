﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.DTO.Data;

namespace Web.Api.Services
{
    public interface ISessionService
    {
        Task<UserRoleModel> GetUserRoleByUserNameAsync(string userName);
        Task<UserRoleModel> SaveUserRoleAsync(UserRoleModel userRole);
    }

    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext dbContext;

        public SessionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserRoleModel> GetUserRoleByUserNameAsync(string userName)
        {
            UserRoleModel ret = await dbContext.UserRoles
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.UserName == userName
                                        && x.IsActive == true);

            return ret;
        }

        public async Task<UserRoleModel> SaveUserRoleAsync(UserRoleModel userRole)
        {
            UserRoleModel ret = null;

            if (null != userRole)
            {
                var dbUser = await dbContext.UserRoles
                    .FirstOrDefaultAsync(x => x.UserName == userRole.UserName);
                if (null != dbUser)
                {
                    dbUser.RoleName = userRole.RoleName;
                    dbUser.FirstName = (string.IsNullOrWhiteSpace(userRole.FirstName)) ? dbUser.FirstName : userRole.FirstName;
                    dbUser.LastName = (string.IsNullOrWhiteSpace(userRole.LastName)) ? dbUser.LastName : userRole.LastName;
                    dbUser.UpdateDate = DateTime.UtcNow;
                    dbUser.UpdateUser = userRole.UpdateUser;
                    dbUser.IsActive = userRole.IsActive;
                    dbContext.UserRoles.Update(dbUser);
                    await dbContext.SaveChangesAsync();
                    ret = dbUser;
                }
                else
                {
                    await dbContext.UserRoles.AddAsync(userRole);
                    await dbContext.SaveChangesAsync();
                    ret = userRole;
                }
            }

            return ret;
        }
    }
}
