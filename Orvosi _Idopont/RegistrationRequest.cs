using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvosi__Idopont
{
    public class RegistrationRequest
    {
        public string RegisterUsername { get; set; }
        public string RegisterPassword { get; set; }
        public string RegisterEmail { get; set; }
        public string role { get; set; }
    }
}
