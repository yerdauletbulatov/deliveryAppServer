using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.DeliveryInterfaces;
using ApplicationCore.Interfaces.HubInterfaces;
using MediatR;
using PublicApi.Helpers;

namespace PublicApi.Commands
{
    public class ConfirmOrderCommandHandler : AsyncRequestHandler<ConfirmOrderCommand>
    {
        private readonly IDelivery _delivery;
        private readonly HubHelper _hubHelper;


        public ConfirmOrderCommandHandler(IDelivery delivery, HubHelper hubHelper)
        {
            _delivery = delivery;
            _hubHelper = hubHelper;
        }

        protected override async Task Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order  = await _delivery.AddToDeliveryAsync(request.OrderId);
            await _hubHelper.SendToClient(order.Client.UserId, cancellationToken);
        }
    }
}