using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("vw_RegionZone")]
    public class vw_RegionZone
    {
        [Key]
        public int ID { get; set; }
        public string Region { get; set; }
    }
}
