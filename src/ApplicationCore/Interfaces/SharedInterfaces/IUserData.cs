using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.SharedInterfaces
{
    public interface IUserData
    {
        public Task<ActionResult> GetDataAsync(string userId, CancellationToken cancellationToken);
    }
}