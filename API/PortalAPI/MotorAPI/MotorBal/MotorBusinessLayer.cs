using API.DAL;
using API.DbManager;
using API.DbManager.DbModels;
using API.Model;
using API.PortalAPI.MotorAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.MotorBal
{
    public class MotorBusinessLayer : IMotorBusinessLayer
    {
        #region Get Quote Time
        public string SaveEnquiry(MotorEnquiry Item)
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
            param.Add("@p_ManufactureID", Item.ManufactureID.ToString());
            param.Add("@p_VehicleID", Item.VehicleID.ToString());
            param.Add("@p_VariantID", Item.VariantID.ToString());
            param.Add("@p_FuelID", Item.FuelID.ToString());
            param.Add("@p_RegistartionYear", Item.RegistartionYear.ToString());
            param.Add("@p_RTOID", Item.RTOID.ToString());
            param.Add("@p_PolicyType", Item.PolicyType.ToString());
            param.Add("@p_MotorType", Item.MotorType.ToString());
            var dataList = Data.ProcedureOutput("sp_Enquiry", param);
            if (dataList.Contains("Exception"))
            {

            }
            return dataList;
        }
        #endregion
        #region Go to Proposal Data
        public string SaveGotoProposal(GoToproposal Item)
        {
            var regConvertDate = Item.RegDate ==null? Convert.ToDateTime("1970-01-01") : Convert.ToDateTime(Item.RegDate);
            var prvConvertDate = Item.PrvPolicyExDate==null? Convert.ToDateTime("1970-01-01") : Convert.ToDateTime(Item.PrvPolicyExDate);
            Item.vehicleManufactureYear = Item.vehicleManufactureYear == null ? "-1" : Item.vehicleManufactureYear;
            Item.vehicleManufactureMonth = Item.vehicleManufactureMonth == null ? "-1" : Item.vehicleManufactureMonth;
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EnquiryNo", Item.EnquiryNo);
            param.Add("@p_BasicOD", Item.BasicOD.ToString());
            param.Add("@p_BasicTP", Item.BasicTP.ToString());
            param.Add("@p_GrossPremium", Item.GrossPremium.ToString());
            param.Add("@p_NetPremium", Item.NetPremium.ToString());
            param.Add("@p_TotalPremium", Item.TotalPremium.ToString());
            param.Add("@p_ServiceTax", Item.ServiceTax.ToString());
            param.Add("@p_QuoteNo", Item.QuoteNo.ToString());
            param.Add("@p_CompanyID", Item.CompanyID.ToString());
            param.Add("@p_PolicyStatus", Item.PolicyStatus.ToString());
            //param.Add("@p_RequestQuoteXml", Item.RequestQuoteXml.ToString());
            //param.Add("@p_ResponseQuoteXml", Item.ResponseQuoteXml.ToString());
            param.Add("@p_Period", Item.Period.ToString());
            param.Add("@p_IDV", Item.IDV.ToString());
            param.Add("@p_CurrentNCB", Item.CurrentNCB.ToString());
            param.Add("@p_PreviousNCB", Item.PreviousNCB.ToString());
            param.Add("@p_SeatingCapacity", Item.SeatingCapacity.ToString());
            param.Add("@p_CC", Item.CC.ToString());
            param.Add("@p_AddonsIDs", Item.AddonsIDs.ToString());
            param.Add("@p_PolicyType", Item.PolicyType.ToString());
            param.Add("@p_MotorType", Item.MotorType.ToString());
            param.Add("@p_RegistrationYear", regConvertDate.Year.ToString());
            param.Add("@p_RegistrationMonth", regConvertDate.Month.ToString());
            param.Add("@p_RegistrationDay", regConvertDate.Day.ToString());
            param.Add("@p_PreviousPolicyExpYear", prvConvertDate.Year.ToString());
            param.Add("@p_PreviousPolicyExpMonth", prvConvertDate.Month.ToString());
            param.Add("@p_PreviousPolicyExpDay", prvConvertDate.Day.ToString());
            param.Add("@p_ManufactureYear", Item.vehicleManufactureYear.ToString());
            param.Add("@p_ManufactureMonth", Item.vehicleManufactureMonth.ToString());
            var dataList = Data.ProcedureOutput("sp_GotoProposal", param);
            SaveReqRespData(Item.EnquiryNo, Item.RequestQuoteXml, Item.ResponseQuoteXml);
            return dataList;
        }
        private string SaveReqRespData(string EnquiryNo, string Req, string Res)
        {
            string Response = "";
            IDataLayer<DbManager.DbModels.Enquiry> en = new DataLayer<DbManager.DbModels.Enquiry>();
            var CheckEnquiryType = en.GetSingelDetailWithCondition(row => row.EnquiryNo == EnquiryNo && row.EnquiryType != "HLT");

            IDataLayer<SaveRequestResponseData> dataReqRes = new DataLayer<SaveRequestResponseData>();
            var CheckExistence = dataReqRes.GetSingelDetailWithCondition(row => row.EnquiryNo == EnquiryNo && row.Product== CheckEnquiryType.EnquiryType);
            dataReqRes = null;
            dataReqRes = new DataLayer<SaveRequestResponseData>();
            if (CheckExistence != null)
            {
                CheckExistence.RequestQuoteXml = Req;
                CheckExistence.ResponseQuoteXml = Res;
                CheckExistence.Product = CheckEnquiryType.EnquiryType;
                Response = dataReqRes.Update(CheckExistence);
            }
            else
            {
                SaveRequestResponseData obj = new SaveRequestResponseData()
                {
                    EnquiryNo = EnquiryNo,
                    RequestQuoteXml = Req,
                    ResponseQuoteXml = Res,
                    Product= CheckEnquiryType.EnquiryType
                };
                Response = dataReqRes.InsertRecord(obj);
            }
            return Response;
        }
        #endregion
        #region Go to Policydetails
        public string Policydetails(PolicydetailsParam Item)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EnquiryNo", Item.EnquiryNo);
            param.Add("@p_ProposalNo", Item.ProposalNo.ToString());
            param.Add("@p_PolicyNo", Item.PolicyNo.ToString());
            param.Add("@p_EngineNo", Item.EngineNo.ToString());
            param.Add("@p_ChesisNo", Item.ChesisNo.ToString());
            param.Add("@p_PrevPolicyNO", Item.PrevPolicyNO.ToString());
            param.Add("@p_PreviousInsurer", Item.PreviousInsurer.ToString());
            param.Add("@p_VehicleNo", Item.VehicleNo.ToString());
            param.Add("@p_PolicyStatus", Item.PolicyStatus.ToString());
            param.Add("@p_Type", Item.Type.ToString());
            param.Add("@p_PaymentStatus", Item.PaymentStatus.ToString());
            param.Add("@p_FirstName", Item.FirstName.ToString());
            param.Add("@p_LastName", Item.LastName.ToString());
            param.Add("@p_Email", Item.Email.ToString());
            param.Add("@p_Gender", Item.Gender.ToString());
            param.Add("@p_MaritalStatus", Item.MaritalStatus.ToString());
            param.Add("@p_DateOfBirth", Item.DateOfBirth.ToString("yyyy-MM-dd"));
            param.Add("@p_AddressLine1", Item.AddressLine1.ToString());
            param.Add("@p_AddressLine2", Item.AddressLine2.ToString());
            param.Add("@p_AddressLine3", Item.AddressLine3.ToString());
            param.Add("@p_CityID", Item.CityID.ToString());
            param.Add("@p_StateID", Item.StateID.ToString());
            param.Add("@p_PinCode", Item.PinCode.ToString());
            param.Add("@p_CountryID", Item.CountryID.ToString());
            //param.Add("@p_IsPrimary", Item.IsPrimary.ToString());
            param.Add("@p_IsPrimary", Item.IsPrimary ? "1" : "0");
            param.Add("@p_NomineeAge", Item.NomineeAge.ToString());
            param.Add("@p_NomineeName", Item.NomineeName.ToString());
            param.Add("@p_NomineeRelationship", Item.NomineeRelationship.ToString());
            param.Add("@p_RequestQuoteXml", Item.RequestProposalXml.ToString());
            param.Add("@p_ResponseQuoteXml", Item.ResponseProposalXml.ToString());
            param.Add("@p_MobileNo", Item.MobileNo);
            var dataList = Data.ProcedureOutput("sp_Policydetails", param);
            return dataList;
        }
        #endregion

        #region Get or Update
        public dynamic DynamicGet(updateTable Param)
        {
            dynamic data = null;
            switch (Param.TableName)
            {
                case "Users":
                    IDataLayer<Users> users = new DataLayer<Users>();
                    data = users.GetSingelDetailWithCondition(row => row.EnquiryNo == Param.EnquiryNo);
                    break;
                case "Motorpolicydetails":
                    IDataLayer<Motorenquiry> mQuery = new DataLayer<Motorenquiry>();
                    var data1 = mQuery.GetSingelDetailWithCondition(row => row.EnquiryNo == Param.EnquiryNo);
                    IDataLayer<Motorpolicydetails> policydetails = new DataLayer<Motorpolicydetails>();
                    data = policydetails.GetSingelDetailWithCondition(row => row.MotorID == data1.MotorID);
                    break;
                case "Motorenquiry":
                    IDataLayer<Motorenquiry> motQuery = new DataLayer<Motorenquiry>();
                    data = motQuery.GetSingelDetailWithCondition(row => row.EnquiryNo == Param.EnquiryNo);
                    break;
                case "Clientmaster":
                    IDataLayer<ClientMaster> Client = new DataLayer<ClientMaster>();
                    data = Client.GetSingelDetailWithCondition(row => row.companyURL.Contains(Param.URL));
                    break;
            }
            return data;
        }
        public string DynamicSet(updateTable Param)
        {
            string Response = "";
            switch (Param.TableName)
            {
                case "Users":
                    Users Item = Param.UsersData;
                    IDataLayer<Users> users = new DataLayer<Users>();
                    Response = users.Update(Item);
                    break;
                case "Motorpolicydetails":
                    Motorpolicydetails Motor = Param.MotorpolicydetailsData;
                    IDataLayer<Motorpolicydetails> policydetails = new DataLayer<Motorpolicydetails>();
                    Response = policydetails.Update(Motor);
                    break;
                case "Motorenquiry":
                    Motorenquiry MotorE = Param.MotorenquiryData;
                    IDataLayer<Motorenquiry> Motordetails = new DataLayer<Motorenquiry>();
                    Response = Motordetails.Update(MotorE);
                    break;
            }
            return Response;
        }
        #endregion Get or Update

        #region Queries
        public string UpdateQuery(string query)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Query", query);
            var dataList = Data.ProcedureOutput("sp_ExecuteQuery", param);
            return dataList;
        }
        public dynamic SelectQuery(string query)
        {
            string obj = "";
            var data = DbHelper.ExecuteSQLQuery(query);
            if (data != null)
            {
                obj = JsonConvert.SerializeObject(data);
            }
            return obj;
        }
        #endregion Queries

        #region RegisterPos
        public string RegisterPos(RegisterPos Item)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", Item.ClientID.ToString());
            param.Add("@p_FirstName", Item.FirstName);
            param.Add("@p_LastName", Item.LastName);
            param.Add("@p_Email", Item.Email);
            param.Add("@p_Mobile", Item.Mobile);
            param.Add("@p_AadhaarNo", Item.AadhaarNo);
            param.Add("@p_PanNo", Item.PanNo);
            param.Add("@p_DOB", Item.DOB == null ? "1888-08-08" : Item.DOB.Value.ToString("yyyy-MM-dd"));
            param.Add("@p_Address", Item.Address);
            param.Add("@p_LeadSource", Item.LeadSource);
            param.Add("@UserID", Item.userid.ToString());
            param.Add("@Qualification", Item.Qualification);
            var dataList = Data.ProcedureOutput("sp_RegisterPos", param);
            return dataList;
        }
        #endregion Queries

        #region ENd User Mail
        public string SendEmailInfoToEndUser(SendmailToUser Item)
        {
            string Response = "";
            IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
            var ContainClientData = ClientData.GetSingelDetailWithCondition(row => row.companyURL.ToLower().Contains(Item.URL.ToLower()));
            if (ContainClientData != null)
            {
                IDataLayer<MailServerOption> mailOPT = new DataLayer<MailServerOption>();
                var ContainmailOPT = mailOPT.GetSingelDetailWithCondition(row => row.MailServiceMaster.ToLower().Contains("enduser payment"));
                if (ContainmailOPT != null)
                {
                    IDataLayer<MailServer> mailServer = new DataLayer<MailServer>();
                    var MailServerContain = mailServer.GetSingelDetailWithCondition(row => row.ClientID == ContainClientData.Id && row.MailserveroptionID == ContainmailOPT.ID);
                    if (MailServerContain != null)
                    {
                        string Pass = Guid.NewGuid().ToString().ToUpper().Substring(0, 4);
                        Pass = Item.Email.Substring(0, 3) + "@" + Pass;
                        string Body = "Email : " + Item.Email + "<br/>";
                        Body += "Pass : " + Pass;

                        IDataLayer<Users> UserData = new DataLayer<Users>();
                        var IsSuccess = UserData.GetSingelDetailWithCondition(row => row.Email == Item.Email && row.ClientID == ContainClientData.Id);
                        if (IsSuccess != null)
                        {
                            UserData = null;
                            UserData = new DataLayer<Users>();
                            IsSuccess.Password = Pass;
                            UserData.Update(IsSuccess);
                            CommonMethods.Common.mailMaster(MailServerContain.FromEmail, Item.Email, "Secure Auth",
                                Body, MailServerContain.FromEmail, MailServerContain.Password, MailServerContain.HostName,
                                MailServerContain.Port, MailServerContain.UseDefaultCredential, MailServerContain.EnableSsl, ref Response);
                        }
                    }
                    else
                        Response = "Mail server Not Configure.";
                }
                else
                    Response = "Mail Option Not Found.";
            }
            else
                Response = "Client Url not found";
            return Response;
        }
        #endregion ENd User Mail

        #region GetEnquiryNo
        public sp_GetEnquiryNo GenEnquiryNo(GenEnquiryNoParam Item)
        {
            try
            {
                IDataLayer<sp_GetEnquiryNo> Data = new DataLayer<sp_GetEnquiryNo>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_EnquiryType", Item.EnquiryType.ToString());
                param.Add("@p_ClientID", Item.ClientID.ToString());
                var dataList = Data.ProceduresGetData("sp_GetEnquiryNo", param);
                return dataList.FirstOrDefault();
            }
            catch (Exception ex) { return null; }
        }
        #endregion GetEnquiryNo

        #region Dead Lock
        public void DuringDeadLock(string Model,string Req,string Message,int ClientID)
        {
            IDataLayer<ClientDBConnection> clientDBConnection = new DataLayer<ClientDBConnection>();
            var data = clientDBConnection.GetSingelDetailWithCondition(row => row.ClientID == ClientID);
            if (data != null)
            {
                string Query = "insert into StoreDataDuringDeadlock(Model,Req,ExceptionMessage,ClientID) values('"+ Model + "','"+Req+"','"+ Message + "',"+ClientID+")";
                SqlDBHelper.ExecuteNonSQLQuery(Query, data.ConnectionString);
            }
        }
        #endregion  Dead Lock
    }
}
