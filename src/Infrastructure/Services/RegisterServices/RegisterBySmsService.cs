using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.RegisterInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.RegisterServices
{
    public class RegisterBySmsService : IRegistration
    {
        public Task<ActionResult> SendTokenAsync(RegistrationInfo info)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActionResult> Confirm(ConfirmRegistrationInfo info, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}