namespace ApplicationCore.Entities.AppEntities
{
    public class Client : BaseEntity
    {
        public Client(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set;}
    }
}