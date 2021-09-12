using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.Model
{
    public class HealthParameters
    {
    }
    public class HealthEnquiry:EnquiryBase
    {
        public int? PinCode { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public bool HStatus { get; set; }
        public string PolicyType { get; set; }
        public int? Gender { get; set; }
        public int? UserAge { get; set; }
        public int? SpouseAge { get; set; }
        public int? FatherAge { get; set; }
        public int? MotherAge { get; set; }
        public int? SonAge1 { get; set; }
        public int? SonAge3 { get; set; }
        public int? SonAge2 { get; set; }
        public int? SonAge4 { get; set; }
        public int? DoughterAge1 { get; set; }
        public int? DoughterAge2 { get; set; }
        public int? DoughterAge3 { get; set; }
        public int? DoughterAge4 { get; set; }
    }
    public class HealthGoToProposalPram
    {
        public string EnquiryNo { get; set; }
        public string QuoteRequest { get; set; }
        public string QuoteResponse { get; set; }
        public int companyID { get; set; }
        public decimal TotalPremium { get; set; }
        public string TransactionRefNo { get; set; }
        public string Policy_Status { get; set; }
        public string QuoteNo { get; set; }
        public decimal NetPremium { get; set; }
        public decimal Service_tax { get; set; }
        public string PlanName { get; set; }
        public decimal CoverAmount { get; set; }
        public decimal BasePremium { get; set; }
        public int Term { get; set; }
    }

    public class UpdateProposal
    {
        public string EnquiryNo { get; set; }
        public string ProposalRequest { get; set; }
        public string ProposalResponse { get; set; }
        public string ProposalNo { get; set; }
        public string Policy_Status { get; set; }
        public string PlanName { get; set; }
        public decimal TotalPremium { get; set; }
        public decimal Service_tax { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Mobile { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelationship { get; set; }
        public DateTime? NomineeDob { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int PinCode { get; set; }
    }
    public class HealthGotoPaymentDataParam
    {
        public string EnquiryNo { get; set; }
    }
}
