using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не был указан Логин")]
        public string Login { get; set; }
        
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Не верно указан номер телефона")]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронный адрес")]
        [Required(ErrorMessage = "Не был указан Email")]
        [Remote(action:"CheckEmail", controller: "Validation", 
            ErrorMessage = "email уже зарегистрирован", AdditionalFields = "Id")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Электронный пароль")]
        [Required(ErrorMessage = "Не был указан пароль")]
        public string Password { get; set; }
       
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтверждение пароля")]
        [Required(ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
     
    }
}