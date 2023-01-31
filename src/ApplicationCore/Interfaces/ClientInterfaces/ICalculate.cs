using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.ClientInterfaces
{
    public interface ICalculate
    {
        public Task<ActionResult> CalculateAsync(OrderInfo info,CancellationToken cancellationToken);
    }
}