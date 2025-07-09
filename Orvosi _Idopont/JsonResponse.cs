using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvosi__Idopont
{
    public class Userprofile
    {
        public int id { get; set; }
        public string Fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public DateTime? létrehozásDátuma { get; set; }
    }

    public class Appointment
    {
        public int id { get; set; }
        public int timeslotId { get; set; }
        public int? páciensId { get; set; }
        public string név { get; set; }
        public string megjegyzés { get; set; }
        public DateTime? létrehozásDátuma { get; set; }
    }

    public class LoginInfo
    {
        public string token { get; set; }
        public string role { get; set; }
        public string message { get; set; }

        public string loginUsername { get; set; }
        public string loginPassword { get; set; }
    }

    public class JsonResponse
    {
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public bool active { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }
}
