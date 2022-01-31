using System.ComponentModel.DataAnnotations;

namespace WebApplicationClient.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(20)]
        [Display(Name = "Логин")]
        public string LoginProp { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [Display(Name = "Подтвердите пароль")]

        public string ConfirmPassword { get; set; }
    }
}
