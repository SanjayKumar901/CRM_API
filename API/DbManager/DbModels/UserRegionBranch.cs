using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("UserRegionBranch")]
    public class UserRegionBranch
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RegionID { get; set; }
        public int? BranchID { get; set; }
    }
}
