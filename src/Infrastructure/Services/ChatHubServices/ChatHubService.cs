using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.Values.Enums;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.HubInterfaces;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ChatHubServices
{
    public class ChatHubService : IChatHub
    {
        private readonly IContext _context;

        public ChatHubService(IContext context)
        {
            _context = context;
        }

        public async Task ConnectedAsync(string userId, string connectId)
        {
            var chatHub = await _context.FindAsync<ChatHub>(c => c.UserId == userId)
                          ?? await CreateChatHubAsync(userId, connectId);
            await _context.UpdateAsync(chatHub.UpdateConnectId(connectId));
        }

        public async Task<string> GetConnectionIdAsync(string userId, CancellationToken cancellationToken)
        {
            var chatHub = await _context.FindAsync<ChatHub>(c => c.UserId == userId);
            return chatHub?.ConnectionId;
        }

        public async Task<List<string>> GetConnectionIdListAsync(List<Order> orders)
        {
            var connectionIds = new List<string>();
            foreach (var order in orders)
            {
                var chatHub = await  _context.FindAsync<ChatHub>(c => c.UserId == order.Client.UserId);
                connectionIds.Add(chatHub?.ConnectionId);
            }
            return connectionIds;
        }

        public async Task DisconnectedAsync(string connectId)
        {
            var chatHub = await _context.FindAsync<ChatHub>(c => c.ConnectionId == connectId);
            await _context.UpdateAsync(chatHub.RemoveConnectId());
        }
        
        private async Task<ChatHub> CreateChatHubAsync(string userId, string connectId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new NullReferenceException();
            }
            var chatHub = new ChatHub(userId, connectId);
            await _context.AddAsync(chatHub);
            return chatHub;
        }
    }
}