using System.Collections.Generic;

namespace WpfClient.Models
{
    class Globals
    {
        public static User LoggedInUser { get; set; }
        public static List<string> RoleUser { get; set; }

        public static bool IsAdmin()
        {
            return LoggedInUser != null && RoleUser.Contains("admin");
        }
    }
}
