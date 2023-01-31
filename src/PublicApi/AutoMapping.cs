using AutoMapper;
using ApplicationCore.Entities.Values;
using PublicApi.Commands;
using PublicApi.Endpoints.Clients.Order;
using PublicApi.Endpoints.Delivery;
using PublicApi.Endpoints.Drivers.Car;
using PublicApi.Endpoints.Drivers.RouteTrip;
using PublicApi.Endpoints.RegisterApi.ConfirmRegister;
using PublicApi.Endpoints.RegisterApi.ProceedRegister;
using PublicApi.Endpoints.RegisterApi.Register;

namespace PublicApi
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RegisterCommand, RegistrationInfo>();
            CreateMap<ConfirmRegisterCommand, ConfirmRegistrationInfo>();
            CreateMap<ProceedRegisterCommand, ProceedRegistrationInfo>();
            CreateMap<CreateDeliveryCommand, RouteTripInfo>();
            CreateMap<CarCommand, CarInfo>();
            CreateMap<CreateOrderCommand, OrderInfo>();
        }   
    }
}