using API.DbManager.DbModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.Model
{
    public class ParameterModel
    {
    }
    public class EnquiryBase
    {
        public string EnquiryNo { get; set; }
        public string MobileNo { get; set; }
        public string EnquiryType { get; set; }
        public int Status { get; set; }
        public string LeadSource { get; set; }
        public int Userid { get; set; }
        public int ClientID { get; set; }
        public string macid { get; set; }
    }
    public class MotorEnquiry:EnquiryBase
    {
        public int ManufactureID { get; set; }
        public int VehicleID { get; set; }
        public int VariantID { get; set; }
        public int FuelID { get; set; }
        public int RegistartionYear { get; set; }
        public int RTOID { get; set; }
        public string PolicyType { get; set; }
        public string MotorType { get; set; }
    }
    public class LifeEnquiry : EnquiryBase
    {
        public int YourAge { get; set; }
        public string Gender { get; set; }
        public string SmokeStaus { get; set; }
        public decimal AnnualInCome { get; set; }
        public decimal PreferredCover { get; set; }
        public int CoverageAge { get; set; }
        public string PolicyType { get; set; }
    }
    public class GoToproposal
    {
        public string EnquiryNo { get; set; }
        public decimal BasicOD { get; set; }
        public decimal BasicTP { get; set; }
        public decimal GrossPremium { get; set; }
        public decimal NetPremium { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal ServiceTax { get; set; }
        public string QuoteNo { get; set; }
        public int CompanyID { get; set; }
        public string PolicyStatus { get; set; }
        public string RequestQuoteXml { get; set; }
        public string ResponseQuoteXml { get; set; }
        public int Period { get; set; }
        public decimal IDV { get; set; }
        public int CurrentNCB { get; set; }
        public int PreviousNCB { get; set; }
        public int SeatingCapacity { get; set; }
        public int CC { get; set; }
        public string AddonsIDs { get; set; }
        public string PolicyType { get; set; }
        public string MotorType { get; set; }
        public string RegDate { get; set; }
        public string vehicleManufactureYear { get; set; }
        public string vehicleManufactureMonth { get; set; }
        public string PrvPolicyExDate { get; set; }
    }
    public class PolicydetailsParam
    {
        public string EnquiryNo { get; set; }
        public string ProposalNo { get; set; }
        public string PolicyNo { get; set; }
        public string EngineNo { get; set; }
        public string ChesisNo { get; set; }
        public string PrevPolicyNO { get; set; }
        public string PreviousInsurer { get; set; }
        public string VehicleNo { get; set; }
        public string PolicyStatus { get; set; }
        public string Type { get; set; }
        public string PaymentStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int PinCode { get; set; }
        public int CountryID { get; set; }
        public bool IsPrimary { get; set; }
        public int NomineeAge { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelationship { get; set; }
        public string RequestProposalXml { get; set; }
        public string ResponseProposalXml { get; set; }
        public string MobileNo { get; set; }
    }

    public class updateTable
    {
        public string TableName { get; set; }
        public string EnquiryNo { get; set; }
        public string GetOrSet { get; set; }
        public string URL { get; set; }
        public Users UsersData { get; set; }
        public Motorpolicydetails MotorpolicydetailsData { get; set; }
        public Motorenquiry MotorenquiryData { get; set; }
    }

    public class QueryParam
    {
        public string Option { get; set; }
        public string Query { get; set; }
        public string Response { get; set; }
        public dynamic Table { get; set; }
    }
    public class RegisterPos
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AadhaarNo { get; set; }
        public string PanNo { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string LeadSource { get; set; }
        public int userid { get; set; }
        public string Qualification { get; set; }
    }
    public class SendmailToUser
    {
        public string Email { get; set; }
        public string URL { get; set; }
        public string DomainURL { get; set; }        
    }
    public class GenEnquiryNoParam
    {
        public string EnquiryType { get; set; }
        public int ClientID { get; set; }
    }
}
