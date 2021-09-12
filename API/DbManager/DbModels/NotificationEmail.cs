using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("NotificationEmail")]
    public class NotificationEmail
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string MailBody { get; set; }
        public string ForAction { get; set; }
        public string MailSubject { get; set; }
        public int? RoleID { get; set; }
    }

    [Table("NotificationSMS")]
    public class NotificationSMS
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string MsgBody { get; set; }
        public string ForAction { get; set; }
    }
}
