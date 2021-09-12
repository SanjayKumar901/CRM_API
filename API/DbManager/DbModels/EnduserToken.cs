using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("EnduserToken")]
    public class EnduserToken
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; }
        public int UserID { get; set; }
        public string OTP { get; set; }
    }
}
