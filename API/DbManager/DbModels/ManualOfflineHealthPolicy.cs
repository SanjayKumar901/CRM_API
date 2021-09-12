using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ManualOfflineHealthPolicy")]
    public class ManualOfflineHealthPolicy
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public int CreatedID { get; set; }
        public int InsurerID { get; set; }
        public string Policytype { get; set; }
        public string PlanName { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal ServiceTax { get; set; }
        public decimal CoverAmount { get; set; }
        public decimal BasePremium { get; set; }
        public string PolicyNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public DateTime ChecqueDate { get; set; }
        public string ChecqueNo { get; set; }
        public string ChecqueBank { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerAddress { get; set; }
        public string SelectBusinessType { get; set; }
        public int? SelectPolicyTerm { get; set; }
        public string InsuranceType { get; set; }
        public int? SelectState { get; set; }
        public int? SelectCity { get; set; }
        public string Pincode { get; set; }
        public DateTime? CustomerDOB { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PolicyIssueDate { get; set; }
        public bool? IsPospProduct { get; set; }

    }
}
