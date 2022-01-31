using System.ComponentModel.DataAnnotations;

namespace WebApplicationClient.Models
{
    public class CreateUserViewModel
    {
        [Display(Name = "Логин")]
        public string LoginProp { get; set; }

        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
