using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;

            if (user?.Identity?.IsAuthenticated ?? false)
            {
               
                string role = user.FindFirst(ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, role);
                }

            
                string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
          
            await base.OnDisconnectedAsync(exception);
        }


        public async Task SendMessage(int userId, string message)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveMessage", message);
        }
        public async Task SendToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveNotification", message);
        }
    }
}
