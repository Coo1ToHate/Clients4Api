using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplicationClient.Models;

namespace WebApplicationClient.Operations
{
    public class ApiOperations
    {
        private string baseUrl;

        public ApiOperations()
        {
            this.baseUrl = "https://localhost:44373/api";
        }

        #region PhoneBookActions

        public async Task<List<PhoneBook>> GetAllPhoneBooks()
        {
            string endpoint = baseUrl + "/PhoneBook";
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";

            try
            {
                string response = await wc.DownloadStringTaskAsync(endpoint);

                var result = JsonConvert.DeserializeObject<List<PhoneBook>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PhoneBook GetPhoneBook(int id)
        {
            string endpoint = baseUrl + "/PhoneBook/" + id;
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";

            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<PhoneBook>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddPhoneBook(PhoneBook phoneBook)
        {
            string endpoint = baseUrl + "/PhoneBook";
            string access_token = Globals.LoggedInUser.access_token;
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                LastName = phoneBook.LastName,
                FirstName = phoneBook.FirstName,
                MiddleName = phoneBook.MiddleName,
                NumberPhone = phoneBook.NumberPhone,
                Address = phoneBook.Address,
                Desc = phoneBook.Desc
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditPhoneBook(PhoneBook phoneBook)
        {
            string endpoint = baseUrl + "/PhoneBook/" + phoneBook.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "PUT";
            string json = JsonConvert.SerializeObject(new
            {
                Id = phoneBook.Id,
                LastName = phoneBook.LastName,
                FirstName = phoneBook.FirstName,
                MiddleName = phoneBook.MiddleName,
                NumberPhone = phoneBook.NumberPhone,
                Address = phoneBook.Address,
                Desc = phoneBook.Desc
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DelPhoneBook(PhoneBook phoneBook)
        {
            string endpoint = baseUrl + "/PhoneBook/" + phoneBook.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "DELETE";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, string.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region UserActions

        public User AuthenticateUser(string login, string password)
        {
            string endpoint = baseUrl + "/Auth/signin";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = login,
                password = password
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User RegisterUser(string login, string password)
        {
            string endpoint = baseUrl + "/Auth/signup";
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = login,
                password = password,
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            string endpoint = baseUrl + "/Users";
            string access_token = Globals.LoggedInUser.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<List<User>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUser(string id)
        {
            string endpoint = baseUrl + "/Users/" + id;
            string access_token = Globals.LoggedInUser.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;
            
            try
            {
                string response = wc.DownloadString(endpoint);
                var result = JsonConvert.DeserializeObject<User>(response);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User AddUser(string login, string password)
        {
            string endpoint = baseUrl + "/Users";
            string access_token = Globals.LoggedInUser.access_token;
            string method = "POST";
            string json = JsonConvert.SerializeObject(new
            {
                username = login,
                password = password,
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return JsonConvert.DeserializeObject<User>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool EditUser(User user)
        {
            string endpoint = baseUrl + "/Users/" + user.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "PUT";
            string json = JsonConvert.SerializeObject(new
            {
                Id = user.Id,
                username = user.UserName
            });

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DelUser(User user)
        {
            string endpoint = baseUrl + "/Users/" + user.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "DELETE";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, string.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region RoleActions

        public List<string> RoleUsers(User user)
        {
            string endpoint = this.baseUrl + "/Roles/" + user.Id;
            string access_token = Globals.LoggedInUser.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<List<string>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Role> GetAllRoles()
        {
            string endpoint = baseUrl + "/Roles";
            string access_token = Globals.LoggedInUser.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;
            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<List<Role>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddRole(string name)
        {
            string endpoint = baseUrl + "/Roles/add/" + name;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "POST";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, string.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditRoleUser(User user, List<string> roles)
        {
            string endpoint = baseUrl + "/Roles/editUserRole/" + user.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "POST";
            string json = JsonConvert.SerializeObject(roles);

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DelRole(string idRole)
        {
            string endpoint = this.baseUrl + "/Roles/" + idRole;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "DELETE";

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.UploadString(endpoint, method, string.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
