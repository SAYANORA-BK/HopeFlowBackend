using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using MediatR;

namespace Application.Interface.IService
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, string message);
    }
}
