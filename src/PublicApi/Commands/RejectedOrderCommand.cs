using System;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.AppEntities.Routes;
using MediatR;

namespace PublicApi.Commands
{
    public class RejectedOrderCommand : IRequest    
    {
        public int OrderId { get; set; }
    }
}