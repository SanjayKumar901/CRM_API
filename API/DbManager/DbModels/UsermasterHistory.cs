using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("usermasterHistory")]
    public class UsermasterHistory
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string MobileNo { get; set; }
        public int RoleID { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string RmCode { get; set; }
    }
}
