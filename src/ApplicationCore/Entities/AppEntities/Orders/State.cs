namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class State : BaseEntity
    {
        public State(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}