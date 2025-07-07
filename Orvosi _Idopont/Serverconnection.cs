using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orvosi__Idopont
{
    public class Serverconnection
    {
        HttpClient client = new HttpClient();
        string baseUrl = "http://127.1.1.1:3000";

        public Serverconnection(string url)
        {
            baseUrl = url;
        }

        public async Task<List<Userprofile>> GetUserprofiles()
        {
            string url = baseUrl + "/users";
            List<Userprofile> all = new List<Userprofile>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string res = await response.Content.ReadAsStringAsync();
                all = JsonConvert.DeserializeObject<List<Userprofile>>(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return all;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            string url = baseUrl + "/appointments";
            List<Appointment> allap = new List<Appointment>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string res = await response.Content.ReadAsStringAsync();
                allap = JsonConvert.DeserializeObject<List<Appointment>>(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return allap;
        }

        public async Task<bool> Registration(string username, string password, string email)
        {
            string url = baseUrl + "/auth/register";

            try
            {
                var jsonData = new
                {
                    RegisterUsername = username,
                    RegisterPassword = password,
                    RegisterEmail = email,
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                MessageBox.Show(responseText);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public async Task<JsonResponse> Login(string username, string password)
        {
            string url = baseUrl + "/auth/login";  

            JsonResponse oneJsonResponse = new JsonResponse() { username = null };

            try
            {
                var jsonData = new
                {
                    loginUsername = username,
                    loginPassword = password,
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                MessageBox.Show(responseText);

                responseJson.username = username;
                responseJson.password = password;

                return responseJson;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return oneJsonResponse;
        }

        public async Task Save(JsonResponse data)  
        {
            string url = baseUrl + "/save";

            try
            {
                var jsonData = new
                {
                    data = data.token,
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.token);
                HttpResponseMessage response = await client.PutAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                MessageBox.Show(responseText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task<bool> PostAppointment(Appointment userappo)
        {
            string url = baseUrl + "/appointment";

            try
            {
                var jsonData = new
                {
                    timeslotId = userappo.timeslotId,
                    páciensId = userappo.páciensId,
                    név = userappo.név,
                    megjegyzés = userappo.megjegyzés,
                    létrehozásDátuma = userappo.létrehozásDátuma,
                };

                string StringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(StringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                string res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    var Notsucc = JsonConvert.DeserializeObject<Message>(res);
                    MessageBox.Show("you have already logedin ");
                    return false;
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    var Sucdelete = JsonConvert.DeserializeObject<Message>(res);
                    MessageBox.Show($"Sikerült törölni: {res}");
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }

            return false;
        }

        public async Task<bool> PostUserprofile(Userprofile userfile)
        {
            string url = baseUrl + "/user"; 

            try
            {
                var jsonData = new
                {
                    NewFullName = userfile.Fullname,
                    NewEmail = userfile.email,
                    NewUsername = userfile.username,
                    NewPassword = userfile.password,
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);

                string res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    var Notsucc = JsonConvert.DeserializeObject<Message>(res);
                    MessageBox.Show("This user already exist");
                    return false;
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    var yesSuc = JsonConvert.DeserializeObject<Message>(res);
                    MessageBox.Show($"Your login was successful: {res}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public async Task Deleteone(int id)
        {
            string url = baseUrl + $"/user/{id}";

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    MessageBox.Show("Sikerült törölni.");  
                }
                else
                {
                    string res = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Nem sikerült törölni: {res}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
