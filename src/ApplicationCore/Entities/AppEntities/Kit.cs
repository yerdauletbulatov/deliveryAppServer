namespace ApplicationCore.Entities.AppEntities
{
    public class Kit : BaseEntity
    {
        public Kit(int id, string name, int quantity, bool isUnlimited)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            IsUnlimited = isUnlimited;
        }

        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public bool IsUnlimited { get; private set; }
    }
}