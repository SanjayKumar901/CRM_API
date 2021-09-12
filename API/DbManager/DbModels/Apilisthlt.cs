using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Apilisthlt")]
    public class Apilisthlt
    {
        [Key]
        public int ID { get; set; }
        public string BrokerName { get; set; }
        public int ClientID { get; set; }
        public bool IsActive { get; set; }
    }
}
