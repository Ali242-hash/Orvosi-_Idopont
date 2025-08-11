using System;

namespace Orvosi__Idopont
{
    public class Userprofile
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime? LétrehozásDátuma { get; set; }
    }

   public class Appointment
    {
        public int TimeslotId { get; set; }
        public int? PaciensId { get; set; }
        public string Név { get; set; }
        public DateTime? LétrehozásDátuma { get; set; }
    }
  
    

    public class LoginInfo
    {
       public string Token { get; set; }
       
       public string Message { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public string RegisterUsername { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterEmail { get; set; }
        public string role { get; set; }


        public string token {  get; set; }
    }

   /* public class JsonResponse
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime? LétrehozásDátuma { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
   */

    public class Token
    {
        public static string token { get; set; }
    }
}
