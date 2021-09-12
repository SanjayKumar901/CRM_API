using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Lifepolicy")]
    public class Lifepolicy
    {
        [Key]
        public int LifePolicyID { get; set; }
        public int UserID { get; set; }
        public int AddressID { get; set; }
        public bool Active { get; set; }
        public string ProposalNo { get; set; }
        public int CompanyID { get; set; }
        public int YourAge { get; set; }
        public string Gender { get; set; }
        public string SmokeStaus { get; set; }
        public string AnnualInCome { get; set; }
        public string PreferredCover { get; set; }
        public int CoverageAge { get; set; }
        public string PolicyType { get; set; }
        public int PaymentID { get; set; }
        public decimal SumAsured { get; set; }
        public decimal BasePremium { get; set; }
        public decimal GrossPremium { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int PolicyTerm { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyDocUrl { get; set; }
        public string PolicyStatus { get; set; }
        public string PolicyRemark { get; set; }
        public DateTime? Entrydate { get; set; }
    }
}
