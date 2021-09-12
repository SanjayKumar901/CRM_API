using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("vw_userregiondata")]
    public class vw_userregiondata
    {
        [Key]
        public int ID { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
        public int Userid { get; set; }
        public int RegionClient { get; set; }
        public int BranchClient { get; set; }
        public int ClientID { get; set; }
    }
}
