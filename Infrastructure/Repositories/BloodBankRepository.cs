using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Dapper;
using Infrastructure.DapperContext;

namespace Infrastructure.Repositories
{
  public   class BloodBankRepository:IBloodBankRepository
    {

        private readonly Dappercontext _dappercontext;
        public BloodBankRepository(Dappercontext context)
        {
            _dappercontext = context;
        }
        public async Task<IEnumerable<BloodInventoryDto>> GetInventory(int bankId)
        {
            string sql = @"
            SELECT 
            blood_group AS BloodGroup, 
            units_available AS UnitsAvailable, 
            last_updated AS LastUpdated
            FROM blood_inventory 
            WHERE blood_bank_id = @BankId";

            using var connection = _dappercontext.CreateConnection();
            return await connection.QueryAsync<BloodInventoryDto>(sql, new { BankId = bankId });
        }



        public async Task<int?> GetBloodBankIdByUserId(int userId)
        {
            var connection = _dappercontext.CreateConnection();

            string sql = "SELECT user_id FROM blood_banks WHERE user_id = @UserId";
            var bankId = await connection.QueryFirstOrDefaultAsync<int?>(sql, new { UserId = userId });

            return bankId;
        }

        public async Task<bool> UpdateInventory(BloodInventoryDto dto, int userId)
        {
            using var connection = _dappercontext.CreateConnection();

         
            var bankId = await GetBloodBankIdByUserId(userId);
            if (!bankId.HasValue)
                throw new Exception("Blood Bank not found for the user. Cannot update inventory.");

           
            var existingCount = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT COUNT(*) FROM blood_inventory 
          WHERE blood_group = @BloodGroup AND blood_bank_id = @BankId",
                new { dto.BloodGroup, BankId = bankId.Value });

            if (existingCount > 0)
            {
              
                var updateSql = @"
                 UPDATE blood_inventory
                 SET units_available = @UnitsAvailable,
                 last_updated = GETDATE()
                 WHERE blood_group = @BloodGroup AND blood_bank_id = @BankId";

                var rowsAffected = await connection.ExecuteAsync(updateSql, new
                {
                    dto.UnitsAvailable,
                    dto.BloodGroup,
                    BankId = bankId.Value
                });

                return rowsAffected > 0;
            }
            else
            {
                
                var insertSql = @"
            INSERT INTO blood_inventory (blood_group, units_available, blood_bank_id, created_at)
            VALUES (@BloodGroup, @UnitsAvailable, @BankId, GETDATE())";

                var rowsAffected = await connection.ExecuteAsync(insertSql, new
                {
                    dto.BloodGroup,
                    dto.UnitsAvailable,
                    BankId = bankId.Value
                });

                return rowsAffected > 0;
            }
        }
        public async Task<IEnumerable<CampDto>> GetCamps(int bankId)
        {
            string sql = "SELECT    camp_name AS campName,    location,   start_date AS startDate,    end_date AS endDate,   description FROM blood_camps where  created_by = @BankId";
            var connection = _dappercontext.CreateConnection();
            return await connection.QueryAsync<CampDto>(sql, new { BankId = bankId });
        }

        public async Task<bool> AddCamp(CampDto dto, int bankId)
        {
            using var connection = _dappercontext.CreateConnection();

            string insertCampSql = @"
                INSERT INTO blood_camps 
                    (camp_name, location, start_date, end_date, description, created_by)
                VALUES
                    (@CampName, @Location, @StartDate, @EndDate, @Description, @CreatedBy)";

            var parameters = new
            {
                CampName = dto.CampName,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Description = dto.Description,
                CreatedBy = bankId
            };

            int rowsAffected = await connection.ExecuteAsync(insertCampSql, parameters);

            return rowsAffected > 0;
        }



        public async Task<bool> DeleteCamp(int campId)
        {
            string sql = "DELETE FROM blood_camps WHERE id = @CampId";
            var connection = _dappercontext.CreateConnection();
            int rows = await connection.ExecuteAsync(sql, new { CampId = campId });
            return rows > 0;
        }

        public async Task<IEnumerable<DonationDto>> GetDonations(int bankId)
        {
            string sql = "SELECT * FROM donations WHERE blood_bank_id = @BankId";
            var connection = _dappercontext.CreateConnection();
            return await connection.QueryAsync<DonationDto>(sql, new { BankId = bankId });
        }

        public async Task<bool> LogDonation(DonationDto dto)
        {
            string sql = @"INSERT INTO donations (donor_id, blood_bank_id, blood_group, units, donated_at)
                       VALUES (@DonorId, @BankId, @BloodGroup, @Units, @DonatedAt)";
            var connection = _dappercontext.CreateConnection(); 
            int rows = await connection.ExecuteAsync(sql, dto);
            return rows > 0;
        }
    }
}
