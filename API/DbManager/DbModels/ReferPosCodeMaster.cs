using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ReferPosCodeMaster")]
    public class ReferPosCodeMaster:BaseRefer
    {
           
    }
    public class sp_GetReferPosCode: BaseRefer
    {

    }
    public class BaseRefer
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string ReferPrifix { get; set; }
        public int CodeVal { get; set; }
        public int RoleID { get; set; }
        public string Options { get; set; }
        public DateTime Create_Date { get; set; }
        public bool? IsParent { get; set; }
    }
}
