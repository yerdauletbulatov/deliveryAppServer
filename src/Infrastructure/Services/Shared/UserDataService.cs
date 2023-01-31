using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.SharedInterfaces;
using Infrastructure.AppData.DataAccess;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Shared
{
    public class UserDataService : IUserData
    {
        private readonly AppIdentityDbContext _dbIdentity;
        private readonly IContext _context;

        public UserDataService(AppIdentityDbContext dbIdentity, IContext context)
        {
            _dbIdentity = dbIdentity;
            _context = context;
        }

        public async Task<ActionResult> GetDataAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _dbIdentity.Users.FirstAsync(u => u.Id == userId, cancellationToken);
            if (user.IsDriver)
            {
                var driver = await _context
                    .Drivers()
                    .IncludeCarBuilder()
                    .FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
                return new ObjectResult(driver);
            }
            
            var client = await _context.FindAsync<Client>(c => c.UserId == userId);
            return new OkObjectResult(client);
        }
    }
}