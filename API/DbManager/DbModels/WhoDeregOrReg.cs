using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("WhoDeregOrReg")]
    public class WhoDeregOrReg
    {
        [Key]
        public int ID { get; set; }
        public int UserRegDereg { get; set; }
        public int WhoDid { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
    }
}
