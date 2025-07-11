using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(int userId, string message)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
