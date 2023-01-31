using ApplicationCore.Entities;
using ApplicationCore.Entities.AppEntities;

namespace ApplicationCore.Interfaces.TokenInterfaces
{
    public interface IGenerateToken
    {
        public string CreateRefreshToken();
        public string CreateAccessToken(User user);
        public int LifeTimeRefreshTokenInYear { get; }

    }
}