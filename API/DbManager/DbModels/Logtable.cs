using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Logtable")]
    public class Logtable
    {
        [Key]
        public int ID { get; set; }
        public int Who { get; set; }
        public int? ForUser { get; set; }
        public string Action { get; set; }
        public string Email { get; set; }
        public DateTime? Date { get; set; }
    }
}
