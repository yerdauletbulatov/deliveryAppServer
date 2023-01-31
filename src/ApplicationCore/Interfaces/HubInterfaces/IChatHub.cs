using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.HubInterfaces
{
    public interface IChatHub
    {
        public Task ConnectedAsync(string userId, string connectId);
        public Task DisconnectedAsync(string connectId);
        public Task<string> GetConnectionIdAsync(string userId, CancellationToken cancellationToken);
        public Task<List<string>> GetConnectionIdListAsync(List<Order> orders);
    }
}