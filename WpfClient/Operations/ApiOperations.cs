using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Newtonsoft.Json;
using WpfClient.Models;

namespace WpfClient.Operations
{
    class ApiOperations
    {
        private string baseUrl;

        public ApiOperations()
        {
            this.baseUrl = "https://localhost:44373/api";
        }

        #region UserActions

        public User AuthenticateUser(string login, string password)
        {
            string endpoint = this.baseUrl + "/Auth/signin";
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
            string endpoint = this.baseUrl + "/Auth/signup";
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

        public ObservableCollection<User> GetAllUsers()
        {
            string endpoint = this.baseUrl + "/Users";
            string access_token = Globals.LoggedInUser.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<ObservableCollection<User>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User AddUser(string login, string password)
        {
            string endpoint = this.baseUrl + "/Users";
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
            string endpoint = this.baseUrl + "/Users/" + user.Id;
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
            string endpoint = this.baseUrl + "/Users/" + user.Id;
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

        public ObservableCollection<Role> GetAllRoles()
        {
            string endpoint = this.baseUrl + "/Roles";
            string access_token = Globals.LoggedInUser.access_token;

            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;
            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<ObservableCollection<Role>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddRole(Role role)
        {
            string endpoint = this.baseUrl + "/Roles/add/" + role.Name;
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

        public bool EditRole(Role role)
        {
            string endpoint = this.baseUrl + "/Roles/" + role.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "PUT";

            string json = JsonConvert.SerializeObject(new
            {
                Id = role.Id,
                Name = role.Name
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

        public bool DelRole(Role role)
        {
            string endpoint = this.baseUrl + "/Roles/" + role.Id;
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

        public bool EditRoleUser(User user, string role)
        {
            string endpoint = this.baseUrl + "/Roles/editUserRole/" + user.Id;
            string access_token = Globals.LoggedInUser.access_token;
            string method = "POST";
            List<string> roles = new List<string>();
            roles.Add(role);
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

        #endregion

        #region PhoneBookActions

        public ObservableCollection<PhoneBook> GetAllPhoneBooks()
        {
            string endpoint = this.baseUrl + "/PhoneBook";
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";

            try
            {
                string response = wc.DownloadString(endpoint);

                var result = JsonConvert.DeserializeObject<ObservableCollection<PhoneBook>>(response);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PhoneBook GetPhoneBook(int id)
        {
            string endpoint = this.baseUrl + "/PhoneBook/" + id;
            string access_token = Globals.LoggedInUser.access_token;
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            wc.Headers["Authorization"] = "Bearer " + access_token;

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
            string endpoint = this.baseUrl + "/PhoneBook";
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
            string endpoint = this.baseUrl + "/PhoneBook/" + phoneBook.Id;
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
            string endpoint = this.baseUrl + "/PhoneBook/" + phoneBook.Id;
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
