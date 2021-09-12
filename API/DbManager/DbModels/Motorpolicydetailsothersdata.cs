using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Motorpolicydetailsothersdata")]
    public class Motorpolicydetailsothersdata
    {
        [Key]
        public int ID { get; set; }
        public int MotorpolicyID { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public decimal? PayableOD { get; set; }
        public decimal? PayableTP { get; set; }
        public decimal? CPA { get; set; }
        public decimal? GrossDiscount { get; set; }
        public decimal? GrossAddOnPremium { get; set; }
        public decimal? NetAddOnPremium { get; set; }
        public string FinancierName { get; set; }
        public DateTime Create_Date { get; set; }
    }
}
