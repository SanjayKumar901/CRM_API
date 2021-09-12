using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class SecurityModel
    {
    }
    public class Token
    {
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public int RoleID { get; set; }
    }
}
