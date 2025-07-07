using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTO;
using Application.Helpers;
using Application.Interface.IRepository;
using Application.Interface.IService;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ommon;

namespace Application.Service
{
    public class LoginService : ILoginservice
    {
        private readonly ILoginrepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly IRolerepository _roleRepository;

        public LoginService(ILoginrepository loginRepository, IConfiguration configuration, IRolerepository roleRepository)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
        }

        public async Task<ApiresponseDto<object>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _loginRepository.GetUserByEmailAsync(loginDto.email);

                if (user == null)
                {
                    return new ApiresponseDto<object>
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }

                var isPasswordValid = PasswordHasher.Verify(loginDto.password, user.hashpassword);

                if (!isPasswordValid)
                {
                    return new ApiresponseDto<object>
                    {
                        StatusCode = 401,
                        Message = "Invalid credentials"
                    };
                }

                var roleName = await _roleRepository.GetRolesAsyncById(Convert.ToInt32(user.role));

                var domainUser = new User
                {
                    Id = user.Id,
                    FullName = user.full_name,
                    Email = user.email,
                    RoleId = Convert.ToInt32(user.role)
                };

                var token = await GenerateTokenAsync(domainUser);

                return new ApiresponseDto<object>
                {
                    StatusCode = 200,
                    Message = "Login successful",
                    Data = new
                    {
                        user.full_name,
                        user.email,
                        role = roleName,
                        token
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiresponseDto<object>
                {
                    StatusCode = 500,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<UserDto> GetOrCreateGoogleUserAsync(string email, string fullName, int googleId)
        {
            var user = await _loginRepository.GetUserByGoogleIdAsync(googleId);
            if (user != null)
                return user;

            var newUser = new UserDto
            {
                email = email,
                full_name = fullName,
                google_id = googleId,
                role = "", 
                hashpassword = "" 
            };

            await _loginRepository.RegisterGoogleUserAsync(newUser);
            return newUser;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var roleName = await _roleRepository.GetRolesAsyncById(user.RoleId);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
               
               
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Authentication:Jwt:Issuer"],
                Audience = _configuration["Authentication:Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
