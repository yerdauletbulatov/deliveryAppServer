using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using BackgroundTasks.Interfaces;
using BackgroundTasks.Model;

namespace BackgroundTasks.Service
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<BackgroundOrder> _queue;

        public BackgroundTaskQueue()
        {
            _queue = Channel.CreateUnbounded<BackgroundOrder>();
        }

        public async ValueTask QueueAsync(BackgroundOrder order)
            => await _queue.Writer.WriteAsync(order);


        public async ValueTask<BackgroundOrder> DequeueAsync(CancellationToken cancellationToken)
            => await _queue.Reader.ReadAsync(cancellationToken);
    }
}