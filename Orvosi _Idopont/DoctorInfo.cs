using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvosi__Idopont
{
    public class DoctorInfo
    {
        public int id {  get; set; }
        public string Docname { get; set; }
        public string Description { get; set; }

        public string profilKépUrl {  get; set; }

        public string specialty {  get; set; }

        public string treatments { get; set; }
    }
}
