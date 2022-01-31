using System.ComponentModel.DataAnnotations;

namespace WebApplicationClient.Models
{
    public class LoginViewModel
    {
        [Required, MaxLength(20)]
        [Display(Name = "Логин")]
        public string LoginProp { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
