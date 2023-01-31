using System;
using ApplicationCore.Entities;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.Values;

namespace Infrastructure
{
    public static class AutoMappingExtensions
    {
        public static RouteTripInfo GetRouteTripInfo(this RouteTrip routeTrip) =>
            new()
            {
                StartCity = routeTrip.Route.StartCity,
                FinishCity = routeTrip.Route.FinishCity,
                DeliveryDate = routeTrip.DeliveryDate,
            };    

        public static OrderInfo GetOrderInfo(this Order order, User client) =>
            new()
            {
                OrderId = order.Id,
                StartCity = order.Route.StartCity,
                FinishCity = order.Route.FinishCity,
                Package = order.Package,
                CarType = order.CarType,
                IsSingle = order.IsSingle,
                Price = order.Price,
                StateName = order.State.Name,
                DeliveryDate = order.Delivery?.RouteTrip?.DeliveryDate ?? order.DeliveryDate,
                Location = order.Location,
                ClientName = client.Name,
                ClientSurname = client.Surname,
                ClientPhoneNumber = client.PhoneNumber
            };

        public static DeliveryInfo GetDeliveryInfo(this Order order, User client, User driver) =>
            new()
            {
                OrderId = order.Id,
                StartCity = order.Route.StartCity,
                FinishCity = order.Route.FinishCity,
                Package = order.Package,
                CarType = order.CarType,
                IsSingle = order.IsSingle,
                Price = order.Price,
                StateName = order.State.Name,
                DeliveryDate = order.Delivery?.RouteTrip?.DeliveryDate ?? order.DeliveryDate,
                Location = order.Location,
                ClientName = client.Name,
                ClientSurname = client.Surname,
                ClientPhoneNumber = client.PhoneNumber,
                DriverPhoneNumber = driver.PhoneNumber,
                DriverName = driver.Name,
                DriverSurname = driver.Surname
            };
    }
}