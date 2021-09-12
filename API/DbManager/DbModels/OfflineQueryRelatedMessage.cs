using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("OfflineQueryRelatedMessage")]
    public class OfflineQueryRelatedMessage
    {
        [Key]
        public int ID { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }
        public string Message { get; set; }
        public bool ReadOrNot { get; set; }
        public DateTime MessageDate { get; set; }
        public string Subject { get; set; }
    }
}
