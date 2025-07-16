using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interface.IRepository
{
   public interface IRolerepository
    {
        Task<List<RoleSelectionDto>> GetAllRoles();

        Task<string> GetRolesById(int roleid);
    }
}
