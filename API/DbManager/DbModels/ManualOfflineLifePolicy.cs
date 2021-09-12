using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ManualOfflineLifePolicy")]
    public class ManualOfflineLifePolicy
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public int CreatedID { get; set; }
        public string POSCode { get; set; }
        public string POSName { get; set; }
        public string POSSource { get; set; }
        public string ReportingManagerName { get; set; }
        public string RegionalManagerName { get; set; }
        public string CustName { get; set; }
        public string Address { get; set; }
        public int? CityID { get; set; }
        public string Pin { get; set; }
        public int? StateID { get; set; }
        public DateTime? Entrydate { get; set; }
        public DateTime? Modifydate { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Insurer { get; set; }
        public int? InsurerID { get; set; }
        public string ProductType { get; set; }
        public string Product { get; set; }
        public string ProductName { get; set; }
        public int? PolicyTerm { get; set; }
        public int? PremiumPayingTerm { get; set; }
        public string PremiumPayingFrequency { get; set; }
        public string BusinessType { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PolicyIssueDate { get; set; }
        public decimal? SumAssured { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? GST { get; set; }
        public decimal? TotalPremium { get; set; }
        public string Enquiryno { get; set; }
        public string ProductIssuanceType { get; set; }
        public string POSPProduct { get; set; }
        public bool? IsPayoutDone { get; set; }
        public DateTime? PayoutDate { get; set; }

    }
}
