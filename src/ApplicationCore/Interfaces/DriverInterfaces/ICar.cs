using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.DriverInterfaces
{
    public interface ICar
    {
        public Task<ActionResult> CreateAsync(CarInfo create, string userId, CancellationToken cancellationToken);
    }
}