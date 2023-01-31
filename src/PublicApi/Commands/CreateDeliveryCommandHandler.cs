using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.ClientInterfaces;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.DriverInterfaces;
using ApplicationCore.Interfaces.HubInterfaces;
using AutoMapper;
using MediatR;
using PublicApi.Helpers;

namespace PublicApi.Commands
{
    public class CreateDeliveryCommandHandler : AsyncRequestHandler<CreateDeliveryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRouteTrip _routeTrip;
        private readonly IOrder _order;
        private readonly HubHelper _hubHelper;

        public CreateDeliveryCommandHandler(IMapper mapper, IRouteTrip routeTrip, IOrder order, HubHelper hubHelper)
        {
            _mapper = mapper;
            _routeTrip = routeTrip;
            _order = order;
            _hubHelper = hubHelper;
        }

        protected override async Task Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _routeTrip.CreateAsync(_mapper.Map<RouteTripInfo>(request), request.UserId);
            if (!await _order.AnyWaitingOrdersAsync(delivery))
            {
                return;
            }
            await _hubHelper.SendToDriverAsync(request.UserId, cancellationToken);
        }
    }
}