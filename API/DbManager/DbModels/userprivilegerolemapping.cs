using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("userprivilegerolemapping")]
    public class userprivilegerolemapping
    {
        [Key]
        public int ID { get; set; }
        public int PrivilegeID { get; set; }
        public int UserID { get; set; }
        public DateTime AssignDate { get; set; }
        public int? AssignBy { get; set; }
        public int? Addrecord { get; set; }
        public int? Editrecord { get; set; }
        public int? deleterecord { get; set; }
        public int? NavBarMasterMenuID { get; set; }
        public int? AsAdmin { get; set; }
        public int? DownloadData { get; set; }
    }
}
