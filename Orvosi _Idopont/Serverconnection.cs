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
            List<Appointment> all = new List<Appointment>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string res = await response.Content.ReadAsStringAsync();
                all = JsonConvert.DeserializeObject<List<Appointment>>(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return all;
        }

        public async Task<bool> Registration(string username, string password, string email, string role)
        {
            string url = baseUrl + "/auth/register";

            try
            {
                var jsonData = new
                {
                    RegisterUsername = username,
                    RegisterPassword = password,
                    RegisterEmail = email,
                    role = role 
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
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
            JsonResponse oneJsonResponse = new JsonResponse() { username=null};

            try
            {
                var jsonData = new
                {
                    loginUsername = username,
                    loginPassword = password
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
                JsonResponse responseJson = JsonConvert.DeserializeObject<JsonResponse>(responseText);
                MessageBox.Show(responseText);

                var allusers = await GetUserprofiles();
                var matchedusers = allusers.Find(use => use.username == username);
                if (matchedusers != null)
                {
                    matchedusers.id = matchedusers.id;
                    matchedusers.username = matchedusers.username;
                    matchedusers.password = matchedusers.password;
                    matchedusers.email = matchedusers.email;
                    matchedusers.role = matchedusers.role;
                    matchedusers.Fullname = matchedusers.Fullname;
                    matchedusers.létrehozásDátuma = matchedusers.létrehozásDátuma;
                }

                responseJson.token = responseJson.token;

                MessageBox.Show("Login was successful");

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
                    NewUsername = data.username,
                    NewPassword = data.password
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.token);
                HttpResponseMessage response = await client.PutAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseText = await response.Content.ReadAsStringAsync();
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
                    létrehozásDátuma = userappo.létrehozásDátuma
                };

                string StringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(StringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                string res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    MessageBox.Show("You have already booked this.");
                    return false;
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    MessageBox.Show($"Successfully booked: {res}");
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    NewPassword = userfile.password
                };

                string stringJson = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                string res = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    MessageBox.Show("This user already exists");
                    return false;
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    MessageBox.Show($"Registration successful: {res}");
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
                    MessageBox.Show("Successfully deleted.");
                }
                else
                {
                    string res = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to delete: {res}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
