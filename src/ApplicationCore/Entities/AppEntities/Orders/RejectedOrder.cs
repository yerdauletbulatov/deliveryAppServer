namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class RejectedOrder : BaseEntity
    {
        public RouteTrip RouteTrip { get; set; }
        public Order Order { get; set; }
    }
}