using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Saveposquotesetup")]
    public class Saveposquotesetup
    {
        [Key]
        public int ID { get; set; }
        public string Product { get; set; }
        public int ClientID { get; set; }
        public int CompanyID { get; set; }
        public bool OnlyPOS { get; set; }
        public bool UserOnly { get; set; }
        public bool BothUser { get; set; }
        public decimal? Suminsured { get; set; }
    }
}
