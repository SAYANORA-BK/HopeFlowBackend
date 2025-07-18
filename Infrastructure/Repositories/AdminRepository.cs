using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Dapper;
using Infrastructure.DapperContext;

namespace Infrastructure.Repositories
{
public class AdminRepository:IAdminRepository

    {
        private readonly Dappercontext _dapperContext;
         public AdminRepository(Dappercontext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<AdminuserDto>> GetAllUsers()
        {
            var sql = "SELECT id, full_name AS FullName, email, role as Role, is_verified AS IsVerified, isblocked AS IsBlocked FROM users where role!='Admin'";
            using var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<AdminuserDto>(sql);
        }

        public async Task<bool> VerifyCertificate(int id)
        {
            var sql = "UPDATE certificate SET verified = 1 WHERE id = @Id";
            var connection = _dapperContext.CreateConnection();
            var affected = await connection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
        public async Task<bool> IsUserBlocked(int id)
        {
            var query = "SELECT ISNULL(IsBlocked, 0) FROM Users WHERE Id = @Id";
            var connection = _dapperContext.CreateConnection();
            return await connection.ExecuteScalarAsync<bool>(query, new { Id = id });
        }

        public async Task<bool> BlockUser(int id)
        {
            var sql = "UPDATE users SET isblocked = 1 WHERE id = @Id";
             var connection = _dapperContext.CreateConnection();
            var affected = await connection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
        public async Task<bool> UnBlockUser(int id)
        {
            var sql = "UPDATE users SET isblocked = 0 WHERE id = @Id";
            var connection = _dapperContext.CreateConnection();
            var affected = await connection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<BloodcampDto>> GetAllCamps()
        {
            var sql = @"
        SELECT 
            id  AS id,
            created_by AS BankId,
            camp_name AS CampName,
            location,
            start_date AS StartDate,
            end_date AS EndDate,
            description,
            is_verified
        FROM blood_camps";
            var connection = _dapperContext.CreateConnection();
            return await connection.QueryAsync<BloodcampDto>(sql);
        }

        public async Task<bool> ApproveCamp(int id)
        {
            var sql = "UPDATE blood_camps SET is_approved = 1 WHERE id = @Id";
             var connection = _dapperContext.CreateConnection();
            var affected = await connection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        public async Task<bool> UpdateCamp(CampDto dto)
        {
            var sql = @"UPDATE blood_camps 
             SET title = @CampName, 
                 date = @StartDate, 
                 location = @Location 
                         WHERE id = @Id";
            var connection = _dapperContext.CreateConnection();
            var affected = await connection.ExecuteAsync(sql, dto);
            return affected > 0;
        }

       

        public async Task<AnalyticDto> GetAnalyticsSummary()
        {
            var sql = @"
                SELECT 
                    (SELECT COUNT(*) FROM users) AS TotalUsers,
                    (SELECT COUNT(*) FROM users WHERE role='Donor' ) AS TotalDonors,
                    (SELECT COUNT(*) FROM users WHERE role= 'recipient') AS TotalRecipients,
                    (SELECT COUNT(*) FROM  users WHERE role='Hospital') AS TotalHospital,
                    (SELECT COUNT(*)FROM users WHERE role ='bloodbank')AS TotalBloodbanks,
                    (SELECT COUNT(*) FROM blood_requests) AS TotalRequests,
                    (SELECT COUNT(*) FROM blood_requests WHERE status = 'Accepted') AS FulfilledRequests";

             var connection = _dapperContext.CreateConnection();
            return await connection.QuerySingleAsync<AnalyticDto>(sql);
        } 
    }
}
