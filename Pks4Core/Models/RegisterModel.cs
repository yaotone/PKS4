using System.ComponentModel.DataAnnotations;

namespace Pks4Core.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Фамилия")]
        public string second_name { get; set;}

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Отчество")]
        public string third_name { get; set;}

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Логин")]
        public string login {  get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пароль")]
        public string password { get; set; }

    }
}
