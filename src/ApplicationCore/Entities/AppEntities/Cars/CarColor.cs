namespace ApplicationCore.Entities.AppEntities.Cars
{
    public class CarColor : BaseEntity
    {
        public CarColor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set;}
    }
}