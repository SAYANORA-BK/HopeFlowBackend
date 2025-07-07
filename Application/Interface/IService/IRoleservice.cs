using Application.DTO;
using ommon;

namespace Application.Interface.IService
{
    public interface IRoleservice
    {

        Task<ApiresponseDto<List<RoleSelectionDto>>> GetAllRolesAsync();
        

    }

}
