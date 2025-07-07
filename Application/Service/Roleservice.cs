
using System.Data;
using Dapper;
using Application.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Application.Interface.IRepository;
using ommon;
using Application.Interface.IService;

namespace Application.Service
{
    public class Roleservice:IRoleservice

    {
        private readonly IRolerepository _rolerepository;
      

        public Roleservice(IRolerepository rolerepository )
        {
          
            _rolerepository = rolerepository;

        }

        public async Task <ApiresponseDto<List<RoleSelectionDto>>> GetAllRolesAsync()
        {
            try
            {
                var data = await _rolerepository.GetAllRolesAsync();
                if (data.Count == 0)
                {
                    return new ApiresponseDto<List<RoleSelectionDto>>
                    {
                        StatusCode = 200,
                        Message = "not data found"

                    };
                }
                return new ApiresponseDto<List<RoleSelectionDto>>
                {
                    StatusCode = 200,
                    Message = "data found",
                    Data = data

                };


            }
            catch (Exception ex)
            {
                return new ApiresponseDto<List<RoleSelectionDto>>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }

        }
}
