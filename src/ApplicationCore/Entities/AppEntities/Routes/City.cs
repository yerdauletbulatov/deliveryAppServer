namespace ApplicationCore.Entities.AppEntities.Routes
{
    public class City : BaseEntity
    {
        public City(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}