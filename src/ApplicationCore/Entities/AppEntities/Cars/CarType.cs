namespace ApplicationCore.Entities.AppEntities.Cars
{
    public class CarType : BaseEntity
    {
        public CarType(int id,string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }
    }
}