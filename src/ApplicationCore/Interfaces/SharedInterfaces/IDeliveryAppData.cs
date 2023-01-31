using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.SharedInterfaces
{
    public interface IDeliveryAppData<T>
    {
        public Task<ActionResult> SendDataAsync(CancellationToken cancellationToken);
    }
}