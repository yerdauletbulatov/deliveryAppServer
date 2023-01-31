using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не был указан Логин")]
        public string Login { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Электронный пароль")]
        [Required(ErrorMessage = "Не был указан пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}