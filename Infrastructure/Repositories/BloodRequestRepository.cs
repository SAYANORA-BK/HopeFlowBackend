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
    public class BloodRequestRepository:IBloodReqestRepository
    {
        private readonly Dappercontext _dappercontext;
        public BloodRequestRepository(Dappercontext context)
        {
            _dappercontext = context;
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            var sql = "SELECT COUNT(1) FROM users WHERE id = @Id";
            using var connection = _dappercontext.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = userId });
            return count > 0;
        }


        public async Task<bool> CreateRequestAsync(BloodrequestDto dto, int requesterId)
        {
            var sql = @"INSERT INTO blood_requests 
                    (requested_by, blood_group, units_required, location, reason, status, requested_at)
                    VALUES (@RequestedBy, @BloodGroup, @Units, @Location, @Reason, 'Pending', CURRENT_TIMESTAMP)";

            using var connection = _dappercontext.CreateConnection();
            var result = await connection.ExecuteAsync(sql, new
            {
                RequestedBy = requesterId,
                BloodGroup = dto.BloodGroup,
                Units = dto.UnitsRequired,
                Location = dto.Location,
                Reason = dto.Reason
            });
            return result > 0;
        }

        public async Task<IEnumerable<BloodrequestResponseDto>> GetRequestsByStatusAsync(string status)
        {
            var sql = @"SELECT r.id, u.full_name AS RequesterName, r.blood_group, r.units_required, 
                           r.location, r.reason, r.status, r.requested_at
                    FROM blood_requests r
                    JOIN users u ON u.id = r.requested_by
                    WHERE r.status = @Status";

            using var connection = _dappercontext.CreateConnection();
            return await connection.QueryAsync<BloodrequestResponseDto>(sql, new { Status = status });
        }

        public async Task<bool> UpdateRequestStatusAsync(int requestId, string status)
        {
            var sql = @"UPDATE blood_requests 
                    SET status = @Status, updated_at = GETDATE() 
                    WHERE id = @RequestId";

            using var connection = _dappercontext.CreateConnection();
            var result = await connection.ExecuteAsync(sql, new { Status = status, RequestId = requestId });
            return result > 0;


        }
        public async Task<IEnumerable<BloodrequestResponseDto>> GetAllRequestsAsync()
        {
            var sql = @"SELECT r.id, u.full_name AS RequesterName, r.blood_group, r.units_required, 
                       r.location, r.reason, r.status, r.requested_at
                FROM blood_requests r
                JOIN users u ON u.id = r.requested_by";

             var connection = _dappercontext.CreateConnection();
            return await connection.QueryAsync<BloodrequestResponseDto>(sql);
        }



    }
}