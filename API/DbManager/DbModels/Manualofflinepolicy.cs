using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Manualofflinepolicy")]
    public class Manualofflinepolicy
    {
        [Key]
        public int? ID { get; set; }
        public int? UserID { get; set; }
        public int? ClientID { get; set; }
        public int? CreatedID { get; set; }
        public string MotorType { get; set; }
        public string PolicyType { get; set; }
        public decimal? BasicOD { get; set; }
        public decimal? BasicTP { get; set; }
        public decimal? GrossPremium { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? ServiceTax { get; set; }
        public string PolicyNo { get; set; }
        public DateTime? Entrydate { get; set; }
        public string EngineNo { get; set; }
        public string ChesisNo { get; set; }
        public string VehicleNo { get; set; }
        public decimal? IDV { get; set; }
        public string FilePath { get; set; }
        public int? Insurer { get; set; }
        public int? Make { get; set; }
        public int? Fuel { get; set; }
        public int? Variant { get; set; }
        public int? ManufacturingMonth { get; set; }
        public int? ManufacturingYear { get; set; }
        public string CustomerName { get; set; }
        public DateTime? PolicyIssuedate { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public string BusinessType { get; set; }
        public int? NCB { get; set; }
        public string ChecqueNo { get; set; }
        public DateTime? ChecqueDate { get; set; }
        public string ChecqueBank { get; set; }
        public int? Vehicle { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public int? PreviousNCB { get; set; }
        public string CubicCapicity { get; set; }
        public int? RTOID { get; set; }
        public string PrevPolicyNO { get; set; }
        public decimal? CPA { get; set; }
        public bool? NillDep { get; set; }
        public string CustomerAddress { get; set; }
        public int? CustomerCityID { get; set; }
        public string CustomerPinCode { get; set; }
        public DateTime? CustomerDOB { get; set; }
        public string CustomerFax { get; set; }
        public string CustomerPANNo { get; set; }
        public decimal? GrossDiscount { get; set; }
        public int? Period { get; set; }
        public string InsuranceType { get; set; }
        public bool? IsPospProduct { get; set; }
        public decimal? AddOnPremium { get; set; }
        public string GVW { get; set; }
        public int? SeatingCapacity { get; set; }
        /*
         * Comment By Ziaur(03/Aug/2021)
        [NotMapped]
        public int SeatingCapacity
        {
            get
            {
                return _SeatCaps != null ? _SeatCaps.Value : 0;
            }
        }
        private int? _SeatCaps = null;
        public int? sea_cap { get { return _SeatCaps; } set { _SeatCaps = value; } }
        */
    }
}
