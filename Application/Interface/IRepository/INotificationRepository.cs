using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Application.Interface.IRepository
{
    public interface INotificationRepository
    {
        Task<bool> SaveNotificationAsync(Notification notification);
    }
}
