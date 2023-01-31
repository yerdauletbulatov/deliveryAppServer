namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class Package : BaseEntity
    {
        public Package(string name, double length, double width, double height, double weight)
        {
            Name = name;
            Length = length;
            Width = width;
            Height = height;
            Weight = weight;
        }

        public string Name { get; private set;}
        public double Length { get; private set;}
        public double Width { get; private set;}
        public double Height { get; private set;}
        public double Weight { get; private set;}
    }
}