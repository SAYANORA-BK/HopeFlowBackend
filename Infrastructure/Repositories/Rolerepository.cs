    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Application.DTO;
    using Application.Interface.IRepository;
    using Infrastructure.DapperContext;
    using Microsoft.Data.SqlClient;
    using Dapper;

    namespace Infrastructure.Repositories
    {
      public  class Rolerepository:IRolerepository
       
        {
            private readonly Dappercontext _dappercontext;

            public Rolerepository(Dappercontext dappercontext)
            {
                _dappercontext = dappercontext;
            }
            public  async Task<List<RoleSelectionDto>> GetAllRolesAsync()
            {
          

                var sql = "SELECT Role_id ,role_name FROM Roles";
                var connection= _dappercontext.CreateConnection();
                var roles = await connection.QueryAsync<RoleSelectionDto>(sql);
                return roles.ToList();
            }

            public async Task<string> GetRolesAsyncById(int roleid)
            {
                var sql = "SELECT role_name FROM Roles WHERE Role_id = @roleid";
                var connection = _dappercontext.CreateConnection();
                var role = await connection.ExecuteScalarAsync<string>(sql, new { roleid });
                return role;
            }






        }
    }
