using API.CommonMethods;
using API.DbManager.DbModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class CommonParam
    {
        public string Token { get; set; }
    }
    public class UrlBase
    {
        public string URL { get; set; }
    }
    public class Privilege:User
    {
        public int Privilegeid { get; set; }

    }
    public class User:CommonParam
    {
        public int Userid { get; set; }
        public string RegisterOrDeregister { get; set; }
        public string Reason { get; set; }
    }
    public class BusinessbyCityParam:User
    {
        public int ClientID { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime todate { get; set; }
    }
    public class PrivilegeMap : CommonParam
    {
        public string Option { get; set; }
        public List<PrvRow> Rows { get; set; }
    }
    public class GenerateLinkParam : PrivilegeMap
    {
        public string LinkType { get; set; }
    }
    public class PrvRow
    {
        public int PrivilegeID { get; set; }
        public int NavBarMasterMenuID { get; set; }
        public int UserID { get; set; }
        public int Addrecord { get; set; }
        public int Editrecord { get; set; }
        public int deleterecord { get; set; }
        public string EuserID { get; set; }
        public int AsAdmin { get; set; }
        public int DownloadData { get; set; }

    }
    public class ClientUser : User
    {
        public int ClientID { get; set; }
    }
    public class FromDateToDate:ClientUser
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ProductType { get; set; }
    }
    public class BusinessReportReq:CommonParam
    {
        public string Product { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class PayyoutParam : User
    {
        public string BeneficiaryName { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSC_Code { get; set; }
        public string PANNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
    }
    public class GSTCertificateParam:User
    {
        public string GST_CERTIFICATE { get; set; }
        public string GST_CERTIFICATE_URL { get; set; }
    }
    public class DocumentsParam : User
    {
        public string Adhaar_Front_URL { get; set; }
        public string Adhaar_Back_URL { get; set; }
        public string PAN_URL { get; set; }
        public string QualificationCertificate_URL { get; set; }
        public string CancelCheque_URL { get; set; }
        public string TearnAndCondition { get; set; }
        public string UserPic { get; set; }
        public string GSTFile { get; set; }
    }
    public class PersonalDetails : User
    {
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
    }
    public class GSTINParam : User
    {
        public string GSTIN { get; set; }
    }
    public class BaseParam:CommonParam
    {
        public string UserID { get; set; }
        public int ClientID { get; set; }
    }
    public class FilterUserByDate : BaseParam
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class RoleTypeParam:CommonParam
    {
        public int ID { get; set; }
    }
    public class TeamUserListParam: BaseParam
    {
        public int RoleID { get; set; }
    }
    public class Login
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string ClientURL { get; set; }
        public string OTP { get; set; }
        public string DeviceName { get; set; }
    }
    public class UserCreationParam :CommonParam
    {
        public string UserName { get; set; }
        public bool Active { get; set; }
        public int? CreateBy { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public int RoleId { get; set; }
        public int KeyAccountManager { get; set; }
        public long? ReferVal { get; set; }
        public string ReferPrifix { get; set; }
        public long? PosVal { get; set; }
        public string PosPrifix { get; set; }
        public string Alternatecode { get; set; }
        public RegionsBranch[] Regions { get; set; }
        public bool? IsDocRequred { get; set; }
        public string PanNum { get; set; }
        public long? AdharNum { get; set; }
        public DateTime? DOB { get; set; }
    }
    public class RegionsBranch
    {
        public int? UserID { get; set; }
        public int RegionID { get; set; }
        public int? BranchID { get; set; }
    }
    public class ReqResParam : CommonParam
    {
        public string FilterText { get; set; }
        public string FilterOption { get; set; }
        public string ProductType { get; set; }
    }
    public class ResetPass : CommonParam
    {
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string ConfirmPass { get; set; }
    }
    public class EnquiryInfo : CommonParam
    {
        public string Action { get; set; }
        public string EnquiryNo { get; set; }
        public string Product { get; set; }
    }
    public class UserCreationList:CommonParam
    {
        public List<UserCreationItem> UserCreations { get; set; }
    }
    public class UserCreationItem
    {
        public string UserName { get; set; }
        public string Active { get; set; }
        public string Address { get; set; }
        public string PANNumber { get; set; }
        public string IFSC_Code { get; set; }
        public string PinCode { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string BankAccountNo { get; set; }
        public string AdhaarNumber { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string RoleId { get; set; }
        public string KeyAccountManager { get; set; }
        public string ReferVal { get; set; }
        public string ReferPrifix { get; set; }
        public string PosVal { get; set; }
        public string PosPrifix { get; set; }
        public string RegionId { get; set; }
        public string BranchID { get; set; }
    }
    public class EnduserMapping:CommonParam
    {
        public int MappUserid { get; set; }
        public string FilterEnquiry { get; set; }
        public string Product { get; set; }
    }
    public class MergeUserPrivilege : CommonParam
    {
        public string SeprateUsers { get; set; }
        public string SepratePriveles { get; set; }
        public int RoleID { get; set; }
    }
    public class ManageUserMapp : CommonParam
    {
        public string SeprateUsers { get; set; }
        public int ReportingManagerID { get; set; }
        public int RegionID { get; set; }
        public int BranchID { get; set; }
    }
    public class MotorParam : CommonParam
    {
        public string Type { get; set; }
        public int ID { get; set; }
    }
    public class MappParam : CommonParam
    {
        public int CompanyID { get; set; }
        public int Variantid { get; set; }
        public string Variantname { get; set; }
        public string Modelname { get; set; }
        public string Makename { get; set; }
    }
    public class MappVariant : CommonParam
    {
        public int CompanyID { get; set; }
        public int Variantid { get; set; }
        public int CompVrID { get; set; }
    }
    public class RenewDataList : CommonParam
    {
        public string UserList { get; set; }
        public string Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class CodePrifixParam : CommonParam
    {
        public string Option { get; set; }
        public string Prifix { get; set; }
        public int CodeVal { get; set; }
        public int RoleID { get; set; }
        public bool IsParent { get; set; }
    }
    public class RegisterPosParam : CommonParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DetailAction { get; set; }
    }
    public class MotorPolicyPDF : CommonParam
    {
        public string PolicyNo { get; set; }
        public string DownloadAction { get; set; }
        public string Product { get; set; }
        public string clienturl { get; set; }
    }
    public class PosDuration:CommonParam
    {
        public int Hours { get; set; }
    }
    public class ExamProcessParam : CommonParam
    {
        public bool Start { get; set; }
    }
    public class CompleteTraining : CommonParam
    {
        public bool IsTrainingComplete { get; set; }
    }
    public class CompleteExam : CommonParam
    {
        public bool IsExamComplete { get; set; }
        public ExamResultCheck[] FinalResult { get; set; }
    }
    public class ExamResultCheck
    {
        public int ID { get; set; }
        public string Answer { get; set; }
    }
    public class MissingPolicyParam:CommonParam
    {
        [Required(ErrorMessage = "Client ID is required.")]
        public string EnquiryStatus { get; set; }
        [Required(ErrorMessage = "Policy No is required.")]
        public string PolicyNo { get; set; }
        [Required(ErrorMessage = "Insurer is required.")]
        public int? Insurer { get; set; }
        [Required(ErrorMessage = "Vehicle No is required.")]
        public string VehicleNo { get; set; }
        [Required(ErrorMessage = "Chasis No is required.")]
        public string ChasisNo { get; set; }
    }
    public class MapRole : CommonParam
    {
        public IList<RoleList> Roles { get; set; }
    }
    public class RoleList
    {
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
    }
    public class IRDAparam : CommonParam
    {
        public bool IsApply { get; set; }
    }
    public class FindEnquiryParam: UrlBase
    {
        public string PolicyNo { get; set; }
    }
    public class VehicleParam :CommonParam
    {
        public string VehicleProperty { get; set; }
    }
    public class AddManufacturerParam : CommonParam
    {
        [Required(ErrorMessage = "ManufacturerName is required.")]
        public string ManufacturerName { get; set; }
        public bool IsCar { get; set; }
        public bool IsBike { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddVehicleParam : CommonParam
    {
        [Required(ErrorMessage = "ManufacturerName is required.")]
        public string VehicleName { get; set; }
        public int ManufacturerID { get; set; }
        public string VehicleType { get; set; }
        public bool IsActive { get; set; }
    }
    public class AddVariantParam : CommonParam
    {
        [Required(ErrorMessage = "ManufacturerName is required.")]
        public string VariantName { get; set; }
        public int VehicleID { get; set; }
        public int? FuelID { get; set; }
        public int? VehicleCC { get; set; }
        public int? SeatingCapacity { get; set; }
        public decimal? ExShowroomPrice { get; set; }
        public bool IsActive { get; set; }
    }
    public class ListRegionWithBranch:CommonParam
    {
        public List<RegionWithBranch> LstRegionWithBranch { get; set; }
    }
    public class RegionWithBranch
    {
        public int BranchID { get; set; }
        public int RegionID { get; set; }
    }
    public class LoginTimeHis : CommonParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class UpdateUserParam : CommonParam
    {
        public string MobileNo { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public long? Adhaar { get; set; }
    }
    public class MailserverSetupParam : CommonParam
    {
        public string Mailserveroption { get; set; }
        public int MailserveroptionID { get; set; }
        public int? RoleID { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public bool UseDefaultCredential { get; set; }
        public string FromEmail { get; set; }
        public string UserName { get; set; }
        public string PasswordVal { get; set; }
        public bool EnableSsl { get; set; }
    }
    public class MailSelectedOption : CommonParam
    {
        public int MailserveroptionID { get; set; }
    }
    public class MailSelectedOptionWithRole : MailSelectedOption
    {
        public int RoleID { get; set; }
    }
    public class CheckForGotoProposalParam : CommonParam
    {
        public string EnquiryNo { get; set; }
    }
    public class UserId
    {
        public string UserEmail { get; set; }
    }
    public class CamapignsParam : CommonParam
    {
        public List<string> Users { get; set; }
        public string NotifationMessage { get; set; }
        public string Subject { get; set; }
    }
    public class GetOfflineFeatureParam : CommonParam
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class SaveOfflineMotorQuoteParam : CommonParam
    {
        public string Enquiryno { get; set; }
        public string MotorType { get; set; }
        public string PolicyType { get; set; }
        public int? ManufacturerID { get; set; }
        public int? VehicleID { get; set; }
        public int? VariantID { get; set; }
        public int? RegYear { get; set; }
        public int? RTOID { get; set; }
        public string InsurerFilePaths { get; set; }
        public string Remarks { get; set; }
    }
    public class SaveOfflineHealthQuoteParam : CommonParam
    {
        public string Enquiryno { get; set; }
        public int PinCode { get; set; }
        public string MobileNo { get; set; }
        public int Gender { get; set; }
        public int UserAge { get; set; }
        public int SpouseAge { get; set; }
        public int FatherAge { get; set; }
        public int MotherAge { get; set; }
        public List<ChildAge> Childs { get; set; }
        public string InsurerFilePaths { get; set; }
    }
    public class ChildAge
    {
        public string ChildGen { get; set; }
        public int Age { get; set; }
    }
    public class FilterText : CommonParam
    {
        public string FilterUser { get; set; }
    }
    public class EndUserProductDetailsParam : CommonParam
    {
        public string Product { get; set; }
    }
    public class OfflineGotoPayment : CommonParam
    {
        public int EnquiryID { get; set; }
        public int CompanyID { get; set; }
    }
    public class BaseOffline : CommonParam
    {
        public string MotorType { get; set; }
        public string PolicyType { get; set; }
        public decimal BasicOD { get; set; }
        public decimal BasicTP { get; set; }
        public decimal? GrossPremium { get; set; }
        public decimal NetPremium { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal ServiceTax { get; set; }
        public string PolicyNo { get; set; }
        public string EngineNo { get; set; }
        public string ChesisNo { get; set; }
        public string VehicleNo { get; set; }
        public decimal IDV { get; set; }
    }
    public class OfflineUpdatePolicy :BaseOffline
    {
        public int MotorID { get; set; }
        public int UserID { get; set; }
        
    }
    public class LifePolicyParam : CommonParam
    {
        public string EnquiryNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int Cityid { get; set; }
        public int StateID { get; set; }
        public int PinCode { get; set; }
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
        public string PolicyRemark { get; set; }
    }
    public class SMSServerSetupParam:CommonParam
    {
        public string APIURL { get; set; }
        public int MailserveroptionID { get; set; }
    }
    public class VehicleVariantMapping : CommonParam
    {
        public string make { get; set; }
        public string model { get; set; }
        public int varientId { get; set; }
        public int companyId { get; set; }
        public string fuel { get; set; }
        public string vehiletypeId { get; set; }
    }
    public class MapUnmap:CommonParam
    {
        public int companyId { get; set; }
        public int BrokerId { get; set; }
        public int varientId { get; set; }
        public int isMapp { get; set; }
    }
    public class FilterEnquiry : CommonParam
    {
        public string MobileNumber { get; set; }
    }

    public class ImportRenewal:CommonParam
    {
        public IList<RenewalModel> Renewals { get; set; }
    }
    public class RenewalModel
    {
        public string MobileNo { get; set; }
        public string ManufactureID { get; set; }
        public string VehicleID { get; set; }
        public string VariantID { get; set; }
        public string FuelID { get; set; }
        public int RegistartionYear { get; set; }
        public string RTOID { get; set; }
        public string PolicyType { get; set; }
        public string MotorType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string CityID { get; set; }
        public string StateID { get; set; }
        public int PinCode { get; set; }
        public decimal TotalPremium { get; set; }
        public string CompanyID { get; set; }
        public string EngineNo { get; set; }
        public string ChesisNo { get; set; }
        public string VehicleNo { get; set; }
        public DateTime PolicyExpiryDate { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public decimal BasicOD { get; set; }
        public decimal BasicTP { get; set; }
    }
    public class OffLineUpdateHLTPolicyParam : CommonParam
    {
        public sp_GetProcessHLTPaymentGetway Getway { get; set; }
        public string PlanName { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal BasePremium { get; set; }
        public decimal CoverAmount { get; set; }
        public string Policyno { get; set; }
        public int Term { get; set; }
    }
    public class CompaniesparamWithProduct : CommonParam
    {
        public string Product { get; set; }
    }
    public class ConsolidateParam : CommonParam
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class SavePayoutDataParam : CommonParam
    {
        public List<PayoutList> payoutList { get; set; }
    }
    public class PayoutList
    {
        public int User { get; set; }
        public string IRDA { get; set; }
        public int Insurer { get; set; }
        public string Product { get; set; }
        public string RangeFrom { get; set; }
        public string RangeTo { get; set; }
        public decimal Payout { get; set; }
        public string ProductOption { get; set; }
    }
        
    public class RemovePayoutData : CommonParam
    {
        public Insurerpayout PayoutData { get; set; }
    }
    public class GetPayoutData : CommonParam
    {
        public int CompanyID { get; set; }
    }
    public class FilterRoleUser : CommonParam
    {
        public int RoleID { get; set; }
    }
    public class FilterUser : CommonParam
    {
        public int UserID { get; set; }
    }
    public class QuestionData : CommonParam
    {
        public Question[] question { get; set; }
    }

    public class Question
    {
        public string Qstn { get; set; }
        public string Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
    }
    public class UserAuth : CommonParam
    {
        public string URl { get; set; }
    }
    public class MergeUserAlterNateCode : CommonParam
    {
        public string SeprateUsers { get; set; }
        public string AlterNateCode { get; set; }
    }
    public class MergeUserRMCode : CommonParam
    {
        public string SeprateUsers { get; set; }
        public string RMCode { get; set; }
    }
    public class OfflineQueryMessage : CommonParam
    {
        public string EnquiryNo { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
    public class OfflineQueryUpdateMessage : CommonParam
    {
        public int MessageID { get; set; }
    }
    public class MotorHeader : CommonParam
    {
        public string MotorType { get; set; }
    }
    public class FilterPrifix : CommonParam
    {
        public int RoleID { get; set; }
    }
    public class ManualOfflineParam : BaseOffline
    {
        public int UserID { get; set; }
        public int Insurer { get; set; }
        public int Make { get; set; }
        public int Vehicle { get; set; }
        public int Fuel { get; set; }
        public int Variant { get; set; }
        public int ManufacturingMonth { get; set; }
        public int ManufacturingYear { get; set; }
        public string CustomerName { get; set; }
        public DateTime PolicyIssuedate { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public string BusinessType { get; set; }
        public int NCB { get; set; }
        public string ChecqueNo { get; set; }
        public DateTime ChecqueDate { get; set; }
        public string ChecqueBank { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public int PreviousNCB { get; set; }
        public string CubicCapicity { get; set; }
        public string PrevPolicyNO { get; set; }
        public int SelectRTO { get; set; }
        public decimal CPA { get; set; }
        public bool NillDep { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string CustomerPin { get; set; }
        public DateTime? CustomerDOB { get; set; }
        public string CustomerFax { get; set; }
        public string CustomerPANNo { get; set; }
        public decimal GrossDiscount { get; set; }
        public int CustomerCityID { get; set; }
        public int Period { get; set; }
        public string InsuranceType { get; set; }
        public bool IsPospProduct { get; set; }
        public decimal? AddOnPremium { get; set; }
        public string GVW { get; set; }
        public int SeatingCapacity { get; set; }
    }
    public class CreateCLientParam: ClientMasterBase
    {

    }
    public class SuperAdmin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public int ClientID { get; set; }
    }
    public class CheckSuperAdminParam
    {
        public int ClientID { get; set; }
    }
    public class ManageRegion:CommonParam
    {
        public string Option { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public int RegionID { get; set; }
        public int BranchID { get; set; }
    }
    public class GetOffilePDF:CommonParam
    {
        public string PolicyNo { get; set; }
        public string Product { get; set; }
    }
    public class PosSeparater : CommonParam
    {
        public string Poses { get; set; }
    }
    public class BasePosCertificationParam:CommonParam
    {
        public string Certificatedescriptop1{ get; set; }
        public string Certificatedescriptop2{ get; set; }
        public string Certificatedescriptop3{ get; set; }
        public string Certificatedescriptop4{ get; set; }
        public string Certificatedescriptop5 { get; set; }
        public int HeaderHeight { get; set; }
        public int FooterHeight { get; set; }
        public string YourTruly { get; set; }
        public string AuthorisedSignatory { get; set; }
        public string Headerimage { get; set; }
    }
    public class PosQuestionID:CommonParam
    {
        public int QuestionID { get; set; }
    }
    public class SmsTestParam : CommonParam
    {
        public string APIUrl { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
    }
    public class UserDocParam : CommonParam
    {
        public int Userid { get; set; }
    }
    public class SendMessageModel: OfflineQueryMessage
    {
        public int ToUserID { get; set; }
    }
    public class RegionActiveParam : CommonParam
    {
        public int RegionID { get; set; }
        public bool IsAcitve { get; set; }
    }
    public class ResetPassParam
    {
        public string EmailID { get; set; }
        public string OldPassReset { get; set; }
        public string NewPassReset { get; set; }
        public string ConfirmPassReset { get; set; }
        public string ClientURL { get; set; }
    }
    public class GetUserReportParam : CommonParam
    {
        public string Option { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class UserVerificationParam : CommonParam
    {
        public string VerifyMessage { get; set; }
        public bool IsDuplicate { get; set; }
        public string UserIDs { get; set; }
    }
    public class RenewNotificationConfigParam : CommonParam
    {
        public SMSRenewal[] SMSs { get; set; }
        public EmailRenewal[] Emails { get; set; }
    }
    public class SMSRenewal
    {
        public int Day { get; set; }
        public string Body { get; set; }
    }
    public class EmailRenewal
    {
        public int Day { get; set; }
        public string Body { get; set; }
    }
    public class GetNotificationBodyParam:CommonParam
    {
        public int MailOptionID { get; set; }
    }
    public class GetNotificationBodyWithRoleParam : GetNotificationBodyParam
    {
        public int RoleID { get; set; }
    }
    public class SaveMailNotificationBodyParam : CommonParam
    {
        public int MailOptionID { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public int? RoleID { get; set; }
    }
    public class UserCreationResponse
    {
        public string UserEmail { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class ReplaceUserParam:CommonParam
    {
        public string ReplaceUserName { get; set; }
        public string ReplaceEmail { get; set; }
        public string ReplaceMobileNo { get; set; }
        public string ReplaceRmCode { get; set; }
    }
    public class DocCHeckParam : CommonParam
    {
        public string DocName { get; set; }
        public bool IsCheck { get; set; }
        public int UserID { get; set; }
    }
    public class PageScriptParam : CommonParam
    {
        public string[] Url { get; set; }
        public string Script { get; set; }
    }
    public class CnfigSupprtParam : CommonParam
    {
        public string Link { get; set; }
        public bool IsActive { get; set; }
    }
    public class YearConsolidateParam : CommonParam
    {
        public string Year { get; set; }
    }
    public class ShowAboutConsolidateParam : CommonParam
    {
        public string Region { get; set; }
        public string Date { get; set; }
    }
    public class HeaderFooter
    {
        public string Header { get; set; }
        public string Footer { get; set; }
    }
    public class OfflineHealthPolicyParam:CommonParam
    {
        public int ManualselectUser { get; set; }
        public int ManualInsurer { get; set; }
        public string ManualPolicytype { get; set; }
        public string ManualPlanName { get; set; }
        public int ManualAdultCount { get; set; }
        public int ManualChildCount { get; set; }
        public decimal ManualTotalPremium { get; set; }
        public decimal ManualServiceTax { get; set; }
        public decimal ManualCoverAmount { get; set; }
        public decimal ManualBasePremium { get; set; }
        public string ManualPolicyNo { get; set; }
        public string ManualCustomerName { get; set; }
        public string ManualCustomerEmail { get; set; }
        public string ManualCustomerMobile { get; set; }
        public DateTime ManualChecqueDate { get; set; }
        public string ManualChecqueNo { get; set; }
        public string ManualChecqueBank { get; set; }
        public string CustomerAddress { get; set; }
        public string SelectBusinessType { get; set; }
        public int SelectPolicyTerm { get; set; }
        public string InsuranceType { get; set; }
        public int SelectState { get; set; }
        public int SelectCity { get; set; }
        public string Pincode { get; set; }
        public DateTime? CustomerDOB { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PolicyIssueDate { get; set; }
        public bool IsPospProduct { get; set; }
    }
    public class FeedbackOptionParam:CommonParam
    {
        public string Option { get; set; }
    }
    public class FeedbackOptionIDParam : CommonParam
    {
        public int FeedID { get; set; }
    }
    public class AddFadBackParam : CommonParam
    {
        public int SelectedFeedbackID { get; set; }
        public string FeedbackText { get; set; }
        public int Rating { get; set; }
    }
    public class SaveMotoRestrictQuoteLstParam:CommonParam
    {
        public SaveMotoRestrictQuote[] saveRestrictQuote { get; set; }
    }
    public class SaveMotoRestrictQuote
    {
        public string BrokerName { get; set; }
        public bool OnlyPOS { get; set; }
        public bool UserOnly { get; set; }
        public bool Both { get; set; }
    }
    public class SaveHLTRestrictQuote: SaveMotoRestrictQuote
    {
        public decimal? Suminsured { get; set; }
    }
    public class SaveHLTRestrictQuoteLstParam : CommonParam
    {
        public SaveHLTRestrictQuote[] saveRestrictQuote { get; set; }
    }
    public class BulkOfflineManualMotorParam : CommonParam
    {
        public BulkMotorBusinessList[] bulkMotorBusinessList { get; set; }
    }
    public class BulkOfflineManualHLTParam : CommonParam
    {
        public BulkHltBusinessList[] bulkHLTBusinessList { get; set; }
    }
    public class BrokerFetchParam : CommonParam
    {
        public string Product { get; set; }
    }
    public class BithdayWisheshParam:CommonParam
    {
        public string EmpIDCollection { get; set; }
        public string Message { get; set; }
    }
    public class CheckPinParam : CommonParam
    {
        public int PinCode { get; set; }
    }
    public class UpdatePinParam : CheckPinParam
    {
        public string PinState { get; set; }
        public string PinCity { get; set; }
    }
    public class CallRequestResponseParam: FromDateToDate
    {
        public string Action { get; set; }
    }
    public class GetCitiesParam : CommonParam
    {
        public int StateID { get; set; }
    }
    public class GetStateParam : CommonParam
    {
        public int CityID { get; set; }
    }
    public class DownloadUserParam
    {
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public string Option { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class SaveDigitalSignParam:CommonParam
    {
        public string DigitalSignBody { get; set; }
    }
    public class OfflinePolicy : CommonParam
    {
        public string Product { get; set; }
        public string PolicyNo { get; set; }
    }
    public class AgencyCodeParam : CommonParam
    {
        public string AgencyCode { get; set; }
        public string APIUrl { get; set; }
        public string AuthParam { get; set; }
        public string UserName { get; set; }
        public string PasswordParam { get; set; }
        public string RedirectionUrl { get; set; }
    }
    public class SalesPersonCodeParam : CommonParam
    {
        public int UserID { get; set; }
        public string SalesPersonCode { get; set; }
    }
    public class PosDocParam : CommonParam
    {
        public int DocID { get; set; }
    }


    #region Offline Life Policy
    public class OfflineLifePolicyParam : CommonParam
    {
        public int UserID { get; set; }
        public int ClientID { get; set; }
        //public int CreatedID { get; set; }
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

    public class BulkOfflineLifeBusiness
    {
        public string UserEmail { get; set; }
        public string InsurerName { get; set; }
        public string POSCode { get; set; }
        public string POSName { get; set; }
        public string POSSource { get; set; }
        public string ReportingManagerName { get; set; }
        public string RegionalManagerName { get; set; }
        public string CustName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string ProductType { get; set; }
        public string Product { get; set; }
        public string ProductName { get; set; }
        public string PolicyTerm { get; set; }
        public string PremiumPayingTerm { get; set; }
        public string PremiumPayingFrequency { get; set; }
        public string BusinessType { get; set; }
        public string PolicyNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PolicyIssueDate { get; set; }
        public string SumAssured { get; set; }
        public string NetPremium { get; set; }
        public string GST { get; set; }
        public string TotalPremium { get; set; }
        public string Enquiryno { get; set; }
        public string ProductIssuanceType { get; set; }
        public string POSPProduct { get; set; }
    }
    public class BulkOfflineLifeParam : CommonParam
    {
        public BulkOfflineLifeBusiness[] bulkLifeBusinessList { get; set; }
    }
    #endregion

    #region Zoho Signup

    /*
     * By: Sunil
     * Date:2021/08/31
     * Use: to sent sftp mail 
     */
    public class SFTPMail : ClientUser
    {
        public string MailService { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string ToEmail { get; set; }
    }

    public class POSMailingIn
    {
        public int ClientID { get; set; }
        public string MailTo { get; set; }
        public string MailService { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class POSUpload
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public List<PosStampMaster> posList { get; set; }
    }

    public class POSStampUpdate
    {
        public string Token { get; set; }
        public int UserID { get; set; }
        public string StampID { get; set; }
        public string POSCode { get; set; }
        public string POSName { get; set; }
        public DateTime SignDate { get; set; }
        public DateTime IIBDate { get; set; }
    }
    #endregion

    #region Zoop 

    /*
     * By: Sunil
     * Date:2021/09/19
     */
    public class ZoopModel
    {
        public string id { get; set; }
        public string consent { get; set; }
        public string consent_text { get; set; }
        public string env { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }
        public string transaction_status { get; set; }
        public string request_timestamp { get; set; }
        public string response_timestamp { get; set; }
        public ZoopResult result { get; set; }
    }
    public class ZoopResult
    {
        public string blackList_status { get; set; }
        public string chassis_no { get; set; }
        public string engine_no { get; set; }
        public string financier { get; set; }
        public string fitness_upto { get; set; }
        public string fuel { get; set; }
        public string insurance_details { get; set; }
        public string insurance_validity { get; set; }
        public string license_address { get; set; }
        public string maker { get; set; }
        public string mv_tax_upto { get; set; }
        public string owner_name { get; set; }
        public string permit_type { get; set; }
        public string permit_validity { get; set; }
        public string pollution_norms { get; set; }
        public string puc_no_upto { get; set; }
        public string regist_no { get; set; }
        public string registration_date { get; set; }
        public string registration_no { get; set; }
        public string report_id { get; set; }
        public string status { get; set; }
        public string vehicle_class { get; set; }
    }
    #endregion
}