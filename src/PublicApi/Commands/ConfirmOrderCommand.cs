using MediatR;

namespace PublicApi.Commands
{
    public class ConfirmOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}