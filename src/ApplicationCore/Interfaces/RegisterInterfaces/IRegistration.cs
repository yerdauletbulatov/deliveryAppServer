using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.RegisterInterfaces
{
    public interface IRegistration
    {
        public Task<ActionResult> SendTokenAsync(RegistrationInfo info);

        public Task<ActionResult> Confirm(ConfirmRegistrationInfo info, CancellationToken cancellationToken);
    }
}