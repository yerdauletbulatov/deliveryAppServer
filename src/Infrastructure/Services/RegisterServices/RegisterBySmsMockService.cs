using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.RegisterInterfaces;
using ApplicationCore.Interfaces.TokenInterfaces;
using Infrastructure.AppData.DataAccess;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.RegisterServices
{
    public class RegisterBySmsMockService : IRegistration
    {
        private readonly IGenerateToken _generateToken;
        private readonly AppIdentityDbContext _identityDb;

        public RegisterBySmsMockService(AppIdentityDbContext identityDb, IGenerateToken generateToken)
        {
            _identityDb = identityDb;
            _generateToken = generateToken;
        }

        public async Task<ActionResult> SendTokenAsync(RegistrationInfo info) => 
            await RandomSms(info);

        private Task<ActionResult> RandomSms(RegistrationInfo info) =>
            Task.FromResult<ActionResult>(new OkObjectResult(info));
        
        public async Task<ActionResult> Confirm(ConfirmRegistrationInfo info, CancellationToken cancellationToken)
        {
             var user = await _identityDb.Users.FirstOrDefaultAsync(
                u => u.PhoneNumber == info.PhoneNumber && u.IsDriver == info.IsDriver, cancellationToken);
            if (user is null)
            {
                user = new User(info.PhoneNumber, info.IsDriver);
                await _identityDb.Users.AddAsync(user, cancellationToken);
                await _identityDb.SaveChangesAsync(cancellationToken);
            }

            user.AddRefreshToken(_generateToken.CreateRefreshToken(), DateTime.UtcNow.AddYears(_generateToken.LifeTimeRefreshTokenInYear));
            _identityDb.Users.Update(user);
            await _identityDb.SaveChangesAsync(cancellationToken);

            return new OkObjectResult(new{accessToken = _generateToken.CreateAccessToken(user), 
                refreshToken = user.RefreshToken, name = user.Name, surname = user.Surname, isValid = user.IsValid});
        }
    }
}