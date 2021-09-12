using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Sharedatadetails")]
    public class Sharedatadetails: ShareBase
    {
        public DateTime CreatedDate { get; set; }
    }
    public class ShareBase
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public string ShareType { get; set; }
        public string ProductType { get; set; }
        public string SharedVia { get; set; }
        public string ToMail { get; set; }
        public int? UserID { get; set; }
        public int ClientID { get; set; }
    }
}
