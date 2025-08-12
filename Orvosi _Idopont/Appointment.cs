using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvosi__Idopont
{
    public class Appointment
    {
        public int TimeslotId { get; set; }
        public int? PaciensId { get; set; }
        public string Név { get; set; }
        public DateTime? LétrehozásDátuma { get; set; }
    }
}
