using System;

namespace ApplicationCore.Interfaces
{
    public interface IValidation
    {
        bool ValidationMobileNumber(string number);
        bool ValidationCode(string code);
        bool ValidationDate(DateTime dateTime);
    }
}