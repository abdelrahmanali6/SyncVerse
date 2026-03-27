using Microsoft.AspNetCore.SignalR;
using SyncVerse.Application.DTOs;
using System.Threading.Tasks;

namespace SyncVerse.API.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly HashSet<string> OnlineUsers = new();

        public async Task SendMessageToChannel(MessageDto message)
        {
            await Clients.Group(message.ChannelId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinChannel(string channelId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId);
        }

        public async Task LeaveChannel(string channelId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                lock (OnlineUsers)
                {
                    OnlineUsers.Add(userId);
                }
                await Clients.All.SendAsync("UserPresenceChanged", new PresenceDto { UserId = userId, IsOnline = true });
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                lock (OnlineUsers)
                {
                    OnlineUsers.Remove(userId);
                }
                await Clients.All.SendAsync("UserPresenceChanged", new PresenceDto { UserId = userId, IsOnline = false });
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
