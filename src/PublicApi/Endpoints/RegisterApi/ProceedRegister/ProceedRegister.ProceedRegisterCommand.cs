using System;

namespace PublicApi.Endpoints.RegisterApi.ProceedRegister
{
    public class ProceedRegisterCommand
    {
        public string PhoneNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string IdentificationNumber { get; set; }
        public string IdentificationSeries { get; set; }
        public DateTime IdentityCardCreateDate { get; set; }
        public string DriverLicenceScanPath { get; set; }
        public string IdentityCardPhotoPath { get; set; }
        public bool IsDriver { get; set; }
    }
}