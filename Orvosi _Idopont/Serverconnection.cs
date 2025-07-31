using System;
using System.Collections.Generic;
using System.Configuration;
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
            baseUrl = "http://127.0.0.1:3000";
        }

        private async Task<object> Connection(string urlString, string methodType, string jsonString = null)
        {
            authHead();

            string url = baseUrl + urlString;
            if ((methodType.ToLower() == "get" || methodType.ToLower() == "post" || methodType.ToLower() == "delete") && jsonString != null)
            {
                MessageBox.Show("Function wroking well");
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
                    response.EnsureSuccessStatusCode();
                    return responseText;
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrWhiteSpace(responseText))

                    try
                    {
                        string message = JsonConvert.DeserializeObject<Message>(responseText).message;
                        MessageBox.Show(message, "hiba");
                    }
                    catch
                {

                        MessageBox.Show("Hiba tortent aza fasyz");

                }
                else
                {
                    MessageBox.Show("Ismereten hiba tortent", "hiba");
                }
            }

            

            return null;
        }

        private void authHead()
        {
            if (!string.IsNullOrEmpty(Token.token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            }
        }


        public async Task<bool> Login(string username, string password)
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

       
                LoginInfo responseData = JsonConvert.DeserializeObject<LoginInfo>(response as string);
                Token.token = responseData.token;

                authHead();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "hiba");
                return false;
            }
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

        public async Task<List<Appointment>> Appointment(string name, string username, string email)
        {
           List<Appointment> all = new List<Appointment>();

            try
            {
                object response = await Connection("/appointments", "get");
                if(response == null) return all;

                all= JsonConvert.DeserializeObject<List<Appointment>>(response as string);

            }

            catch(Exception ex)
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
                object response = await Connection("/auth/register", "post", data);
                if (response == null) return false;

                Userprofile users = JsonConvert.DeserializeObject<Userprofile>(response as string);
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
            string url = $"/users/{id}";

            try
            {
                object response = await Connection(url, "delete");
                if (response == null) return false;

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
