using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("LoginAttempt")]
    public class LoginAttempt
    {
        [Key]
        public int UserID { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
