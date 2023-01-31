using MediatR;

namespace PublicApi.Commands
{
    public class LocationCommand : IRequest
    {
        public double Latitude { get;  set; }
        public double Longitude { get;  set; }
        public string UserId { get; private set; }
        public string DriverName { get;  set; }
        public string DriverSurname { get;  set; }
        public string DriverPhoneNumber { get;  set; }
        public LocationCommand SetUserId(string userId)
        {
            UserId = userId;
            return this;
        }
    }
}