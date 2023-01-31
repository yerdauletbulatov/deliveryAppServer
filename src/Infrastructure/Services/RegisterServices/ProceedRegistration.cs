using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.Values;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.RegisterInterfaces;
using Infrastructure.AppData.DataAccess;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.RegisterServices
{
    public class ProceedRegistrationService : IProceedRegistration
    {
        private readonly AppIdentityDbContext _identityDb;
        private readonly IContext _context;

        public ProceedRegistrationService(AppIdentityDbContext identityDb, IContext context)
        {
            _identityDb = identityDb;
            _context = context;
        }

        public async Task<ActionResult> ProceedRegistration(ProceedRegistrationInfo info, string userId,
            CancellationToken cancellationToken)
        {
            var user = (await _identityDb.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken))
                       .AddFullName(info.Name, info.Surname) ??
                       throw new NotExistUserException("User is not found");
            if (user.IsDriver)
            {
                var driver = new Driver(user.Id, info.IdentificationNumber, info.IdentificationSeries,
                    info.IdentityCardCreateDate, info.DriverLicenceScanPath, info.IdentityCardPhotoPath);
                await _context.AddAsync(driver);
            }
            else
            {
                await _context.AddAsync(new Client(user.Id));
            }

            _identityDb.Users.Update(user);
            await _identityDb.SaveChangesAsync(cancellationToken);
            return new OkObjectResult(new { name = user.Name, surname = user.Surname });
        }
    }
}