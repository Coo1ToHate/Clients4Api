using System.ComponentModel.DataAnnotations;

namespace WebApplicationClient.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Логин")]
        public string LoginProp { get; set; }
    }
}
