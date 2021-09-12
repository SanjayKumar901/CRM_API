using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class LicRedirectionModel
    {
        public string AgencyCode { get; set; }
        public string CheckSum { get; set; }
        public string ActionURL { get; set; }
        public string SpCode { get; set; }
    }
}
