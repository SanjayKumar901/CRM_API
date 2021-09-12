using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Branchcity")]
    public class Branchcity
    {
        [Key]
        public int ID { get; set; }
        public string CityName { get; set; }
        public int RegionID { get; set; }
        public int? ClientID { get; set; }
    }
}
