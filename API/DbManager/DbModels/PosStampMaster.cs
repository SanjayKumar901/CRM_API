using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DbManager.DbModels
{
    [Table("PosStampMaster")]
    public class PosStampMaster
    {
        [Key]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string StampID { get; set; }
        public string POSCode { get; set; }
        public string POSName { get; set; }
        public DateTime? PosActiveDate { get; set; }
        public bool? SentToZoho { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? IIBDate { get; set; }
        public DateTime Entrydate { get; set; }
    }
}
