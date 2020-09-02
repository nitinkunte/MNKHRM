using System;
using System.Threading.Tasks;

namespace Web.Api.Services
{
    public interface IDbService
    {
        Task<bool> RollBack();

        Task<bool> Commit();
    }
}
