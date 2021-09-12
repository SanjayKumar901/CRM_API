using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ConfigureSupport")]
    public class ConfigureSupport
    {
        [Key]
        public int ID { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public int ClientID { get; set; }
    }
}
