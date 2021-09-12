using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("MailServerOption")]
    public class MailServerOption
    {
        [Key]
        public int ID { get; set; }
        public string MailServiceMaster { get; set; }
        public string OptionName { get; set; }
    }
}
