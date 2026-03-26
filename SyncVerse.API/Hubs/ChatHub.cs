using Microsoft.AspNetCore.SignalR;
using SyncVerse.Application.DTOs;
using System.Threading.Tasks;

namespace SyncVerse.API.Hubs
{
    public class ChatHub : Hub
    {
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
    }
}
