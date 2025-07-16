using System.Threading.Tasks;
using Application.DTO;
using Application.Interface.IRepository;
using Dapper;
using Infrastructure.DapperContext;
using BCrypt.Net;

namespace Infrastructure.Repositories
{
    public class LoginRepository :  ILoginrepository
    {
        private readonly Dappercontext _dapperContext;

        public LoginRepository(Dappercontext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var sql = "SELECT u.*, r.role_name FROM users u JOIN roles r ON u.role_id = r.Role_id WHERE u.email = @Email ";

            using var connection = _dapperContext.CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<UserDto>(sql, new { Email = email });
            Console.WriteLine(user.role_name);
            return user;
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.hashpassword);
        }
        public async Task<UserDto> GetUserByGoogleIdAsync(int googleId)
        {
            var sql = "SELECT * FROM users WHERE google_id = @GoogleId";
            using var conn = _dapperContext.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<UserDto>(sql, new { GoogleId = googleId });
        }
        public async Task<bool> RegisterGoogleUserAsync(UserDto user)
        {
            var sql = @"
                INSERT INTO users (full_name, email, google_id, role_id)
                VALUES (@full_name, @email, @google_id, @role_id)";
            using var conn = _dapperContext.CreateConnection();
            var result = await conn.ExecuteAsync(sql, user);
            return result > 0;
        }

    }
}
