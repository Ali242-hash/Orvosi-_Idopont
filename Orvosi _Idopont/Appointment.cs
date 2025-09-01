using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orvosi__Idopont
{
    public class Appointment
    {
        [JsonProperty("timeslotId")]
        public int TimeslotId { get; set; }

        [JsonProperty("páciensId")]
        public int? PaciensId { get; set; }

        [JsonProperty("név")]
        public string Név { get; set; }

        [JsonProperty("megjegyzés")]
        public string megjegyzés { get; set; }

        [JsonProperty("létrehozásDátuma")]
        public DateTime LétrehozásDátuma { get; set; }

        [JsonProperty("Status_Condition")]
        public string Status_Condition { get; set; }

        [JsonProperty("doctor")]
        public string doctor { get; set; }
    }
}
