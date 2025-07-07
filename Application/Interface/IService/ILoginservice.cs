using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using ommon;

namespace Application.Interface.IService
{
  public   interface ILoginservice
    {
        Task<ApiresponseDto<object>> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetOrCreateGoogleUserAsync(string email, string fullName, int googleId);
    }
}
