using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Orvosi__Idopont
{
    public class Serverconnection
    {
        HttpClient client = new HttpClient();
        string baseUrl = string.Empty;

        public Serverconnection()
        {
            baseUrl = "http://127.1.1.1:3000";
        }

        private async Task<object> Connection(string urlStirng, string methodType, string jsonString = null)
        {
            authHead();
            string url = baseUrl + urlStirng;

            if ((methodType.ToLower() == "get" || methodType.ToLower() == "post" || methodType.ToLower() == "delete") && jsonString != null)
            {
                MessageBox.Show("kosenanat");
                return null;
            }

            string responseText = string.Empty;

            try
            {
                HttpResponseMessage response = new HttpResponseMessage();

                if (jsonString != null)
                {
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    if (methodType.ToLower() == "post")
                    {
                        response = await client.PostAsync(url, content);
                    }
                    else if (methodType.ToLower() == "put")
                    {
                        response = await client.PutAsync(url, content);
                    }
                }
                else
                {
                    if (methodType.ToLower() == "get")
                    {
                        response = await client.GetAsync(url);
                    }
                    else if (methodType.ToLower() == "delete")
                    {
                        response = await client.DeleteAsync(url);
                    }
                }

                if (response.Content != null)
                {
                    responseText = await response.Content.ReadAsStringAsync();
                }

                response.EnsureSuccessStatusCode();
                return responseText;
            }
            catch (Exception)
            {
                string message = JsonConvert.DeserializeObject<Message>(responseText).message;
                MessageBox.Show(message, "hiba");
            }

            return null;
        }



        private void authHead()
        {
            if (client.DefaultRequestHeaders.Authorization == null && !string.IsNullOrEmpty(Token.token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer" + Token.token);
            }
        }

        public async Task<bool> login(string username, string password)
        {
            try
            {
                var json = new
                {
                    loginUsername = username,
                    loginPassword = password
                };

                string data = JsonConvert.SerializeObject(json);
                object response = await Connection("/auth/login", "post", data);
                if (response == null)
                    return false;

                LoginInfo responsData = JsonConvert.DeserializeObject<LoginInfo>(response as string);
                Token.token = responsData.token;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "hiba");
                return false;
            }

            return true;
        }

        public async Task<List<Userprofile>> GetUserprofiles()
        {
            List<Userprofile> users = new List<Userprofile>();

            try
            {
                object response = await Connection("/users/all", "get");
                if (response == null)
                    return users;

                users = JsonConvert.DeserializeObject<List<Userprofile>>(response as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "hiba");
            }

            return users;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            List<Appointment> all = new List<Appointment>();

            try
            {
                object request = await Connection("/appointments", "get");

                if (request == null)
                    return all;

                all = JsonConvert.DeserializeObject<List<Appointment>>(request as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "hiba");
            }

            return all;
        }

        public async Task<bool> Registration(string username, string password, string email, string role, string fullname)
        {
            try
            {
                var json = new
                {
                    RegisterUsername = username,
                    RegisterPassword = password,
                    RegisterEmail = email,
                    fullname = fullname,
                    role = role
                };

                string data = JsonConvert.SerializeObject(json);
                object request = await Connection("/auth/register/", "post", data);
                if (request == null)
                    return false;

                Userprofile responsedata = JsonConvert.DeserializeObject<Userprofile>(request as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "hiba");
                return false;
            }

            return true;
        }

        public async Task<bool> Deleteone(int id)
        {
            try
            {
                string url = $"/users/{id}";
                object request = await Connection(url,"delete" );
                if (request == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "hiba");
            }

            return false;
        }
    }
}
