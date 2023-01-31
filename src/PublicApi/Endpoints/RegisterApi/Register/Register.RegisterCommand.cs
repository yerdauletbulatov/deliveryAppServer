using System.ComponentModel.DataAnnotations;

namespace PublicApi.Endpoints.RegisterApi.Register
{
    public class RegisterCommand
    {
        [Required]
        public string PhoneNumber { get; set; } = null!;
    }
}