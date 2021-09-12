using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("BranchForClient")]
    public class BranchForClient
    {
        [Key]
        public int ID { get; set; }
        public int BranchID { get; set; }
        public int RegionID { get; set; }
        public int ClientID { get; set; }
    }
}
