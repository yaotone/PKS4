using System.ComponentModel.DataAnnotations;

namespace Pks4Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Логин")]
        public string login { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пароль")]
        public string password { get; set; }

    }
}
