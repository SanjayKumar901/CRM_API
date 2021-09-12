using API.DAL;
using API.DbManager.DbModels;
using API.PortalAPI.MotorAPI.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace API.PortalAPI.MotorAPI.HealthBal
{
    public class HealthBusinessLayer : IHealthBusinessLayer
    {
        public string SaveEnquiry(HealthEnquiry Item)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EnquiryNo", Item.EnquiryNo);
            param.Add("@p_MobileNo", Item.MobileNo);
            param.Add("@p_EnquiryType", Item.EnquiryType);
            param.Add("@p_Status", Item.Status.ToString());
            param.Add("@p_LeadSource", Item.LeadSource);
            param.Add("@p_Userid", Item.Userid.ToString());
            param.Add("@p_ClientID", Item.ClientID.ToString());
            param.Add("@p_macid", Item.macid);
            param.Add("@p_PinCode", Item.PinCode.ToString());
            param.Add("@p_AdultCount", Item.AdultCount.ToString());
            param.Add("@p_ChildCount", Item.ChildCount.ToString());
            param.Add("@p_hltStatus", Item.HStatus ? "1" : "0");
            //param.Add("@p_hltStatus", Item.HStatus.ToString());
            param.Add("@p_PolicyType", Item.PolicyType.ToString());
            param.Add("@p_Gender", Item.Gender.ToString());
            param.Add("@p_UserAge", Item.UserAge.ToString());
            param.Add("@p_SpouseAge", Item.SpouseAge.ToString());
            param.Add("@p_FatherAge", Item.FatherAge.ToString());
            param.Add("@p_MotherAge", Item.MotherAge.ToString());
            param.Add("@p_SonAge1", Item.SonAge1.ToString());
            param.Add("@p_SonAge3", Item.SonAge3.ToString());
            param.Add("@p_SonAge2", Item.SonAge2.ToString());
            param.Add("@p_SonAge4", Item.SonAge4.ToString());
            param.Add("@p_DoughterAge1", Item.DoughterAge1.ToString());
            param.Add("@p_DoughterAge2", Item.DoughterAge2.ToString());
            param.Add("@p_DoughterAge3", Item.DoughterAge3.ToString());
            param.Add("@pDoughterAge4", Item.DoughterAge4.ToString());
            var Response = Data.ProcedureOutput("sp_HealthEnquiry", param);
            return Response;
        }
        public string GotoProposal(HealthGoToProposalPram Item)
        {
            string Response = "";
            try
            {
                IDataLayer<dynamic> Data = new DataLayer<dynamic>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_EnquiryNo", Item.EnquiryNo);
                param.Add("@p_QuoteRequest", Item.QuoteRequest);
                param.Add("@p_QuoteResponse", Item.QuoteResponse);
                param.Add("@p_companyID", Item.companyID.ToString());
                param.Add("@p_TotalPremium", Item.TotalPremium.ToString());
                param.Add("@p_TransactionRefNo", Item.TransactionRefNo);
                param.Add("@p_Policy_Status", Item.Policy_Status);
                param.Add("@p_QuoteNo", Item.QuoteNo);
                param.Add("@p_NetPremium", Item.NetPremium.ToString());
                param.Add("@p_Service_tax", Item.Service_tax.ToString());
                param.Add("@p_PlanName", Item.PlanName);
                param.Add("@p_CoverAmount", Item.CoverAmount.ToString());
                param.Add("@p_BasePremium", Item.BasePremium.ToString());
                param.Add("@p_Term", Item.Term.ToString());
                Response = Data.ProcedureOutput("sp_GotoHealthProposal", param);
                return Response;
            }
            catch (Exception ex) { Response = ex.Message; }
            return Response;
        }
        public string UpdateProposalDetails(UpdateProposal Item)
        {
            string Response = "";
            try
            {
                IDataLayer<dynamic> Data = new DataLayer<dynamic>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_EnquiryNo", Item.EnquiryNo);
                param.Add("@p_ProposalRequest", Item.ProposalRequest);
                param.Add("@p_ProposalResponse", Item.ProposalResponse);
                param.Add("@p_ProposalNo", Item.ProposalNo);
                param.Add("@p_Policy_Status", Item.Policy_Status);
                param.Add("@p_PlanName", Item.PlanName);
                param.Add("@p_TotalPremium", Item.TotalPremium.ToString());
                param.Add("@p_Service_tax", Item.Service_tax.ToString());
                param.Add("@p_FirstName", Item.FirstName);
                param.Add("@p_LastName", Item.LastName);
                param.Add("@p_MaritalStatus", Item.MaritalStatus.ToString());
                param.Add("@p_DateOfBirth", Item.DateOfBirth.Value.ToString("yyyy-MM-dd"));
                param.Add("@p_Email", Item.Email.ToString());
                param.Add("@p_Gender", Item.Gender.ToString());
                param.Add("@p_Mobile", Item.Mobile);
                param.Add("@p_NomineeName", Item.NomineeName);
                param.Add("@p_NomineeRelationship", Item.NomineeRelationship);
                param.Add("@p_NomineeDob", Item.NomineeDob.Value.ToString("yyyy-MM-dd"));
                param.Add("@p_AddressLine1", Item.AddressLine1);
                param.Add("@p_AddressLine2", Item.AddressLine2);
                param.Add("@p_AddressLine3", Item.AddressLine3);
                param.Add("@p_CityID", Item.CityID.ToString());
                param.Add("@p_StateID", Item.StateID.ToString());
                param.Add("@p_PinCode", Item.PinCode.ToString());
                Response = Data.ProcedureOutput("sp_UpdateProposal", param);
                return Response;
            }
            catch (Exception ex) { Response = ex.Message; }
            return Response;
        }
        public HealthGotoPaymentData GetHealthGotoPaymentData(HealthGotoPaymentDataParam Item)
        {
            IDataLayer<HealthGotoPaymentData> data = new DataLayer<HealthGotoPaymentData>();
            return data.GetSingelDetailWithCondition(row => row.EnquiryNo == Item.EnquiryNo);
        }
    }
}
