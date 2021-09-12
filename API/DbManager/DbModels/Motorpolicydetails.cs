using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("motorpolicydetails")]
    public class Motorpolicydetails
    {
        [Key]
        public int MotorPolicyID { get; set; }
        public int MotorID { get; set; }
        public int UserID { get; set; }
        public int? ClientID { get; set; }
        public long? AddressID { get; set; }
        public long? NomineeID { get; set; }
        public long? InsuredVehicleID { get; set; }
        public string MotorType { get; set; }
        public string PolicyType { get; set; }
        public int? Period { get; set; }
        public decimal? BasicOD { get; set; }
        public decimal? BasicTP { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? BasePremium { get; set; }
        public decimal? GrossPremium { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? ServiceTax { get; set; }
        public long? PaymentID { get; set; }
        public string PaymentStatus { get; set; }
        public string QuoteNo { get; set; }
        public string ProposalNo { get; set; }
        public int? CompanyID { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyDocUrl { get; set; }
        public string PolicyStatus { get; set; }
        public DateTime? Entrydate { get; set; }
        public string EngineNo { get; set; }
        public string ChesisNo { get; set; }
        public string PrevPolicyNO { get; set; }
        public string PreviousInsurer { get; set; }
        public string VehicleNo { get; set; }
        public decimal? IDV { get; set; }
    }
}
