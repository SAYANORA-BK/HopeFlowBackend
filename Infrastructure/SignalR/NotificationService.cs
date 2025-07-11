using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.IRepository;
using Application.Interface.IService;
using Domain.Model;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IHubContext<NotificationHub> hubContext,INotificationRepository notificationRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task SendNotificationAsync(int userId, string message)
        {
            
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message);

            
            var notification = new Notification
            {
                SentBy = userId, 
                Message = message,
                TargetRole = "Donor,BloodBank", 
                SentAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.SaveNotificationAsync(notification);
        }

    }
}
