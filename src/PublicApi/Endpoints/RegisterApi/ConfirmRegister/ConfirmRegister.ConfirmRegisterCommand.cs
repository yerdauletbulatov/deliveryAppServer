using System.ComponentModel.DataAnnotations;

namespace PublicApi.Endpoints.RegisterApi.ConfirmRegister
{
    public class ConfirmRegisterCommand
    {
        [Required]
        public string SmsCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool IsDriver { get; set; }
    }
}