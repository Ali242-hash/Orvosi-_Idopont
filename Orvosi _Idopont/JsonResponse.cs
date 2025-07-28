using Newtonsoft.Json;
using System;

namespace Orvosi__Idopont
{
    public class Userprofile
    {
        public int id { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public DateTime? létrehozásDátuma { get; set; }
    }

    public class Appointment
    {
        public int timeslotId { get; set; }
        public int? páciensId { get; set; }
        public string név { get; set; }
        public string megjegyzés { get; set; }
        public DateTime? létrehozásDátuma { get; set; }
    }

    public class LoginInfo
    {
      
        public string token { get; set; }
        public string Registerrole { get; set; }
        public string message { get; set; }
        public string loginUsername { get; set; }
        public string loginPassword { get; set; }
        public string RegisterUsername { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterEmail { get; set; }
    }

    public class JsonResponse
    {
        public int id { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }
        public string role { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public bool active { get; set; }

        [JsonProperty("létrehozásDátuma")]
        public DateTime? LétrehozásDátuma { get; set; }

        public string token { get; set; }
        public string message { get; set; }
    }

    public class Token
    {
        public static string token { get; set; }
    }
}
