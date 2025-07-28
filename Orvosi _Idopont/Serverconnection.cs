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
        private readonly HttpClient client = new HttpClient();
        private readonly string baseUrl = string.Empty;

        public Serverconnection()
        {
            baseUrl = "http://127.1.1.1:3000";
        }

        private async Task<object> Connection(string methodType, string urlString, string jsonString = null)
        {
            authHeader();
            string url = baseUrl + urlString;

            string responseText = null;

            if ((methodType.ToLower() == "get" || methodType.ToLower() == "post" || methodType.ToLower() == "delete") && jsonString != null)
            {
                MessageBox.Show("get,post and delete is working");
                return null;
            }

            responseText = string.Empty;

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
                    if (methodType.ToLower() == "delete")
                    {
                        response = await client.DeleteAsync(url);
                    }
                    else if (methodType.ToLower() == "get")
                    {
                        response = await client.GetAsync(url);
                    }
                }

                responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Hiba a szerver válaszában: {response.StatusCode}\n{responseText}", "hiba");
                    return null;
                }

                return responseText;
            }
            catch (Exception)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(responseText) && responseText.TrimStart().StartsWith("{"))
                    {
                        string message = JsonConvert.DeserializeObject<Message>(responseText)?.message;
                        MessageBox.Show(message ?? "ismeretlen hiba", "hiba");
                    }
                    else
                    {
                        MessageBox.Show("nem json válasz érkezett a szervertől", "hiba");
                    }
                }
                catch
                {
                    MessageBox.Show("hiba történt a hibaüzenet feldolgozása során", "hiba");
                }
            }

            return null;
        }

        private void authHeader()
        {
            if (client.DefaultRequestHeaders.Authorization == null && !string.IsNullOrEmpty(Token.token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
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
                object response = await Connection("post", "/auth/login", data);

                if (response == null)
                    return false;

                LoginInfo responseData = JsonConvert.DeserializeObject<LoginInfo>(response as string);
                Token.token = responseData.token;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "hiba");
                return false;
            }

            return true;
        }

        public async Task<List<Userprofile>> GetUserprofiles()
        {
            List<Userprofile> users = new List<Userprofile>();

            try
            {
                object request = await Connection("get", "/users/all");

                if (request == null)
                    return users;

                users = JsonConvert.DeserializeObject<List<Userprofile>>(request as string);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "hiba");
            }

            return users;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            List<Appointment> all = new List<Appointment>();

            try
            {
                object request = await Connection("get", "/appointments");

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
                object response = await Connection("post", "/auth/register", data);

                if (response == null)
                    return false;

                Userprofile responseData = JsonConvert.DeserializeObject<Userprofile>(response as string);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "hiba");
                return false;
            }

            return true;
        }

        public async Task<bool> Deleteone(int id)
        {
            try
            {
                string url = $"/user/{id}";
                object request = await Connection("delete", url);

                if (request == null) return false;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hiba");
                return false;
            }
        }
    }
}
