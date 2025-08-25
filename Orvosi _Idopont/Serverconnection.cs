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

        public async Task<object> Connection(string urlstring, string methodType, string jsonString = null)
        {
            authHead();
            string url = baseUrl + urlstring;
            string responseText = string.Empty;

            try
            {
                HttpResponseMessage response;

                if (methodType.ToLower() == "post")
                {
                    var content = new StringContent(jsonString ?? "", Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                }
                else if (methodType.ToLower() == "put")
                {
                    var content = new StringContent(jsonString ?? "", Encoding.UTF8, "application/json");
                    response = await client.PutAsync(url, content);
                }
                else if (methodType.ToLower() == "get")
                {
                    response = await client.GetAsync(url);
                }
                else if (methodType.ToLower() == "delete")
                {
                    response = await client.DeleteAsync(url);
                }
                else
                {
                    return null;
                }

                responseText = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return responseText;
            }
            catch (Exception)
            {
                string res = string.Empty;
                try
                {
                    res = JsonConvert.DeserializeObject<Message>(responseText).message;
                }
                catch
                {

                  //  MessageBox.Show($"Hiba a codot kharkose {res}");

                }

            }
            return null;
        }

        private void authHead()
        {


            if (client.DefaultRequestHeaders.Authorization == null && !string.IsNullOrEmpty(Token.token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.token);
            }
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var jsonData = new
                {
                    loginUsername = username,
                    loginPassword = password
                };

                string data = JsonConvert.SerializeObject(jsonData);
                object response = await Connection("/auth/login", "post", data);
                if (response == null) return false;

                LoginInfo reponseData = JsonConvert.DeserializeObject<LoginInfo>(response as string);
                Token.token = reponseData.token;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> Registration(string username, string password, string email, string role, string fullname)
        {
            try
            {
                var jsonData = new
                {
                    RegisterUsername = username,
                    RegisterPassword = password,
                    RegisterEmail = email,
                    fullname = fullname,
                    role = role,
                    active = true
                };

                string data = JsonConvert.SerializeObject(jsonData);
                object reponse = await Connection("/auth/register", "post", data);
                if (reponse == null) return false;
                RegistrationRequest reponseData = JsonConvert.DeserializeObject<RegistrationRequest>(reponse as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return false;
            }
            return true;
        }

        public async Task<List<Userprofile>> GetUserprofiles()
        {
            List<Userprofile> users = new List<Userprofile>();


            try
            {
                object repsone = await Connection("/users/all", "get");
                if (repsone == null) return users;

                users = JsonConvert.DeserializeObject<List<Userprofile>>(repsone as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            return users;
        }

        public async Task<List<Appointment>>GetAppointments()
        {
            List<Appointment> all = new List<Appointment>();
            try
            {
                object reponse = await Connection("/appointments/history", "get");
                if (reponse == null) return all;

                all = JsonConvert.DeserializeObject<List<Appointment>>(reponse as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return all;
        }

        public async Task<bool> loginAppointment(int timeslotId, string név)
        {
            try
            {
                var jsonData = new
                {
                    timeslotId = timeslotId,
                    név = név,
                };

                string data = JsonConvert.SerializeObject(jsonData);
                object reponse = await Connection("/appointments", "post", data);
                if (reponse == null) return false;
                Appointment reponseData = JsonConvert.DeserializeObject<Appointment>(reponse as string);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public async Task<bool> Deleteone(int id)
        {
            string url = $"/users/{id}";
      

            try
            {
                object reponse = await Connection(url, "delete");
                if (reponse == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
    }
}