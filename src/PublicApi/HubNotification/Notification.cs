using System;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.HubInterfaces;
using Infrastructure.Config.Attributes;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PublicApi.Commands;

namespace PublicApi.HubNotification
{
    [Authorize]
    public class Notification : Hub
    {
        private readonly IChatHub _chatHub;
        private readonly IMediator _mediator;
        private readonly ILogger<Notification> _logger;

        public Notification(IChatHub chatHub, IMediator mediator, ILogger<Notification> logger)
        {
            _chatHub = chatHub;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task ReceiveDriverLocation(LocationCommand request)
        {
            await _mediator.Send(request.SetUserId(Context.GetHttpContext().Items["UserId"]?.ToString()));
            _logger.LogInformation($"{request.DriverName} : {DateTime.Now:G}");
        }
        
        
        public override async Task<Task> OnConnectedAsync()
        {
            await _chatHub.ConnectedAsync(Context.GetHttpContext().Items["UserId"]?.ToString(), Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override async Task<Task> OnDisconnectedAsync(Exception exception)
        {
            await _chatHub.DisconnectedAsync(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}