using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.IRepository;
using Dapper;
using Infrastructure.DapperContext;

namespace Infrastructure.Repositories
{
  public  class CertificateRepository:ICertificaterepository
    {
        private readonly Dappercontext _dappercontext;
        public CertificateRepository(Dappercontext context)
        {
            _dappercontext = context;
        }
        public async Task<bool> AddCertificateAsync(string certificate, int userId)
        {
            var sql = @"INSERT INTO certificate ( user_id, role_id, upload_date, verified,image)
                VALUES ( @UserId, @RoleId, @UploadDate, @Verified,@Certificate)";

            using var connection = _dappercontext.CreateConnection();

            var certificates = new
            {
                Certificate = certificate,
                UserId = userId,
                RoleId = 2, 
                UploadDate = DateTime.UtcNow,
                Verified = false
            };

            var rowsAffected = await connection.ExecuteAsync(sql, certificates);
            return rowsAffected > 0;
        }

    }
}
