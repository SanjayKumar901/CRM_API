using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("IRDAHiddenProcess")]
    public class IRDAHiddenProcess
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public bool IsHidden { get; set; }
        public DateTime CreateDate { get; set; }
        public int ClientID { get; set; }
    }
}
