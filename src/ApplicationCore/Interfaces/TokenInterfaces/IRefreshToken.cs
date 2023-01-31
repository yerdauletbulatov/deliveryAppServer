using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.TokenInterfaces
{
    public interface IRefreshToken
    {
        public Task<ActionResult> RefreshTokenAsync(RefreshTokenInfo tokenInfo);
    }
}    