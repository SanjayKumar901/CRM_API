using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    public class Procedres
    {
    }

    public class sp_Rolelist
    {
        [Key]
        public int roleid { get; set; }
        public string rolename { get; set; }
    }
    public class sp_RoleMastWithClientID
    {
        [Key]
        public int roleid { get; set; }
        public string rolename { get; set; }
    }
    public class sp_TeamUserList
    {
        [Key]
        public int userid { get; set; }
        public string UserName { get; set; }
        public string Emailaddress { get; set; }
    }
    public class sp_CreateByList:baseUserList
    {
        public int? BranchID { get; set; }
        public string PosTraining { get; set; }
        public string CREATEDDATE { get; set; }
        public string ReferCode { get; set; }
        public string PosCode { get; set; }
    }
    public class baseUserList
    {
        [Key]
        public int USERID { get; set; }
        public string USERNAME { get; set; }
        public string ROLENAME { get; set; }
        public string USERTYPE { get; set; }
        public bool? Active { get; set; }
        public int? Regionid { get; set; }
        public string EmailAddress { get; set; }
    }
    public class sp_CreateByListOfflineBusines:baseUserList
    {
        public DateTime? CREATEDDATE { get; set; }
    }
    public class sp_ManualOfflineBusiness: ManageOnlineOfflineMotor
    {
        
    }
    public class sp_UserInfo
    {
        [Key]
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PanNumber { get; set; }
        public string AdhaarNumber { get; set; }
        public string ReportingManager { get; set; }
        public string CreatedBy { get; set; }
        public string Refercode { get; set; }
        public string Password { get; set; }
        public string LinkUrl { get; set; }
        public int RoleID { get; set; }
    }
    public class sp_PayoutData
    {
        [Key]
        public string Success { get; set; }
    }
    public class SP_BusinessReportViewDetails : ManageOnlineOfflineMotor
    {
        
    }
    public class ManageOnlineOfflineMotor
    {
        [Key]
        public int VariantID { get; set; }
        public int? Companyid { get; set; }
        public string EnquiryNo { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string NAME { get; set; }
        public string MobileNo { get; set; }
        public string VehicleName { get; set; }
        public string VariantName { get; set; }
        public string FuelName { get; set; }
        public string RTOName { get; set; }
        public int? RegistartionYear { get; set; }
        public string DealOwner { get; set; }
        public string CloseDate { get; set; }
        public string LeadSource { get; set; }
        public int? MotorPolicyID { get; set; }
        public decimal? PaidAmount { get; set; }
        public int? ClientID { get; set; }
        public string CompanyName { get; set; }
        public string PolicyNo { get; set; }
        public string referCode { get; set; }
        public string VehicleNo { get; set; }
        public int? PolicyFileID { get; set; }
        public string macid { get; set; }
        public string MotorType { get; set; }
        public decimal? PayoutCalC { get; set; }
    }
    public class sp_HealthBusinessReport: HealthBusiness
    {
    }
    public class HealthBusiness
    {
        [Key]
        public int Userid { get; set; }
        public string EnquiryNo { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string rolename { get; set; }
        public string PolicyType { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string PlanName { get; set; }
        public string ProposalNo { get; set; }
        public decimal TotalPremium { get; set; }
        public string CreatedDate { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyName { get; set; }
        public string LeadSource { get; set; }
        public string referCode { get; set; }
        public string Path { get; set; }
    }
    public class SP_PRIVILAGES
    {
        [Key]
        public string PrivilegeName { get; set; }
        public string PrivilegeGroupName { get; set; }
        public string Active { get; set; }
        public string url { get; set; }
        public string Separation { get; set; }
        public int? Addrecord { get; set; }
        public int? Editrecord { get; set; }
        public int? DownloadData { get; set; }
    }
    public class sp_DashboardHeader
    {
        [Key]
        public decimal TotalCollection { get; set; }
        public decimal TodayCollection { get; set; }
        public int TodayNoPS { get; set; }
        public int YDANoPS { get; set; }
        public decimal PosCollection { get; set; }
        public decimal PoSThisMonth { get; set; }
        public decimal PoSPrevMonth { get; set; }
        public decimal TodayCollectionThisMonth { get; set; }
        public decimal TodayCollectionPrevMonth { get; set; }
        public int TotalNOP { get; set; }
        public int CurrentMonthNops { get; set; }
        public int PrevMonthNops { get; set; }
        public int Todaylead { get; set; }
        public int Totaluser { get; set; }
        public int PrevTotaluser { get; set; }
        public int CurrentTotaluser { get; set; }
        public int CountPos { get; set; }
        public int PrevPOsTotaluser { get; set; }
        public int CurrentPOsTotaluser { get; set; }
        public int? TodayQuote { get; set; }
        public int? TodayProposal { get; set; }
        #region Pending Property
        //public int todayProposal { get; set; } 
        //public int todayQuote { get; set; }
        #endregion
    }
    public class sp_LeadDetailGrid
    {
        [Key]
        public int ID { get; set; }
        public string EnqNo { get; set; }
        public string EnquiryNo { get; set; }
        public string ENQUIRYTYPE { get; set; }
        public string MOBILENO { get; set; }
        public string LEADSOURCE { get; set; }
        public string ENQUIRYDATE { get; set; }
        public string PolicyStatus { get; set; }
        //public string Region { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string MotorDetails { get; set; }
        public string PolicyType { get; set; }
        public string Macid { get; set; }
    }
    public class sp_MobileLoginData
    {
        [Key]
        public int UserID { get; set; }
        public int? ClientID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }
        public int? RoleId { get; set; }
        public string MobileNo { get; set; }
    }
    public class sp_privilegelist
    {
        [Key]
        public int PrivilegeID { get; set; }
        public string PrivilegeName { get; set; }
        public int NavBarMasterMenuID { get; set; }
        public string GroupName { get; set; }
    }

    public class sp_ReferAndPosSeries
    {
        [Key]
        public long? ReferVal { get; set; }
        public string ReferPrifix { get; set; }
        public long? PosVal { get; set; }
        public string PosPrifix { get; set; }
    }
    public class SP_BUSINESSBYCITY
    {
        [Key]
        public string POLICYSTATUS { get; set; }
        public int CITYID { get; set; }
        public decimal TotalPremium { get; set; }
        public string CITYNAME { get; set; }
    }
    public class sp_ReqAndResponse : ReqAndResponseBase
    {
    }
    public class sp_QuoteReqAndResponse : ReqAndResponseBase
    {
    }

    public class ReqAndResponseBase
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public string REQ { get; set; }
        public string RES { get; set; }
        public string Companyname { get; set; }
    }

    public class sp_QuoteInfo
    {
        [Key]
        public string EnquiryNo { get; set; }
        public string Variantname { get; set; }
        public string Motortype { get; set; }
        public string Rtoname { get; set; }
        public string Policytype { get; set; }
        public string EnquiryDate { get; set; }
        public string Manufacturername { get; set; }
        public string Mobileno { get; set; }
        public string Fuelname { get; set; }
        public string Username { get; set; }
        public string ReportingManager { get; set; }
        public string Rolename { get; set; }
        public string PolicyUrl { get; set; }
        public string RcUrl { get; set; }
    }

    public class sp_hltQouteInfo
    {
        [Key]
        public string EnquiryNo { get; set; }
        public string Gender { get; set; }
        public string EnquiryDate { get; set; }
        public int? AdultCount { get; set; }
        public string PolicyType { get; set; }
        public int? Childcount { get; set; }
        public string Username { get; set; }
        public string Rolename { get; set; }
        public string MobileNo { get; set; }
    }
    public class sp_HltAfterQuoteInfo
    {
        [Key]
        public string Enquiryno { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Nomineename { get; set; }
        public string DOB { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Addressline3 { get; set; }
        public int? Pincode { get; set; }
        public string PolicyType { get; set; }
        public string ProposalNo { get; set; }
        public string QuoteNo { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? Service_Tax { get; set; }
        public string PlanName { get; set; }
        public string UserName { get; set; }
        public string Rolename { get; set; }
        public string CompanyName { get; set; }
        public string EnquiryDate { get; set; }
    }
    public class sp_AfterQuoteInfo
    {
        [Key]
        public string Enquiryno { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Nomineename { get; set; }
        public string DOB { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Addressline3 { get; set; }
        public int Pincode { get; set; }
        public string Motortype { get; set; }
        public string ProposalNo { get; set; }
        public string QuoteNo { get; set; }
        public string EngineNo { get; set; }
        public string Chesisno { get; set; }
        public string Variantname { get; set; }
        public string Rtoname { get; set; }
        public string Fuelname { get; set; }
        public string Manufacturername { get; set; }
        public string Policytype { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? NetPremium { get; set; }
        public decimal? ServiceTax { get; set; }
        public decimal? IDV { get; set; }
        public int RegistartionYear { get; set; }
        public string VehicleNo { get; set; }
        public string UserName { get; set; }
        public string Rolename { get; set; }
        public string CompanyName { get; set; }
    }
    public class sp_PriviligeList
    {
        [Key]
        public int PrivilegeID { get; set; }
        public string PrivilegeName { get; set; }
        public int NavBarMasterMenuID { get; set; }
    }
    public class sp_PaymentFail
    {
        [Key]
        public int ID { get; set; }
        public int Userid { get; set; }
        public string RoleName { get; set; }
        public decimal? TotalPremium { get; set; }
        public string VehicleNo { get; set; }
        public string ResponsePayment { get; set; }
    }
    public class UserDetailBase
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? entryDate { get; set; }
        public int userid { get; set; }
        public string EnquiryNo { get; set; }
    }
    public class sp_UserDetailViaMPD : UserDetailBase
    {

    }
    public class sp_UserDetailViaHPD : UserDetailBase
    {

    }
    public class sp_HealthbusinessReportHeader: HealthHeaderReport
    {
    }
    public class sp_HealthManualbusinessReportHeader : HealthHeaderReport
    {
    }
    public class HealthHeaderReport
    {
        [Key]
        public decimal TotalCollection { get; set; }
        public decimal TodayCollection { get; set; }
        public int TotalNoPs { get; set; }
        public int TodayNoPs { get; set; }
    }
    public class sp_MapOrUnmapList
    {
        [Key]
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public string CubicCapacity { get; set; }
        public string FuelType { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
    }
    public class sp_AcitveUserList
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int? RoleID { get; set; }
    }
    public class sp_RenewData
    {
        [Key]
        public string EnquiryNo { get; set; }
        public string EmpUserName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public int Userid { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNo { get; set; }
        public string CompanyName { get; set; }
        public decimal? TotalPremium { get; set; }
        public string PolicyNo { get; set; }
    }
    public class sp_RegionZoneWithClientid
    {
        public int ID { get; set; }
        public string Region { get; set; }
    }
    public class sp_RenewHealthData
    {
        [Key]
        public string EnquiryNo { get; set; }
        public string EmpUserName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public int Userid { get; set; }
        public string CompanyName { get; set; }
        public decimal? TotalPremium { get; set; }
        public int? UserAge { get; set; }
        public int? SpouseAge { get; set; }
        public string PlanName { get; set; }
        public string PolicyNo { get; set; }
    }
    public class PrivilegeMasterData
    {
        public List<sp_privilegelist> Privilegelist { get; set; }
        public List<userprivilegerolemapping> UserprivilegerolemappingS { get; set; }
    }
    public class PrivilegeMaster : sp_privilegelist
    {
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool AsAdmin { get; set; }
        public bool DownloadData { get; set; }
    }
    public class sp_Logintimehistory
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string LoginTimeHis { get; set; }
        public string LogOutTimeHis { get; set; }
        public string LoginDevice { get; set; }
    }
    public class sp_MapRoleList
    {
        [Key]
        public int Roleid { get; set; }
        public string Rolename { get; set; }
        public int? Clientid { get; set; }
        public bool? Isactive { get; set; }
    }
    public class sp_Regionbranchwithclient
    {
        [Key]
        public int? CityID { get; set; }
        public int? RegionID { get; set; }
        public int? ClientID { get; set; }
        public int? BranchID { get; set; }
        public string CityName { get; set; }
        public string Region { get; set; }
    }
    public class sp_GetCitiesWithRegionid
    {
        [Key]
        public int ID { get; set; }
        public string CityName { get; set; }
    }
    public class sp_MotorBusinessHeader: OnlineOfflineBusinessHeaderBase
    {
        
    }
    public class sp_MotorManualBusinessHeader: OnlineOfflineBusinessHeaderBase
    {

    }
    public class OnlineOfflineBusinessHeaderBase
    {
        [Key]
        public decimal? TotalCollection { get; set; }
        public decimal? TodayCollection { get; set; }
        public decimal? TodayNoPS { get; set; }
        public decimal? TotalNOP { get; set; }
    }
    public class sp_EndUserData
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string MotorType { get; set; }
        public string PolicyType { get; set; }
        public string POlicyno { get; set; }
        public string VehicleNo { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Vehicle { get; set; }
        public string Companyname { get; set; }
        public decimal? TotalPremium { get; set; }
        public string Mobile { get; set; }
    }
    public class sp_EndUserhealthData
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PolicyType { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? TotalPremium { get; set; }
        public string Companyname { get; set; }
        public string Mobile { get; set; }
    }
    public class BasePaymentProcess
    {
        [Key]
        public int ID { get; set; }
        public int Userid { get; set; }
        public int CustUserID { get; set; }
        public string EnquiryNo { get; set; }
        public string InsurerFiles { get; set; }
        public string username { get; set; }
        public string EmailAddress { get; set; }
        public string Policytype { get; set; }
        public int SelectedIsurer { get; set; }
        public string CompanyName { get; set; }
    }
    public class sp_GetProcessPaymentGetway : BasePaymentProcess
    {
        public int MotorID { get; set; }
        public string MotorType { get; set; }
    }
    public class sp_GetProcessHLTPaymentGetway : BasePaymentProcess
    {

    }
    public class sp_Consolidate
    {
        [Key]
        public int ID { get; set; }
        public string Region { get; set; }
        public string Enquirydate { get; set; }
        public int EnquiryCount { get; set; }
        public decimal? TotalPremium { get; set; }
    }
    public class sp_BranchListByRegion
    {
        [Key]
        public int BranchID { get; set; }
        public int RegionID { get; set; }
        public int ClientID { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
    }
    public class sp_InactiveUserList
    {
        [Key]
        public int userid { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PosCode { get; set; }
        public string PassOrFail { get; set; }
        public bool Docverified { get; set; }
    }
    public class sp_GetEnquiryNo
    {
        [Key]
        public string Enquiryno { get; set; }
    }
    public class sp_SharedDetails: ShareBase
    {
        public string CreatedDate { get; set; }
    }
    public class sp_ClaimDetails : ClaimBase
    {
        public string CreatedDate { get; set; }
    }
    public class sp_CheckDashBoardPrivilege
    {
        [Key]
        public bool IsTrue { get; set; }
    }
    public class sp_RenewUserData
    {
        [Key]
        public int ID { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public DateTime? ManageDate { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? ClientID { get; set; }
    }
    public class sp_YearConsolidate
    {
        [Key]
        public int RN { get; set; }
        public string Region { get; set; }
        public string Enquirydate { get; set; }
        public int EnquiryCount { get; set; }
    }
    public class sp_ShowAboutConsolidate
    {
        [Key]
        public int RN { get; set; }
        public string EnQuiryNo { get; set; }
        public string Motortype { get; set; }
        public string policyno { get; set; }
    }
    public class sp_UserProgresive
    {
        [Key]
        public int UseriD { get; set; }
        public decimal? TotalPremium { get; set; }
        public string PolicyStatus { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string RoleName { get; set; }
    }
    public class sp_MailserverData: BaseMail
    {

    }
    public class sp_MailServerActionTime : BaseMail
    {

    }
    public class sp_UserCreationMailConfig
    {
        [Key]
        public int ID { get; set; }
        public string Message { get; set; }
    }
    public class sp_ManualOfflineHLTBusiness: HealthBusiness
    {
    }
    public class sp_FeeddbackData
    {
        [Key]
        public int ID { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public int FeedbackOptionID { get; set; }
        public string FeedbackText { get; set; }
        public DateTime? FeedbackDate { get; set; }
    }
    public class sp_EndUserLogin
    {
        [Key]
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
    }
    public class sp_SavePosMotorQuoteSetup
    {
        [Key]
        public string Response { get; set; }
    }
    public class sp_SavePosHLTQuoteSetup
    {
        [Key]
        public string Response { get; set; }
    }
    public class sp_GetUserBirthdayList
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string RoleName { get; set; }
    }
    public class sp_ReqResponse
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string PolicyType { get; set; }
        public string MotorType { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? ResponseTime { get; set; }
    }
    public class sp_GetPayoutData
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string Product { get; set; }
        public string FromRange { get; set; }
        public string ToRange { get; set; }
        public decimal? Payout { get; set; }
        public string TypeOfPayout { get; set; }
    }
    public class sp_GetSalesPersonRecord
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string SalesPersonCode { get; set; }
    }

    #region Offline Life Policy
    public class sp_ManualOfflieTermLifeReport
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
        public string Statename { get; set; }
        public string CityName { get; set; }
    }
    public class sp_LifeManualbusinessReportHeader
    {
        [Key]
        public decimal TotalCollection { get; set; }
        public decimal TodayCollection { get; set; }
        public int TotalNOP { get; set; }
        public int TodayNoPS { get; set; }
    }
    #endregion

    #region Zoho Signup

    public class sp_POSUserList
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PosCode { get; set; }
        public string PassOrFail { get; set; }
        public DateTime? Timing { get; set; }
    }

    public class sp_POSUserListForZoho
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PosCode { get; set; }
        public DateTime? PosActiveDate { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
        public string AadharNo { get; set; }
        public string StampID { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? IIBDate { get; set; }
    }
    #endregion
}
