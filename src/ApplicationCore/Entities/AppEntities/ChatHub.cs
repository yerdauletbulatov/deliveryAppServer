namespace ApplicationCore.Entities.AppEntities
{
    public class ChatHub : BaseEntity
    {
        public string UserId { get; private set;}
        public string ConnectionId { get; private set; }

        public ChatHub(string userId, string connectionId)
        {
            UserId = userId;
            ConnectionId = connectionId;
        }

        public ChatHub UpdateConnectId(string connectionId)
        {
            ConnectionId = connectionId;
            return this;
        }
        public ChatHub RemoveConnectId()
        {
            ConnectionId = string.Empty;
            return this;
        }
    }
}