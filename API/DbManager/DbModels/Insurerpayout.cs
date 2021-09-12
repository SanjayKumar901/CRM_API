using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Insurerpayout")]
    public class Insurerpayout
    {
        [Key]
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Product { get; set; }
        public string FromRange { get; set; }
        public string ToRange { get; set; }
        public decimal Payout { get; set; }
        public int ClientID { get; set; }
        public string TypeOfPayout { get; set; }
        public int UserID { get; set; }
        public string ProductOption { get; set; }
    }
}
