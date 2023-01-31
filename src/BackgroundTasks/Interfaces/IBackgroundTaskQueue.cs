using System.Threading;
using System.Threading.Tasks;
using BackgroundTasks.Model;

namespace BackgroundTasks.Interfaces
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueAsync(BackgroundOrder order);
        ValueTask<BackgroundOrder> DequeueAsync(CancellationToken cancellationToken);
    }
}