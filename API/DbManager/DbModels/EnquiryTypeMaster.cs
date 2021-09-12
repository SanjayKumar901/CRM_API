using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("EnquiryTypeMaster")]
    public class EnquiryTypeMaster
    {
        [Key]
        public int ID { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }
}
