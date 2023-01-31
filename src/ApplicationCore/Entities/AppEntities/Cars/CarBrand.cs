namespace ApplicationCore.Entities.AppEntities.Cars
{
    public class CarBrand : BaseEntity
    {
        public CarBrand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }
    }
}