using System.Collections.Generic;

namespace WebApplicationClient.Models
{
    public class ChangeRoleViewModel
    {
        public string Id { get; set; }
        public string LoginProp { get; set; }
        public List<Role> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
