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
  public   class DonorRepository:IDonorRepository
    {
        private readonly Dappercontext _dapperContext;

        public DonorRepository(Dappercontext dapperContext)
        {
            _dapperContext = dapperContext;
        }


        public async Task<IEnumerable<BloodrequestResponseDto>> GetMatchingRequests(int userId)
        {
            var sql = @"
        SELECT 
            br.id,
            u.full_name AS RequesterName,
            br.blood_group,
            br.units_required AS UnitsRequired,
            br.location AS Location,
            br.reason AS Reason,
            br.status AS Status,
            br.created_at AS RequestedAt
        FROM blood_requests br
        INNER JOIN users u ON br.id = u.id
        INNER JOIN users d ON d.id = @UserId
        WHERE br.blood_group = d.blood_group 
          AND br.status = 'Pending'";

            using var connection = _dapperContext.CreateConnection();

            return await connection.QueryAsync<BloodrequestResponseDto>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<DonationDto>> GetDonationHistory(int userId)
        {
            using var conn = _dapperContext.CreateConnection();

            var bloodGroup = await conn.QueryFirstOrDefaultAsync<string>(
                "SELECT blood_group FROM users WHERE id = @UserId",
                new { UserId = userId });

            if (string.IsNullOrEmpty(bloodGroup))
                return Enumerable.Empty<DonationDto>();

            var sql = @"
        SELECT 
            id, 
            donor_id AS DonorId, 
            camp_id AS CampId, 
            donation_date AS DonationDate, 
            units_donated, 
            blood_group AS BloodGroup
        FROM donations
        WHERE donor_id = @UserId AND blood_group = @BloodGroup
        ORDER BY donation_date DESC";

            return await conn.QueryAsync<DonationDto>(sql, new { UserId = userId, BloodGroup = bloodGroup });
        }

        public async Task<DateTime?> GetLastDonationDate(int userId)
        {
            using var conn = _dapperContext.CreateConnection();

            var sql = @"
            SELECT TOP 1 donation_date
            FROM donations
            WHERE donor_id = @UserId
            ORDER BY donation_date DESC";

            return await conn.QueryFirstOrDefaultAsync<DateTime?>(sql, new { UserId = userId });
        }

        public async Task<bool> SaveCertificate(int userId, string filePath)
        {
            using var conn = _dapperContext.CreateConnection();

            var sql = @"
            UPDATE donors
            SET certificate_path = @Path
            WHERE user_id = @UserId";

            var affected = await conn.ExecuteAsync(sql, new { Path = filePath, UserId = userId });
            return affected > 0;
        }

        public async Task<string> GetCertificatePath(int userId)
        {
            using var conn = _dapperContext.CreateConnection();

            var sql = @"SELECT certificate_path FROM donors WHERE user_id = @UserId";

            return await conn.QueryFirstOrDefaultAsync<string>(sql, new { UserId = userId });
        }


    }
}
