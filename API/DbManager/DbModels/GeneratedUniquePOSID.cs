using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("GeneratedUniquePOSID")]
    public class GeneratedUniquePOSID
    {
        [Key]
        public int ID { get; set; }
        public int CLientid { get; set; }
        public string UniqueID { get; set; }
        public string UniqueEmail { get; set; }
        public string LinkType { get; set; }
    }
}
