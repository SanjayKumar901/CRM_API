using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Posexamsummary")]
    public class Posexamsummary
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string DocName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FileName { get; set; }
    }
    public class BindPosExam
    {
        public int DocID { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
    }
}
