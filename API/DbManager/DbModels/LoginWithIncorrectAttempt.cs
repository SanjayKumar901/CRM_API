using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("LoginWithIncorrectAttempt")]
    public class LoginWithIncorrectAttempt
    {
        [Key]
        public string EmailId { get; set; }
        public int Counter { get; set; }
        public int ClientID { get; set; }
    }
}
