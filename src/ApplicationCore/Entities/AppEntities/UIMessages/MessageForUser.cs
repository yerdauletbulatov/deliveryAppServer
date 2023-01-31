namespace ApplicationCore.Entities.AppEntities.UIMessages
{
    public class MessageForUser : BaseEntity
    {
        public MessageForUser(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Description { get; private set; }
    }
}