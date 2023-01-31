using System;
using System.Text.RegularExpressions;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class ValidationService : IValidation
    {
        private const string SmsCodePattern = @"[0-9]{4}";
        private const string PhoneNumberPattern = @"^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$";
        private const int SmsCodeLength = 4;
        
        public bool ValidationMobileNumber(string phoneNumber) => new Regex(PhoneNumberPattern).IsMatch(phoneNumber);
        public bool ValidationCode(string code) => new Regex(SmsCodePattern).IsMatch(code) && code.Length == SmsCodeLength;
        public bool ValidationDate(DateTime dateTime) => dateTime >= DateTime.Today;

    }
}