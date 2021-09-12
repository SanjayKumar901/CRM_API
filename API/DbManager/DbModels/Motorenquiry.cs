using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("motorenquiry")]
    public class Motorenquiry
    {
        [Key]
        public int MotorID { get; set; }
        public string EnquiryNo { get; set; }
        public int? ClientID { get; set; }
        public int? ManufactureID { get; set; }
        public int? VehicleID { get; set; }
        public int? VariantID { get; set; }
        public int? FuelID { get; set; }
        public int? RegistartionYear { get; set; }
        public int? RTOID { get; set; }
        public string PolicyType { get; set; }
        public string MotorType { get; set; }
        public int? Status { get; set; }
        public string ResultID { get; set; }
    }
}
