using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ScriptPagePrivilege")]
    public class ScriptPagePrivilege
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string PageUrl { get; set; }
        public string Script { get; set; }
    }
}
