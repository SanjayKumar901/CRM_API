using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("MailServer")]
    public class MailServer: BaseMail
    {
        
    }
    public class BaseMail
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int MailserveroptionID { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public bool UseDefaultCredential { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public int? RoleID { get; set; }
    }
}
