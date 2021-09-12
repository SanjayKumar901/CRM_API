using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Healthenquiry")]
    public class Healthenquiry
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public int? ClientID { get; set; }
        public int? UserID { get; set; }
        public int? PinCode { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Status { get; set; }
        public string ResultId { get; set; }
        public string PolicyType { get; set; }
        public int? Gender { get; set; }
        public int? UserAge { get; set; }
        public int? SpouseAge { get; set; }
        public int? FatherAge { get; set; }
        public int? MotherAge { get; set; }
        public int? SonAge1 { get; set; }
        public int? SonAge2 { get; set; }
        public int? SonAge3 { get; set; }
        public int? SonAge4 { get; set; }
        public int? DoughterAge1 { get; set; }
        public int? DoughterAge2 { get; set; }
        public int? DoughterAge3 { get; set; }
        public int? DoughterAge4 { get; set; }
    }
}
