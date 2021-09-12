using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("RegionZone")]
    public class RegionZone
    {
        [Key]
        public int ID { get; set; }
        public string Region { get; set; }
        public int? ClientID { get; set; }
        public bool? IsActive { get; set; }
    }
}
