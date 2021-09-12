using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("LoginTimeHistory")]
    public class LoginTimeHistory
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime LoginTimeHis { get; set; }
        public int ClientID { get; set; }
        public string Action { get; set; }
        public DateTime? LogOutTimeHis { get; set; }
        public string LoginDevice { get; set; }
    }
}
