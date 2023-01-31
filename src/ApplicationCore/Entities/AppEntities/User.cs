using System;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.AppEntities
{
    public sealed class User : IdentityUser
    {
        public User(string userName, string phoneNumber, string  email, string name, string surname)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Name = name;
            Surname = surname;
        }
        public User(string phoneNumber, bool  isDriver)
        {
            PhoneNumber = phoneNumber;
            IsDriver = isDriver;
        }
        public string Name { get; private set; }
        public string Surname { get;private set; }
        public bool IsDriver { get;private set; }
        public bool IsValid { get;private set; }
        public bool IsDeleted { get;private set; }
        public string RefreshToken { get;private set; }
        public DateTime? RefreshTokenExpiryTime { get;private set; }



        public void AddRefreshToken(string refreshToken, DateTime refreshTokenExpiryTime)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
        public User AddFullName(string name, string surname)
        {
            Name = name;
            Surname = surname;
            IsValid = true;
            return this;
        }
    }
}