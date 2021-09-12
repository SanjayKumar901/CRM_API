using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("UserMaster")]
    public class UserMaster
    {
        [Key]
        public int UserID { get; set; }
        public int? ClientID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string UserType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public int RoleId { get; set; }
        public long? AdhaarNumber { get; set; }
        public string PANNumber { get; set; }
        public string GSTNumber { get; set; }
        public int? KeyAccountManager { get; set; }
        public string BeneficiaryName { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSC_Code { get; set; }
        public string Qualification { get; set; }
        public int? TrainingPeriod { get; set; }
        public string PinCode { get; set; }
        public DateTime? DOB { get; set; }
        public bool? Married { get; set; }
        public int? Gender { get; set; }
        public string GST_CERTIFICATE { get; set; }
        public string referCode { get; set; }
        public string PoS_AssociateCode { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public long? ReferVal { get; set; }
        public string ReferPrifix { get; set; }
        public long? PosVal { get; set; }
        public string PosPrifix { get; set; }
        public string Alternatecode { get; set; }
        public string UserProfilePic { get; set; }
        public string RmCode { get; set; }
        public bool? IsDocRequred { get; set; }
        /*
         //public string CRM_MAKE_ID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Product { get; set; }
        //public string CarPayout { get; set; }
        public string TwoWheelerPayout { get; set; }
        public string HealthPayout { get; set; }
        public string TravelPayout { get; set; }
        //public string GST_Number { get; set; }
        public string LifePayout { get; set; }
        
        public string Insurer { get; set; }
        
        
        
        
        public string facebook { get; set; }
        public string GCV_PAYOUT { get; set; }
        public string TAXI_PAYOUT { get; set; }
        public string MARINE_PAYOUT { get; set; }
        
        public long? OTP { get; set; }
        public DateTime? MODPassDate { get; set; }
        public int? Productid { get; set; }
        public string Form_16_url { get; set; }
        public string Bank_Statement_Url { get; set; }
        public string InvestmentProof_url { get; set; }
        public string Others_url { get; set; }
        public char? IsAuthenticated { get; set; }
        public string CarPayoutOpt { get; set; }
        public string TwoPayoutOpt { get; set; }
        public string RHead { get; set; }
        public int? bhCityId { get; set; }
        //public string PhoneNo { get; set; }
//public string AlternateNo { get; set; }
        //public int? DesignationNO { get; set; }
        //public int? CompanyID { get; set; }
        //public int? DeptId { get; set; }
        //public string ProfileImage_URL { get; set; }
        //public string Adhaar_Front_URL { get; set; }
        //public string Adhaar_Back_URL { get; set; }
        //public string PAN_URL { get; set; }
        //public string QualificationCertificate_URL { get; set; }
        //public string CancelCheque_URL { get; set; }
        //public string C_PANNumber { get; set; }
        //public long? CD_Limit { get; set; }
        //public string BeneficiaryName { get; set; }
        */

    }
    public class UserAllocation
    {
        public string RHead { get; set; }
        public int? bhCityId { get; set; }
    }
    public class Myprofile
    {
        public UserMaster userMaster { get; set; }
        public RoleType roleType { get; set; }
        public UserDocument UserDocuments { get; set; }
        public PosExamStart posExamStart { get; set; }
        public IEnumerable<vw_userregiondata> UserRegions { get; set; }
        public int WrongLoginAttempt { get; set; }
        public int WhoView { get; set; }
    }
}
