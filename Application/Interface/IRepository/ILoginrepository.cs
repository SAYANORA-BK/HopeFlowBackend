using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interface.IRepository
{
 public   interface ILoginrepository
    {
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByGoogleIdAsync(int googleId);
         Task<bool> RegisterGoogleUserAsync(UserDto newUser);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
    }
}
