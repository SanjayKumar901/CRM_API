using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Enquiry")]
    public class Enquiry
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public string MobileNo { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public string EnquiryType { get; set; }
        public bool Status { get; set; }
        public string LeadSource { get; set; }
        public int? UserID { get; set; }
        public int? ClientID { get; set; }
        public string MotorDetails { get; set; }
        public string macid { get; set; }
    }
}
