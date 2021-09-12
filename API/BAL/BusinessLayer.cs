using API.CommonMethods;
using API.DAL;
using API.DbManager;
using API.DbManager.DbModels;
using API.Model;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Method = RestSharp.Method;

namespace API.BAL
{
    public class BusinessLayer : IBusinessLayer
    {
        int counter = 0;
        #region DomainMapping
        public ClientMaster GetClientData(UrlBase Item)
        {

            IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
            var ss = data.GetSingelDetailWithCondition(row => row.companyURL.ToLower().Contains(Item.URL.ToLower()));
            return ss;
        }
        #endregion DomainMapping
        #region Home Controller
        public IEnumerable<SP_PRIVILAGES> UserPrevileges(Privilege paramModal)
        {
            IEnumerable<SP_PRIVILAGES> dataList = new List<SP_PRIVILAGES>();
            UserModel Umodel = Common.DecodeToken(paramModal.Token);
            IDataLayer<SP_PRIVILAGES> Data = new DataLayer<SP_PRIVILAGES>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            string DocStatus;
            param.Add("@P_userId", paramModal.Userid.ToString());
            param.Add("@p_privilegeID", paramModal.Privilegeid.ToString());
            if (Umodel.RoleID == 8)
            {
                if (IsActivePos(Umodel.UserID, Umodel.ClientID.Value, out DocStatus))
                    dataList = Data.ProceduresGetData("SP_PRIVILAGES", param);
            }
            else
            {
                dataList = Data.ProceduresGetData("SP_PRIVILAGES", param);
            }
            return dataList;
        }
        public sp_DashboardHeader DahsboardHeader(ClientUser paramModal)
        {
            if (!CheckDashBoardPrivilege(paramModal.Userid, "Dashboard Header"))
                return null;
            IDataLayer<sp_DashboardHeader> Data = new DataLayer<sp_DashboardHeader>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", paramModal.ClientID.ToString());
            param.Add("@p_userid", paramModal.Userid.ToString());
            var dataList = Data.ProceduresGetData("sp_DashboardHeader", param);
            if (dataList == null)
                return null;
            else
                return dataList.ToList()[0];
        }
        private bool CheckDashBoardPrivilege(int UserID, string privilegename)
        {
            IDataLayer<sp_CheckDashBoardPrivilege> Data = new DataLayer<sp_CheckDashBoardPrivilege>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", UserID.ToString());
            param.Add("@p_privilegename", privilegename);
            var dataList = Data.ProceduresGetData("sp_CheckDashBoardPrivilege", param);
            if (dataList == null)
                return false;
            else
            {
                return dataList.ToList()[0].IsTrue;
            }
        }
        public IEnumerable<sp_LeadDetailGrid> DashboardLeadGrid(FromDateToDate paramModal)
        {

            if (!CheckDashBoardPrivilege(paramModal.Userid, "Lead Details"))
                return null;
            IDataLayer<sp_LeadDetailGrid> Data = new DataLayer<sp_LeadDetailGrid>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", paramModal.Userid.ToString());
            param.Add("@p_clientid", paramModal.ClientID.ToString());
            param.Add("@p_startdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@p_enddate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_LeadDetailGrid", param);
            return dataList;
        }
        public IEnumerable<SP_BUSINESSBYCITY> BUSINESSBYCITY(BusinessbyCityParam item)
        {
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            item.Userid = model.UserID;
            item.ClientID = model.ClientID.Value;
            IDataLayer<SP_BUSINESSBYCITY> Data = new DataLayer<SP_BUSINESSBYCITY>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_CLIENTID", item.ClientID.ToString());
            param.Add("@p_FROMDATE", item.fromDate.ToString("yyyy-MM-dd"));
            param.Add("@p_TODATE", item.todate.ToString("yyyy-MM-dd"));
            param.Add("@p_UserId", item.Userid.ToString());
            var dataList = Data.ProceduresGetData("SP_BUSINESSBYCITY", param);
            return dataList;
        }
        public sp_QuoteInfo EnquiryInfo(EnquiryInfo item)
        {
            IDataLayer<sp_QuoteInfo> Data = new DataLayer<sp_QuoteInfo>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            param.Add("@p_EnquiryNo", item.EnquiryNo);
            param.Add("@p_Clientid", model.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_QuoteInfo", param);
            return dataList.ToList()[0];
        }
        public sp_hltQouteInfo HltEnquiryInfo(EnquiryInfo item)
        {
            IDataLayer<sp_hltQouteInfo> Data = new DataLayer<sp_hltQouteInfo>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            param.Add("@p_EnquiryNo", item.EnquiryNo);
            param.Add("@p_Clientid", model.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_hltQouteInfo", param);
            return dataList.ToList()[0];
        }
        public sp_HltAfterQuoteInfo HltEnquiryInfoAfterQuote(EnquiryInfo item)
        {
            IDataLayer<sp_HltAfterQuoteInfo> Data = new DataLayer<sp_HltAfterQuoteInfo>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            param.Add("@p_Enquiryno", item.EnquiryNo);
            param.Add("@p_Clientid", model.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_HltAfterQuoteInfo", param);
            return dataList.ToList()[0];
        }
        public sp_AfterQuoteInfo EnquiryInfoAfterQuote(EnquiryInfo item)
        {
            IDataLayer<sp_AfterQuoteInfo> Data = new DataLayer<sp_AfterQuoteInfo>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            param.Add("@p_EnquiryNo", item.EnquiryNo);
            param.Add("@p_Clientid", model.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_AfterQuoteInfo", param);
            return dataList.ToList()[0];
        }
        public Response_Register_as_pos GetPosRegistrationData(RegisterPosParam Item)
        {
            UserModel Um = Common.DecodeToken(Item.Token);

            if (!CheckDashBoardPrivilege(Um.UserID, Item.DetailAction))//"POS Request Details"))
                return null;
            Response_Register_as_pos RAP_Obj = new Response_Register_as_pos();
            RAP_Obj.RoleID = Um.RoleID.Value;

            IDataLayer<sp_PosRequestData> Data = new DataLayer<sp_PosRequestData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_clientID", Um.ClientID.ToString());
            param.Add("@p_userID", Um.UserID.ToString());
            param.Add("@p_fromDate", Item.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@p_toDate", Item.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_PosRequestData", param);
            RAP_Obj.Register_as_poss = dataList;
            return RAP_Obj;
        }
        public RMWIthMobile AcitveOrNot(CommonParam Item)
        {
            RMWIthMobile Rmmodel = new RMWIthMobile();
            UserModel um = Common.DecodeToken(Item.Token);
            string DOcStatus;
            if (um.RoleID == 8)
            {
                Rmmodel.active = IsActivePos(um.UserID, um.ClientID.Value, out DOcStatus);
                if (!Rmmodel.active)
                {
                    IDataLayer<UserMaster> UserM = new DataLayer<UserMaster>();
                    var UserDetail = UserM.GetSingelDetailWithCondition(Row => Row.UserID == um.UserID);
                    UserM = null;
                    UserM = new DataLayer<UserMaster>();
                    var RmDetails = UserM.GetSingelDetailWithCondition(Row => Row.UserID == UserDetail.KeyAccountManager);
                    Rmmodel.RmName = RmDetails.UserName;
                    Rmmodel.MobileNo = UserDetail.MobileNo;
                    Rmmodel.DOcStatus = DOcStatus;
                    Rmmodel.userDocWithStatus = DOcStatus == "QC Approval is awaited" ? UserDOC(um.UserID) : null;
                }
            }
            else
                Rmmodel.active = true;
            Rmmodel.IsAffilate = um.RoleID == 29 ? true : false;
            return Rmmodel;
        }
        private bool IsActivePos(int UserID, int ClientID, out string Status)
        {
            Status = "";
            bool docRequirement = false;
            IDataLayer<UserMaster> UserM = new DataLayer<UserMaster>();
            var model = UserM.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.UserID == UserID);
            docRequirement = model.IsDocRequred == null ? false : model.IsDocRequred.Value;
            IDataLayer<PosUserActiveDuration> data = new DataLayer<PosUserActiveDuration>();
            var hourModel = data.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.RoleID == 8);
            if (hourModel == null)
            {
                return true;
            }
            else
            {
                IDataLayer<PosExamStart> ExamSatus = new DataLayer<PosExamStart>();
                var existExamTest = ExamSatus.GetSingelDetailWithCondition(row => row.UserID == UserID);
                var hours = (DateTime.Now - model.CreatedDate).TotalHours;
                DateTime dtCheckPosOldOrNew = Convert.ToDateTime("2020-11-21");
                if (model.CreatedDate < dtCheckPosOldOrNew)
                {
                    return true;
                }
                else
                {
                    if (existExamTest == null)
                    {
                        return false;
                    }
                    else if (hours >= hourModel.HourDuration && existExamTest.PassOrFail == "Pass")
                    {
                        if (!docRequirement)
                        {
                            Status = "Doc not Required";
                            return true;
                        }
                        else
                        {
                            IDataLayer<UserDocument> DocSatus = new DataLayer<UserDocument>();
                            var CHeckExistDoc = DocSatus.GetSingelDetailWithCondition(row => row.UserID == UserID);
                            if (CHeckExistDoc == null)
                                Status = "Please complete your Profile details first";
                            else if (CHeckExistDoc.DocVerified == false || CHeckExistDoc.DocVerified == null)
                            {
                                Status = "QC Approval is awaited";
                            }
                            if (CHeckExistDoc != null && CHeckExistDoc.DocVerified == true)
                                return true;
                            else
                                return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        private List<UserDocWithStatus> UserDOC(int UserID)
        {
            IDataLayer<UserDocument> DocSatus = new DataLayer<UserDocument>();
            var Docs = DocSatus.GetSingelDetailWithCondition(row => row.UserID == UserID);
            List<UserDocWithStatus> lstDoc = new List<UserDocWithStatus>();
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.Adhaar_Back_URL == null ? "Adhaar Back Side Not Uploaded" : "Adhaar Back Side",
                Flag = Docs.Adhaar_Back_URL == null ? 3 : Docs.IsAdharBackCheck == null ? 0 : Convert.ToInt32(Docs.IsAdharBackCheck)
            });
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.Adhaar_Front_URL == null ? "Adhaar Front Side Not Uploaded" : "Adhaar Front Side",
                Flag = Docs.Adhaar_Front_URL == null ? 3 : Docs.IsAdharBackCheck == null ? 0 : Convert.ToInt32(Docs.IsAdharFrontCheck)
            });
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.CancelCheque_URL == null ? "Cancel Cheque Not Uploaded" : "Cancel Cheque",
                Flag = Docs.CancelCheque_URL == null ? 3 : Docs.IsCancelChequeCheck == null ? 0 : Convert.ToInt32(Docs.IsCancelChequeCheck)
            });
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.GST_CERTIFICATE_URL == null ? "GST Certificate Not Uploaded" : "GST Certificate",
                Flag = Docs.GST_CERTIFICATE_URL == null ? 3 : Docs.IsGSTCertificateCheck == null ? 0 : Convert.ToInt32(Docs.IsGSTCertificateCheck)
            });
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.PAN_URL == null ? "PAN URL Not Uploaded" : "PAN URL",
                Flag = Docs.PAN_URL == null ? 3 : Docs.IsPanCheck == null ? 0 : Convert.ToInt32(Docs.IsPanCheck)
            });
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.POS_Certificate_Front_URL == null ? "POS Certificate Not Uploaded" : "POS Certificate",
                Flag = Docs.POS_Certificate_Front_URL == null ? 3 : Docs.IsPosCertificateCheck == null ? 0 : Convert.ToInt32(Docs.IsPosCertificateCheck)
            });
            lstDoc.Add(new UserDocWithStatus()
            {
                DocName = Docs.QualificationCertificate_URL == null ? "Qualification Certificate Not Uploaded" : "Qualification Certificate",
                Flag = Docs.QualificationCertificate_URL == null ? 3 : Docs.IsQualificationCheck == null ? 0 : Convert.ToInt32(Docs.IsQualificationCheck)
            });
            return lstDoc;
        }
        public PosExamStartBase CheckHourTestProcess(CommonParam Item)
        {
            PosExamStartBase basemodel = new PosExamStartBase();
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<PosExamStart> data = new DataLayer<PosExamStart>();
            var dataModel = data.GetSingelDetailWithCondition(row => row.ClientID == um.ClientID
            && row.UserID == um.UserID);
            if (dataModel != null)
            {
                basemodel.Automatically = dataModel.Automatically;
                basemodel.Start = dataModel.Start;
                basemodel.hours = (DateTime.Now - dataModel.Timing.Value).TotalHours;
                basemodel.TrainingComplete = dataModel.TrainingComplete == null ? false : dataModel.TrainingComplete.Value;
                basemodel.Result = dataModel.Result == null ? false : dataModel.Result.Value;
                basemodel.PassOrFail = dataModel.PassOrFail;
            }
            return basemodel;
        }
        public string ForFurtherProcess(ExamProcessParam Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<PosExamStart> Data = new DataLayer<PosExamStart>();
            var Exist = Data.GetSingelDetailWithCondition(row => row.UserID == um.UserID
            && row.ClientID == um.ClientID);
            if (Exist == null)
            {
                Data = null;
                Data = new DataLayer<PosExamStart>();
                PosExamStart pos = new PosExamStart()
                {
                    ID = 0,
                    ClientID = um.ClientID.Value,
                    UserID = um.UserID,
                    Automatically = Item.Start == true ? false : true,
                    Start = Item.Start,
                    Timing = DateTime.Now
                };
                Response = Data.InsertRecord(pos);
            }
            return Response;
        }
        public bool PosTrainingDone(CompleteTraining Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<PosExamStart> Data = new DataLayer<PosExamStart>();
            var Exist = Data.GetSingelDetailWithCondition(row => row.UserID == um.UserID
            && row.ClientID == um.ClientID);
            if (Exist == null)
            {
                return false;
            }
            else
            {
                var Traininghour = (DateTime.Now - Exist.Timing.Value).TotalHours;
                if (Traininghour >= 15)
                {
                    Data = null;
                    Data = new DataLayer<PosExamStart>();
                    Exist.TrainingComplete = Item.IsTrainingComplete;
                    Exist.TrainingCompleteDate = DateTime.Now;
                    Data.Update(Exist);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string PosExamDone(CompleteExam item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(item.Token);
            IDataLayer<PosExamStart> Data = new DataLayer<PosExamStart>();
            var Exist = Data.GetSingelDetailWithCondition(row => row.UserID == um.UserID
                                                          && row.ClientID == um.ClientID);
            int total = item.FinalResult.Length;
            IDataLayer<PosQuestions> Qstndata = new DataLayer<PosQuestions>();
            var qstns = Qstndata.GetDetailsWithCondition(row => row.ClientID == um.ClientID.Value);
            int couter = 0;
            for (int i = 0; i < item.FinalResult.Length; i++)
            {
                if (item.FinalResult[i].Answer == qstns.FirstOrDefault(row => row.ID == item.FinalResult[i].ID).Answer)
                {
                    couter += 1;
                }
            }
            string Result = "";
            if (couter >= (total * 50 / 100))
                Result = "Pass";
            else
                Result = "Fail";

            if (Exist != null)
            {
                Exist.Result = item.IsExamComplete;
                Exist.PassOrFail = Result;
                Exist.ExamCompleteDate = DateTime.Now;
                Data = null;
                Data = new DataLayer<PosExamStart>();
                Response = Data.Update(Exist);
            }
            try
            {
                Thread th = new Thread(() => WelcomeActivePosMail(Exist.UserID, um.ClientID.Value));
                th.Start();
            }
            catch (Exception ex)
            { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\ErrorWelcomeActivePosMail.txt", ex.Message); }
            return Result;
        }
        private string WelcomeActivePosMail(int userid, int ClientID)
        {
            string Message = "";
            IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
            var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster == "WelcomeActivePos");
            if (mailoptiondata != null)
            {
                IDataLayer<MailServer> MailServerList = new DataLayer<MailServer>();
                var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.MailserveroptionID == mailoptiondata.ID);
                if (mailServer != null)
                {
                    IDataLayer<NotificationEmail> NotificationEmailList = new DataLayer<NotificationEmail>();
                    var notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "WelcomeActivePos"
                                            && row.ClientID == ClientID);
                    if (notificationEmail != null)
                    {
                        IDataLayer<UserMaster> Umas = new DataLayer<UserMaster>();
                        var UserInfo = Umas.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.UserID == userid);
                        IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
                        var client = ClientData.GetSingelDetailWithCondition(row => row.Id == ClientID);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[URL]", client.companyURL);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[CompanyName]", client.companyName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[IMG]", client.CompanyLogo);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[Name]", UserInfo.UserName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[URLName]", client.companyName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[Email]", client.contactNo);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[UserID]", UserInfo.EmailAddress);
                        Common.mailMaster(mailServer.FromEmail, UserInfo.EmailAddress, notificationEmail.MailSubject, notificationEmail.MailBody,
                            mailServer.UserName, mailServer.Password, mailServer.HostName, mailServer.Port,
                            mailServer.UseDefaultCredential, mailServer.EnableSsl, ref Message);
                        return Message;
                    }
                }
            }
            return null;
        }
        public IEnumerable<sp_ClaimDetails> Claim(RegisterPosParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            if (!CheckDashBoardPrivilege(um.UserID, "Total Claim Details"))
                return null;

            if (um.RoleID == 1 || um.RoleID == 28)
            {
                IDataLayer<sp_ClaimDetails> claim = new DataLayer<sp_ClaimDetails>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_ClientID", um.ClientID.ToString());
                param.Add("@p_fromdate", Item.FromDate.ToString("yyyy-MM-dd"));
                param.Add("@p_todate", Item.ToDate.ToString("yyyy-MM-dd"));
                var dataList = claim.ProceduresGetData("sp_ClaimDetails", param);
                return dataList;
            }
            else
                return null;
        }
        public IEnumerable<sp_SharedDetails> Sharedatadetail(RegisterPosParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            if (!CheckDashBoardPrivilege(um.UserID, "Total Shared Details"))
                return null;
            if (um.RoleID == 1 || um.RoleID == 28)
            {
                IDataLayer<sp_SharedDetails> claim = new DataLayer<sp_SharedDetails>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_ClientID", um.ClientID.ToString());
                param.Add("@p_fromdate", Item.FromDate.ToString("yyyy-MM-dd"));
                param.Add("@p_todate", Item.ToDate.ToString("yyyy-MM-dd"));
                var dataList = claim.ProceduresGetData("sp_SharedDetails", param);
                return dataList;
            }
            else
                return null;
        }
        public string GetClientLogo(CommonParam Item)
        {
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
            var Logo = data.GetSingelDetailWithCondition(row => row.Id == Um.ClientID);
            return Logo.CompanyLogo;
        }
        public IEnumerable<sp_Logintimehistory> GetLoginHistory(LoginTimeHis item)
        {
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            if (!CheckDashBoardPrivilege(model.UserID, "Total Login History"))
                return null;
            IDataLayer<sp_Logintimehistory> Data = new DataLayer<sp_Logintimehistory>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", model.UserID.ToString());
            param.Add("@p_ClientID", model.ClientID.Value.ToString());
            param.Add("@p_StartDate", item.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@p_EndDate", item.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_Logintimehistory", param);
            return dataList;
        }
        public string DownloadLoginHistory(LoginTimeHis item)
        {
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            if (!CheckDashBoardPrivilege(model.UserID, "Total Login History"))
                return null;

            string Query = "call sp_Logintimehistory(" + model.UserID + "," + model.ClientID.Value + ",'" + item.FromDate.ToString("yyyy-MM-dd") + "','" + item.ToDate.ToString("yyyy-MM-dd") + "')";
            return FileReturnUrl(Query, model.UserID, model.ClientID.Value, "LoginHis");
        }
        public string LeadDownload(BusinessReportReq param)
        {
            try
            {
                UserModel model = Common.DecodeToken(param.Token);
                FromDateToDate paramModal = new FromDateToDate()
                {
                    ClientID = model.ClientID.Value,
                    FromDate = param.FromDate,
                    ToDate = param.ToDate,
                    Userid = model.UserID
                };
                string Query = "call sp_DownloadLeadDetailGrid(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                //try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\ReportDownloadQuery.txt", Query); } catch { }
                var data = DbHelper.ExecuteSQLQuery(Query);
                //try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\ReportDownloadAfterQuery.txt", JsonConvert.SerializeObject(data)); } catch { }
                string[] Header = data.Columns.Cast<DataColumn>().
                        Select(row => row.ColumnName).ToArray();
                StringBuilder Table = new StringBuilder();
                foreach (string head in Header)
                {
                    Table.Append(head + ",");
                }
                Table.Append("\n");
                foreach (DataRow dr in data.Rows)
                {
                    foreach (string head in Header)
                    {
                        Table.Append(dr[head] + ",");
                    }
                    Table.Append("\n");
                }
                string filePath = System.IO.Directory.GetCurrentDirectory() + @"\Downloads\" + model.UserID + "_LeadData.csv";
                File.WriteAllText(filePath, Table.ToString());
                IDataLayer<ClientMaster> Data = new DataLayer<ClientMaster>();
                var ClientUrl = Data.GetSingelDetailWithCondition(row => row.Id == model.ClientID);
                return ClientUrl.CoreAPIURL + "/Downloads/" + model.UserID + "_LeadData.csv";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public GotoProposalRespose CheckForGotoProposal(CheckForGotoProposalParam Item)
        {
            GotoProposalRespose Response = new GotoProposalRespose();
            Response.EncryptENQ = Common.Encrypt(Item.EnquiryNo);
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Motorenquiry> Me = new DataLayer<Motorenquiry>();
            var MeContain = Me.GetSingelDetailWithCondition(row => row.EnquiryNo == Item.EnquiryNo);
            Response.PolicyType = MeContain.PolicyType;
            if (MeContain != null)
            {
                IDataLayer<Motorpolicydetails> mpd = new DataLayer<Motorpolicydetails>();
                var mpdContain = mpd.GetSingelDetailWithCondition(row => row.MotorID == MeContain.MotorID);
                if (mpdContain != null)
                {
                    if (mpdContain.PolicyType == null)
                    {
                        mpdContain.PolicyType = MeContain.PolicyType;
                        goto Resp;
                    }
                    if (mpdContain.PolicyType.ToLower() == "expired")
                    {
                        Response.Status = "Success";
                        Response.PolicyType = "Ex";
                        goto Resp;
                    }
                    if (mpdContain.PolicyType.ToLower() == "new" && (DateTime.Now - mpdContain.Entrydate.Value).TotalDays == 0)
                    {
                        Response.Status = "Success";
                        Response.PolicyType = "N";
                        goto Resp;
                    }
                    else if (mpdContain.PolicyType.ToLower() == "new" && (DateTime.Now - mpdContain.Entrydate.Value).TotalDays >= 1)
                    {
                        Response.Status = "In new case need to go from start.";
                        Response.PolicyType = "N";
                        goto Resp;
                    }
                    if (mpdContain.PolicyType.ToLower() == "renew")
                    {
                        Response.Status = "Success";
                        Response.PolicyType = "R";
                        /*
                         * Comment by Sumit
                        IDataLayer<Motorpolicydetailsothersdata> otherData = new DataLayer<Motorpolicydetailsothersdata>();
                        var otherContain = otherData.GetSingelDetailWithCondition(row => row.MotorpolicyID == mpdContain.MotorPolicyID);
                        if (otherContain != null)
                        {
                            if (otherContain.PolicyStartDate.AddDays(-1) < DateTime.Now)
                            {
                                Response.Status = "Success";
                                Response.PolicyType = "R";
                            }
                            else
                            {
                                Response.Status = "In this renew case need to go from start.";
                                Response.PolicyType = "R";
                            }
                        }
                        else
                        {
                            Response.Status = "Data not found in OtherData.";
                            Response.PolicyType = "R";
                        }
                        */
                    }
                }
                else
                {
                    Response.Status = "Policy is not found.";
                }
            }
            else
            {
                Response.Status = "EnquiryNo is not found.";
            }
        Resp:
            return Response;
        }
        public IEnumerable<vw_Uploadpolicy> GetOfflineFeatureLead(GetOfflineFeatureParam Item)
        {
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<vw_Uploadpolicy> upload = new DataLayer<vw_Uploadpolicy>();
            var Data = upload.GetDetailsWithCondition(row => row.ClientID == Um.ClientID.Value && (row.CreatedDate.Date >= Item.From.Date || row.CreatedDate.Date >= Item.To));
            return Data;
        }
        public string SaveOfflineMotorQuote(SaveOfflineMotorQuoteParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Offlinerequestmotor> offline = new DataLayer<Offlinerequestmotor>();
            IDataLayer<Motorenquiry> Motoren = new DataLayer<Motorenquiry>();
            var ContainMe = Motoren.GetSingelDetailWithCondition(row => row.EnquiryNo == Item.Enquiryno);
            if (ContainMe != null)
            {
                var Contain = offline.GetSingelDetailWithCondition(row => row.Enquiryno == Item.Enquiryno && row.ClientID == Umodel.ClientID.Value);
                if (Contain == null)
                {
                    Offlinerequestmotor obj = new Offlinerequestmotor()
                    {
                        ClientID = Umodel.ClientID.Value,
                        Enquiryno = Item.Enquiryno,
                        ID = 0,
                        InsurerFiles = Item.InsurerFilePaths,
                        Remarks = Item.Remarks
                    };
                    offline = null;
                    offline = new DataLayer<Offlinerequestmotor>();
                    Response = offline.InsertRecord(obj);
                    Motoren = null;
                    Motoren = new DataLayer<Motorenquiry>();
                    ContainMe.ManufactureID = Item.ManufacturerID;
                    ContainMe.MotorType = Item.MotorType;
                    ContainMe.PolicyType = Item.PolicyType;
                    ContainMe.RegistartionYear = Item.RegYear;
                    ContainMe.RTOID = Item.RTOID;
                    ContainMe.Status = 1;
                    ContainMe.VariantID = Item.VariantID;
                    ContainMe.VehicleID = Item.VehicleID;
                    Motoren.Update(ContainMe);
                }
                else
                    Response = "Already Exist.";
            }
            else
                Response = "Not Found.";
            return Response;
        }
        public string SaveOfflineHealthQuote(SaveOfflineHealthQuoteParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Offlinerequestmotor> offline = new DataLayer<Offlinerequestmotor>();
            IDataLayer<Healthenquiry> Healthen = new DataLayer<Healthenquiry>();
            var ContainMe = Healthen.GetSingelDetailWithCondition(row => row.EnquiryNo == Item.Enquiryno);
            if (ContainMe != null)
            {
                var Contain = offline.GetSingelDetailWithCondition(row => row.Enquiryno == Item.Enquiryno && row.ClientID == Umodel.ClientID.Value);
                if (Contain == null)
                {
                    Offlinerequestmotor obj = new Offlinerequestmotor()
                    {
                        ClientID = Umodel.ClientID.Value,
                        Enquiryno = Item.Enquiryno,
                        ID = 0,
                        InsurerFiles = Item.InsurerFilePaths
                    };
                    offline = null;
                    offline = new DataLayer<Offlinerequestmotor>();
                    Response = offline.InsertRecord(obj);
                    Healthen = null;
                    int AdultCount = 0;
                    AdultCount = Item.FatherAge > 0 ? 1 : 0;
                    AdultCount += Item.MotherAge > 0 ? 1 : 0;
                    AdultCount += Item.SpouseAge > 0 ? 1 : 0;
                    AdultCount += Item.UserAge > 0 ? 1 : 0;
                    var sonList = Item.Childs.Where(row => row.ChildGen == "Son").ToList().Count;
                    var DoughterList = Item.Childs.Where(row => row.ChildGen == "Doughter").ToList().Count;


                    Healthen = new DataLayer<Healthenquiry>();
                    ContainMe.AdultCount = AdultCount;
                    ContainMe.UserAge = Item.UserAge;
                    ContainMe.Gender = Item.Gender;
                    ContainMe.ChildCount = Item.Childs.Count;
                    ContainMe.PolicyType = (AdultCount + sonList + DoughterList) > 1 ? "FamilyFlooter" : "Individual";
                    ContainMe.ClientID = Umodel.ClientID.Value;
                    ContainMe.CreatedDate = DateTime.Now;
                    ContainMe.ChildCount = sonList + DoughterList;
                    ContainMe.Status = true;
                    ContainMe.DoughterAge1 = DoughterList > 0 ? Item.Childs.Where(row => row.ChildGen == "Doughter").ToList()[0].Age : 0;
                    ContainMe.DoughterAge2 = DoughterList > 1 ? Item.Childs.Where(row => row.ChildGen == "Doughter").ToList()[1].Age : 0;
                    ContainMe.DoughterAge3 = DoughterList > 2 ? Item.Childs.Where(row => row.ChildGen == "Doughter").ToList()[2].Age : 0;
                    ContainMe.DoughterAge4 = DoughterList > 3 ? Item.Childs.Where(row => row.ChildGen == "Doughter").ToList()[3].Age : 0;
                    ContainMe.SonAge1 = sonList > 0 ? Item.Childs.Where(row => row.ChildGen == "Son").ToList()[0].Age : 0;
                    ContainMe.SonAge2 = sonList > 1 ? Item.Childs.Where(row => row.ChildGen == "Son").ToList()[1].Age : 0;
                    ContainMe.SonAge3 = sonList > 2 ? Item.Childs.Where(row => row.ChildGen == "Son").ToList()[2].Age : 0;
                    ContainMe.SonAge4 = sonList > 3 ? Item.Childs.Where(row => row.ChildGen == "Son").ToList()[3].Age : 0;
                    Healthen.Update(ContainMe);
                }
                else
                    Response = "Already Exist.";
            }
            else
                Response = "Not Found.";
            return Response;
        }
        public IEnumerable<sp_OfflineLead> DashbordOfflineleadGrid(CommonParam Item)
        {

            UserModel Umodel = Common.DecodeToken(Item.Token);
            if (!CheckDashBoardPrivilege(Umodel.UserID, "Dashboard Offline Lead"))
                return null;
            IDataLayer<sp_OfflineLead> Data = new DataLayer<sp_OfflineLead>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", Umodel.UserID.ToString());
            param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
            var data = Data.ProceduresGetData("sp_OfflineLead", param);
            return data;
        }
        public string OfflineGotoPayment(OfflineGotoPayment Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EnquiryID", Item.EnquiryID.ToString());
            param.Add("@p_InsurerCompanyID", Item.CompanyID.ToString());
            var data = Data.ProcedureOutput("sp_OfflineGotoPaymentGetway", param);
            if (data == "1")
                Response = "Request Sent Successfully.";
            else
                Response = data;
            return Response;
        }
        public IEnumerable<PosQuestions> PosExamQuestions(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            List<PosQuestions> QstnList = new List<PosQuestions>();
            IDataLayer<PosQuestions> dataQ = new DataLayer<PosQuestions>();
            var GetData = dataQ.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID.Value).ToList();
            Random rnd = new Random();
            int maxlen = GetData.Count();
            if (maxlen > 25)
                maxlen = 25;
            for (int i = 0; i < maxlen; i++)
            {
                int index = rnd.Next(GetData.Count());
                QstnList.Add(GetData[index]);
                GetData.RemoveAt(index);
                ReleaseObject<Random>(ref rnd);
            }
            return QstnList;
        }
        public string QuoteRelatedMessage(OfflineQueryMessage Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<Enquiry> enquiry = new DataLayer<Enquiry>();
            var enquiryData = enquiry.GetSingelDetailWithCondition(row => row.EnquiryNo == Item.EnquiryNo);
            if (enquiryData.UserID != null)
            {
                Response = SendMessage(um.UserID, Item.Message, Item.Subject, enquiryData.UserID.Value);
            }
            else
                Response = "Not Found";
            return Response;
        }
        private string SendMessage(int FromUserID, string Message, string Subject, int ToUser)
        {
            string Response = "";
            if (ToUser > 1)
            {
                IDataLayer<OfflineQueryRelatedMessage> offlineQueryRelatedMessage = new DataLayer<OfflineQueryRelatedMessage>();
                OfflineQueryRelatedMessage obj = new OfflineQueryRelatedMessage()
                {
                    FromUser = FromUserID,
                    ID = 0,
                    Message = Message,
                    Subject = Subject,
                    MessageDate = DateTime.Now,
                    ReadOrNot = false,
                    ToUser = ToUser
                };
                Response = offlineQueryRelatedMessage.InsertRecord(obj);
            }
            else
                Response = "To User is required.";
            return Response;
        }
        private string DocumentRelatedMessage(int ClientID, int UserID)
        {
            string Message = "";
            try
            {
                IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
                var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster.Contains("DocumentMessage"));
                if (mailoptiondata != null)
                {
                    IDataLayer<MailServer> MailServerList = new DataLayer<MailServer>();
                    var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.MailserveroptionID == mailoptiondata.ID);
                    IDataLayer<NotificationEmail> NotificationEmailList = new DataLayer<NotificationEmail>();
                    var notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "DocumentMessage"
                                            && row.ClientID == ClientID);
                    if (notificationEmail != null)
                    {
                        IDataLayer<UserMaster> userMaster = new DataLayer<UserMaster>();
                        var Data = userMaster.GetSingelDetailWithCondition(row => row.UserID == UserID && row.ClientID == ClientID);
                        IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
                        var client = ClientData.GetSingelDetailWithCondition(row => row.Id == ClientID);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[URL]", client.companyURL);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[CompanyName]", client.companyName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[IMG]", client.CompanyLogo);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[Name]", Data.UserName);
                        Common.mailMaster(mailServer.FromEmail, Data.EmailAddress, notificationEmail.MailSubject, notificationEmail.MailBody,
                            mailServer.UserName, mailServer.Password, mailServer.HostName, mailServer.Port,
                            mailServer.UseDefaultCredential, mailServer.EnableSsl, ref Message);
                    }
                    else
                    {
                        Message = "NotificationEmail not configured";
                    }
                }
                else
                    Message = "MailServerOption not configured";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Message;
        }
        public int QuoteRelatedMessageCounter(CommonParam Item)
        {
            int i = 0;
            UserModel um = Common.DecodeToken(Item.Token);
            LoginAttempFun(um.UserID);
            IDataLayer<OfflineQueryRelatedMessage> dataCounter = new DataLayer<OfflineQueryRelatedMessage>();
            i = dataCounter.GetDetailsWithCondition(row => row.ToUser == um.UserID && row.ReadOrNot == false).Count();
            return i;
        }
        public IEnumerable<OfflineQueryRelatedMessage> QuoteRelatedMessageList(CommonParam Item)
        {
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<OfflineQueryRelatedMessage> offlinemessage = new DataLayer<OfflineQueryRelatedMessage>();
            var offlineMessageData = offlinemessage.GetDetailsWithCondition(row => row.ToUser == Um.UserID).OrderBy(row => row.MessageDate);
            return offlineMessageData;
        }
        public string ReadedMessage(OfflineQueryUpdateMessage Item)
        {
            string response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<OfflineQueryRelatedMessage> offline = new DataLayer<OfflineQueryRelatedMessage>();
            var messagedata = offline.GetSingelDetailWithCondition(row => row.ID == Item.MessageID);
            offline = null;
            offline = new DataLayer<OfflineQueryRelatedMessage>();
            messagedata.ReadOrNot = true;
            response = offline.Update(messagedata);
            return response;
        }
        public IEnumerable<sp_privilegelist> DashoardModulePrivilege(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_privilegelist> Data = new DataLayer<sp_privilegelist>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", Umodel.UserID.ToString());
            param.Add("@p_clientid", Umodel.ClientID.Value.ToString());
            var data = Data.ProceduresGetData("sp_DashoardModulePrivilege", param);
            return data;
        }
        public string GetPosRqstDownload(BusinessReportReq Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            string Query = "call sp_PosRequestData(" + umodel.ClientID.Value.ToString() + "," +
                "" + umodel.UserID.ToString() + ",'" + Item.FromDate.ToString("yyyy-MM-dd") + "','" + Item.ToDate.ToString("yyyy-MM-dd") + "')";
            Response = FileReturnUrl(GetTableData(Query), umodel.UserID, umodel.ClientID.Value, "PosRequest");
            return Response;
        }
        public string AddFadBack(AddFadBackParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<FeedbackData> feedbackData = new DataLayer<FeedbackData>();
            var Exist = feedbackData.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value
                                    && row.FeedbackText == Item.FeedbackText && row.UserID == umodel.UserID);
            if (Exist == null)
            {
                feedbackData = new DataLayer<FeedbackData>();
                Response = feedbackData.InsertRecord(new FeedbackData()
                {
                    ClientID = umodel.ClientID.Value,
                    FeedbackOptionID = Item.SelectedFeedbackID,
                    FeedbackText = Item.FeedbackText,
                    ID = 0,
                    Rating = Item.Rating,
                    FeedbackDate = DateTime.Now,
                    UserID = umodel.UserID
                });
            }
            else
                Response = "Duplicate Feedback.";
            return Response;
        }
        public IEnumerable<sp_GetUserBirthdayList> BirthdayUserList(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_GetUserBirthdayList> Data = new DataLayer<sp_GetUserBirthdayList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", Umodel.UserID.ToString());
            param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
            var data = Data.ProceduresGetData("sp_GetUserBirthdayList", param);
            return data;
        }
        public string SendBirthdayMessages(BithdayWisheshParam item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<OfflineQueryRelatedMessage> offline = new DataLayer<OfflineQueryRelatedMessage>();
            List<OfflineQueryRelatedMessage> messageList = new List<OfflineQueryRelatedMessage>();
            if (item.EmpIDCollection.Contains(","))
            {
                foreach (string userID in item.EmpIDCollection.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    messageList.Add(new OfflineQueryRelatedMessage()
                    {
                        FromUser = Umodel.UserID,
                        ToUser = Convert.ToInt32(userID),
                        ID = 0,
                        Message = item.Message,
                        MessageDate = DateTime.Now,
                        ReadOrNot = false,
                        Subject = "Birthday Message From(" + Umodel.Email + ")"
                    });
                }
            }
            else
            {
                messageList.Add(new OfflineQueryRelatedMessage()
                {
                    FromUser = Umodel.UserID,
                    ToUser = Convert.ToInt32(item.EmpIDCollection),
                    ID = 0,
                    Message = item.Message,
                    MessageDate = DateTime.Now,
                    ReadOrNot = false,
                    Subject = "Birthday Message From(" + Umodel.UserName + ")"
                });
            }
            Response = offline.InsertRecordList(messageList);
            return Response;
        }
        #endregion Home Controller
        #region User Controller
        public IEnumerable<RoleType> RoleTypes()
        {
            IDataLayer<RoleType> Data = new DataLayer<RoleType>();
            return Data.GetDetailsWithCondition(row => row.RoleID == 8 || row.RoleID == 26 || row.RoleID == 27);
        }
        public IEnumerable<sp_Rolelist> RoleTypeWithID(RoleTypeParam role)
        {
            var model = CommonMethods.Common.DecodeToken(role.Token);
            role.ID = model.RoleID.Value;
            int ClientID = model.ClientID.Value;
            IDataLayer<sp_Rolelist> Data = new DataLayer<sp_Rolelist>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_RoleID", role.ID.ToString());
            param.Add("@p_UserID", model.UserID.ToString());
            param.Add("@p_ClientID", ClientID.ToString());
            var data = Data.ProceduresGetData("sp_Rolelist", param);
            return data;
        }
        public IEnumerable<sp_RoleMastWithClientID> RoleTeamList(RoleTypeParam role)
        {
            UserModel Um = Common.DecodeToken(role.Token);
            IDataLayer<sp_RoleMastWithClientID> Data = new DataLayer<sp_RoleMastWithClientID>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@RoleID", role.ID.ToString());
            param.Add("@p_UserID", Um.UserID.ToString());
            param.Add("@p_ClientID", Um.ClientID.ToString());
            var data = Data.ProceduresGetData("sp_RoleMastWithClientID", param);
            return data;
        }
        public IEnumerable<sp_TeamUserList> TeamUserList(TeamUserListParam Team)
        {
            List<sp_TeamUserList> lst = new List<sp_TeamUserList>();
            IDataLayer<sp_TeamUserList> Data = new DataLayer<sp_TeamUserList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_paruserid", Team.UserID.ToString());
            param.Add("@p_clientid", Team.ClientID.ToString());
            param.Add("@p_roleid", Team.RoleID.ToString());
            var dataList = Data.ProceduresGetData("sp_TeamUserList", param);
            //var dataList = Data.ProceduresGetData("new_procedure", param);
            return dataList;
        }
        public IEnumerable<sp_GetCitiesWithRegionid> GetCities(RoleTypeParam item)
        {
            UserModel Um = Common.DecodeToken(item.Token);
            IDataLayer<sp_GetCitiesWithRegionid> cities = new DataLayer<sp_GetCitiesWithRegionid>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Clientid", Um.ClientID.Value.ToString());
            param.Add("@p_RegionID", item.ID.ToString());
            var Response = cities.ProceduresGetData("sp_GetCitiesWithRegionid", param);
            return Response;
        }
        public IEnumerable<sp_RegionZoneWithClientid> GetRegions(CommonParam item)
        {
            UserModel um = Common.DecodeToken(item.Token);
            IDataLayer<sp_RegionZoneWithClientid> RegionZones = new DataLayer<sp_RegionZoneWithClientid>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Clientid", um.ClientID.ToString());
            var dataList = RegionZones.ProceduresGetData("sp_RegionZoneWithClientid", param);
            return dataList;
        }
        public IEnumerable<UserRegionBranch> UserAllocation(BaseParam param)
        {
            IDataLayer<UserRegionBranch> UM = new DataLayer<UserRegionBranch>();
            var data = UM.GetDetailsWithCondition(row => row.UserID == Convert.ToInt32(param.UserID));
            //UserAllocation ual = new UserAllocation()
            //{
            //    bhCityId = data.bhCityId,
            //    RHead = data.RHead
            //};
            //return ual;
            return data;
        }
        public string UserMasterSave(UserCreationParam Item)
        {
            string GeneratePass = "";
            string Response = "";
            UserModel model = CommonMethods.Common.DecodeToken(Item.Token);
            string Pass = Guid.NewGuid().ToString().ToUpper().Substring(0, 4);
            GeneratePass = Item.EmailAddress.Substring(0, 3) + "@" + Pass;
            Pass = Common.Encrypt(GeneratePass);
            IDataLayer<UserMaster> data = new DataLayer<UserMaster>();
            var ReportingData = data.GetSingelDetailWithCondition(row => row.ClientID == model.ClientID.Value &&
                                                                  row.UserID == Item.KeyAccountManager);
            string RmCode = "";
            if (ReportingData != null)
                RmCode = ReportingData.RmCode;
            UserMaster merge = new UserMaster()
            {
                Active = Item.Active,
                CreatedDate = DateTime.Now,
                EmailAddress = Item.EmailAddress,
                KeyAccountManager = Item.KeyAccountManager,
                MobileNo = Item.MobileNo,
                ClientID = model.ClientID,//Item.ClientID,
                UserID = 0,
                UserName = Item.UserName,
                RoleId = Item.RoleId,
                CreatedBy = model.UserID,//Item.CreateBy.Value,
                Password = Pass,
                ReferVal = Item.ReferVal,
                ReferPrifix = Item.ReferPrifix,
                PosVal = Item.PosVal,
                PosPrifix = Item.PosPrifix,
                Alternatecode = Item.Alternatecode,
                RmCode = RmCode,
                IsDocRequred = Item.IsDocRequred,
                PANNumber = Item.PanNum,
                AdhaarNumber = Item.AdharNum,
                DOB = Item.DOB == null ? DateTime.Now.AddYears(-18) : Item.DOB
            };
            data = null;
            data = new DataLayer<UserMaster>();
            Response = data.InsertRecord(merge);
            if (Response == "Data Save Successfully.")
            {
                IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
                var user = um.GetSingelDetailWithCondition(row => row.EmailAddress == Item.EmailAddress && row.ClientID == model.ClientID);
                IDataLayer<UserRegionBranch> UR = null;
                foreach (var rb in Item.Regions)
                {
                    UserRegionBranch regionsBranch = new UserRegionBranch()
                    {
                        BranchID = rb.BranchID,
                        RegionID = rb.RegionID,
                        UserID = user.UserID
                    };
                    UR = new DataLayer<UserRegionBranch>();
                    UR.InsertRecord(regionsBranch);
                    regionsBranch = null;
                    UR = null;
                }
                AddPrivilegeOnCreationTime(user.UserID, user.RoleId, model.UserID);
                try { SendEmailNotification(Item.UserName, model.ClientID.Value, Item.EmailAddress, GeneratePass, user.RoleId); } catch { }
                try { SendNotification(Item.MobileNo, model.ClientID.Value, Item.UserName, Item.EmailAddress, GeneratePass); } catch { }
                /*
                if (model.ClientID == 59)
                {
                    SendNotification(Item.MobileNo, "Hi, " + Item.UserName + " You have registerd successfully Our team will contact you shortly");
                    //Common.mailMaster("",Item.EmailAddress,"Subject","Body","rblinsureadmin",)
                }
                */
            }
            return Response;
        }
        private string AddPrivilegeOnCreationTime(int UserID, int RoleID, int AssignBy)
        {
            string Response = "";
            IDataLayer<userprivilegerolemapping> PrivilegeData =
                    new DataLayer<userprivilegerolemapping>();
            userprivilegerolemapping objPriv = new userprivilegerolemapping()
            {
                Addrecord = 1,
                AssignBy = AssignBy,
                AssignDate = DateTime.Now,
                deleterecord = 1,
                Editrecord = 1,
                ID = 0,
                NavBarMasterMenuID = 4,
                PrivilegeID = 35,
                UserID = UserID
            };
            PrivilegeData.InsertRecord(objPriv);

            if (RoleID == 8)
            {
                int[] PrivIDs = { 100, 101, 102, 103, 117 };
                foreach (int privID in PrivIDs)
                {
                    PrivilegeData = null;
                    objPriv = null;
                    PrivilegeData = new DataLayer<userprivilegerolemapping>();
                    objPriv = new userprivilegerolemapping()
                    {
                        Addrecord = 1,
                        AssignBy = AssignBy,
                        AssignDate = DateTime.Now,
                        deleterecord = 1,
                        Editrecord = 1,
                        ID = 0,
                        NavBarMasterMenuID = 3,
                        PrivilegeID = privID,
                        UserID = UserID
                    };
                    PrivilegeData.InsertRecord(objPriv);
                }
            }
            return Response;
        }
        public void SendEmailNotification(string UserName, int ClientID, string UserEmail, string Pass, int roleID)
        {
            IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
            var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster.Contains("Create"));
            if (mailoptiondata != null)
            {
                IDataLayer<sp_MailserverData> MailserverData = new DataLayer<sp_MailserverData>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_ClientID", ClientID.ToString());
                param.Add("@p_RoleID", roleID.ToString());
                var Response = MailserverData.ProceduresGetData("sp_MailServerActionTime", param);
                /*
                IDataLayer<MailServer> MailServerList = new DataLayer<MailServer>();
                var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.MailserveroptionID == mailoptiondata.ID);
                */
                if (Response != null)
                {
                    var mailServer = Response.FirstOrDefault();
                    string Message = "";
                    IDataLayer<NotificationEmail> NotificationEmailList = new DataLayer<NotificationEmail>();
                    var notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "CreateUser"
                                            && row.ClientID == ClientID && row.RoleID == roleID);
                    if (notificationEmail == null)
                    {
                        NotificationEmailList = null;
                        NotificationEmailList = new DataLayer<NotificationEmail>();
                        notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "CreateUser"
                                            && row.ClientID == ClientID && row.RoleID == 0);
                    }
                    if (notificationEmail != null)
                    {
                        IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
                        var client = ClientData.GetSingelDetailWithCondition(row => row.Id == ClientID);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[URL]", client.companyURL);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[CompanyName]", client.companyName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[IMG]", client.CompanyLogo);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[Name]", UserName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[URLName]", client.companyName);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[Email]", client.contactNo);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[UserID]", UserEmail);
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[Password]", Pass);
                        Common.mailMaster(mailServer.FromEmail, UserEmail, notificationEmail.MailSubject, notificationEmail.MailBody,
                            mailServer.UserName, mailServer.Password, mailServer.HostName, mailServer.Port,
                            mailServer.UseDefaultCredential, mailServer.EnableSsl, ref Message);
                    }
                }
            }
        }
        public string SendNotification(string mobileno, int ClientID, string UserName, string UserEmail, string Pass)
        {
            string Response = "";
            IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
            var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster.Contains("Create"));
            if (mailoptiondata != null)
            {
                IDataLayer<SmsServer> MailServerList = new DataLayer<SmsServer>();
                NotificationSMS notificationEmail = null;
                var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == ClientID && row.MailServerOptionID == mailoptiondata.ID);
                if (mailServer != null)
                {
                    IDataLayer<NotificationSMS> NotificationEmailList = new DataLayer<NotificationSMS>();
                    notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "CreateUser" && row.ClientID == ClientID);
                    if (notificationEmail != null)
                    {
                        IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
                        var client = ClientData.GetSingelDetailWithCondition(row => row.Id == ClientID);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[URL]", client.companyURL);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[CompanyName]", client.companyName);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[IMG]", client.CompanyLogo);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[Name]", UserName);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[URLName]", client.companyName);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[Email]", client.contactNo);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[UserID]", UserEmail);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[Password]", Pass);
                    }
                }
                mailServer.SMSAPI = mailServer.SMSAPI.Replace("[to]", mobileno);
                mailServer.SMSAPI = mailServer.SMSAPI.Replace("[message]", notificationEmail.MsgBody);
                var url = mailServer.SMSAPI;
                Common.SendSms(url, out Response);
            }
            return "successfully sent";
        }

        public IEnumerable<sp_CreateByList> UserListWithUserID(FilterUserByDate item)
        {
            IDataLayer<sp_CreateByList> Data = new DataLayer<sp_CreateByList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", item.UserID.ToString());
            param.Add("@p_clientid", item.ClientID.ToString());
            param.Add("@p_startdate", item.startDate.ToString("yyyy-MM-dd"));
            param.Add("@p_enddate", item.endDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_CreateByList", param);

            return dataList;
        }
        public sp_UserInfo UserInfo(User item)
        {
            UserModel umodel = Common.DecodeToken(item.Token);
            sp_UserInfo ui = new sp_UserInfo();
            IDataLayer<sp_UserInfo> um = new DataLayer<sp_UserInfo>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userID", item.Userid.ToString());
            param.Add("@p_whocheckinfo", umodel.UserID.ToString());
            var info = um.ProceduresGetData("sp_UserInfo", param);
            ui = info.ToList()[0];
            ui.Password = Common.Decrypt(ui.Password);
            return ui;
        }
        [HttpPost]
        public IEnumerable<sp_UserInfo> SubUserList(User item)
        {
            IDataLayer<sp_UserInfo> um = new DataLayer<sp_UserInfo>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userID", item.Userid.ToString());
            var info = um.ProceduresGetData("sp_SubUserList", param);
            return info;
        }
        public Myprofile MyProfile(User param)
        {
            UserModel Umodel = Common.DecodeToken(param.Token);
            if (param.RegisterOrDeregister == "0")
            {
                param.Userid = Umodel.UserID;
            }
            else
            {
                var DecryptID = Common.Decrypt(param.RegisterOrDeregister);
                param.Userid = Convert.ToInt32(DecryptID);
            }
            var dataBind = Profile(param.Userid, Umodel.ClientID.Value);
            dataBind.userMaster.Password = "";
            dataBind.WhoView = Umodel.RoleID.Value;
            return dataBind;
        }
        private Myprofile Profile(int Userid, int ClientID)
        {
            PosExamStart PosExamdata = null;
            IDataLayer<UserMaster> Um = new DataLayer<UserMaster>();
            var data = Um.GetSingelDetailWithCondition(row => row.UserID == Userid && row.ClientID == ClientID);
            IDataLayer<RoleType> rtype = new DataLayer<RoleType>();
            var roletype = rtype.GetSingelDetailWithCondition(row => row.RoleID == data.RoleId);
            IDataLayer<UserDocument> UserDoc = new DataLayer<UserDocument>();
            var UserDocs = UserDoc.GetSingelDetailWithCondition(row => row.UserID == Userid);

            IDataLayer<LoginWithIncorrectAttempt> LoginAttempt = new DataLayer<LoginWithIncorrectAttempt>();
            var Attempt = LoginAttempt.GetSingelDetailWithCondition(row => row.EmailId == data.EmailAddress);

            IDataLayer<vw_userregiondata> Userregiondata = new DataLayer<vw_userregiondata>();
            var userregionList = Userregiondata.GetDetailsWithCondition(row => row.Userid == Userid);

            data.Password = "";
            if (data.UserProfilePic != null)
            {
                IDataLayer<ClientMaster> clm = new DataLayer<ClientMaster>();
                var clientdata = clm.GetSingelDetailWithCondition(row => row.Id == ClientID);
                data.UserProfilePic = clientdata.CoreAPIURL + data.UserProfilePic;
            }
            if (data.RoleId == 8)
            {
                DateTime dtCheckPosOldOrNew = Convert.ToDateTime("2020-11-21");
                if (data.CreatedDate < dtCheckPosOldOrNew)
                    PosExamdata = new PosExamStart() { PassOrFail = "Pass" };
                else
                {
                    IDataLayer<PosExamStart> posExam = new DataLayer<PosExamStart>();
                    PosExamdata = posExam.GetSingelDetailWithCondition(row => row.UserID == Userid);
                    ReleaseObject<IDataLayer<PosExamStart>>(ref posExam);
                }
            }

            Myprofile myprofile = new Myprofile()
            {
                roleType = roletype,
                userMaster = data,
                UserDocuments = UserDocs,
                WrongLoginAttempt = Attempt == null ? 0 : Attempt.Counter,
                UserRegions = userregionList,
                posExamStart = PosExamdata
            };
            rtype = null;
            Um = null;
            return myprofile;
        }
        public string PayoutData(PayyoutParam item)
        {
            string Response = "";
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Userid", item.Userid.ToString());
            param.Add("@p_BeneficiaryName", item.BeneficiaryName);
            param.Add("@p_BankAccountNo", item.BankAccountNo);
            param.Add("@p_IFSC_Code", item.IFSC_Code);
            param.Add("@p_PANNumber", item.PANNumber);
            param.Add("@p_bankname", item.BankName);
            param.Add("@p_BankBranch", item.BankBranch);
            var dataList = Data.ProcedureOutput("sp_PayoutData", param);
            if (!dataList.Contains("Exception"))
                Response = "Data Updated Successfully";
            else
                Response = "Something went wrong";
            return Response;
        }
        public string Gstin(GSTINParam param)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(param.Token);
            IDataLayer<UserMaster> Data = new DataLayer<UserMaster>();
            var exist = Data.GetSingelDetailWithCondition(row => row.UserID == param.Userid && row.ClientID == umodel.ClientID.Value);
            Data = null;
            Data = new DataLayer<UserMaster>();
            if (exist != null)
            {
                exist.GSTNumber = param.GSTIN;
                Response = Data.Update(exist);
            }
            else
                Response = "User Not found.";
            return Response;
        }
        public string PersonalDetails(PersonalDetails item)
        {
            string Response = "";
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Userid", item.Userid.ToString());
            param.Add("@p_DOB", item.DOB.ToString("yyyy-MM-dd"));
            param.Add("@p_Address", item.Address);
            param.Add("@p_PinCode", item.PinCode);
            var dataList = Data.ProcedureOutput("sp_Personaldetails", param);
            if (!dataList.Contains("Exception"))
                Response = "Data Updated Successfully";
            else
                Response = "Something went wrong";
            return Response;
        }
        public string GSTCeritificate(GSTCertificateParam item)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", item.Userid.ToString());
            param.Add("@p_gstin", item.GST_CERTIFICATE);
            param.Add("@p_gsCertificateUrl", item.GST_CERTIFICATE_URL);
            var dataList = Data.ProcedureOutput("sp_StoreGstCertificate", param);
            return dataList;
        }
        public string DocUrls(DocumentsParam item, string DocName)
        {
            string Response = "";
            IDataLayer<UserDocument> UserDoc = new DataLayer<UserDocument>();
            var data = UserDoc.GetSingelDetailWithCondition(row => row.UserID == item.Userid);
            UserDoc = null;
            UserDoc = new DataLayer<UserDocument>();
            switch (DocName)
            {
                case "Certificate":
                    if (data != null)
                    {
                        data.QualificationCertificate_URL = item.QualificationCertificate_URL;
                        data.QualificationDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            QualificationCertificate_URL = item.QualificationCertificate_URL,
                            UserID = item.Userid,
                            QualificationDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "Cheque":
                    if (data != null)
                    {
                        data.CancelCheque_URL = item.CancelCheque_URL;
                        data.CancelChequeDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            CancelCheque_URL = item.CancelCheque_URL,
                            UserID = item.Userid,
                            CancelChequeDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "PANCard":
                    if (data != null)
                    {
                        data.PAN_URL = item.PAN_URL;
                        data.PanDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            PAN_URL = item.PAN_URL,
                            UserID = item.Userid,
                            PanDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "AadharCardFront":
                    if (data != null)
                    {
                        data.Adhaar_Front_URL = item.Adhaar_Front_URL;
                        data.AdharFrontDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            Adhaar_Front_URL = item.Adhaar_Front_URL,
                            UserID = item.Userid,
                            AdharFrontDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "AadharCardBack":
                    if (data != null)
                    {
                        data.Adhaar_Back_URL = item.Adhaar_Back_URL;
                        data.AdharBackDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            Adhaar_Back_URL = item.Adhaar_Back_URL,
                            UserID = item.Userid,
                            AdharBackDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "TandC":
                    if (data != null)
                    {
                        data.TearnAndCondition = item.TearnAndCondition;
                        data.TermConditionDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            TearnAndCondition = item.TearnAndCondition,
                            UserID = item.Userid,
                            TermConditionDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "GSTFile":
                    if (data != null)
                    {
                        data.GST_CERTIFICATE_URL = item.GSTFile;
                        data.TermConditionDate = DateTime.Now;
                        Response = UserDoc.Update(data);
                    }
                    else
                    {
                        UserDocument doc = new UserDocument()
                        {
                            GST_CERTIFICATE_URL = item.GSTFile,
                            UserID = item.Userid,
                            GSTCertificateDate = DateTime.Now
                        };
                        Response = UserDoc.InsertRecord(doc);
                    }
                    break;
                case "UserProfilePic":
                    UploadUserPIC(item.Userid, item.UserPic);
                    break;
            }
            //IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            //Dictionary<string, string> param = new Dictionary<string, string>();
            //param.Add("@p_userid", item.Userid.ToString());
            //param.Add("@p_Adhaar_Front_URL", item.Adhaar_Front_URL);
            //param.Add("@p_Adhaar_Back_URL", item.Adhaar_Back_URL);
            //param.Add("@p_PAN_URL", item.PAN_URL);
            //param.Add("@p_QualificationCertificate_URL", item.QualificationCertificate_URL);
            //param.Add("@p_CancelCheque_URL", item.CancelCheque_URL);
            //param.Add("@p_TearnAndCondition", item.TearnAndCondition);
            //var dataList = Data.ProcedureOutput("sp_UploadFiles", param);
            return Response;
        }
        public PrivilegeListWithRoleid PrivilegeList(User Param)
        {
            int UserID = 0;
            UserModel model = CommonMethods.Common.DecodeToken(Param.Token);
            Param.Userid = model.UserID;
            IDataLayer<sp_privilegelist> Data = new DataLayer<sp_privilegelist>();
            Dictionary<string, string> paramPro = new Dictionary<string, string>();
            paramPro.Add("@p_userid", Param.Userid.ToString());
            var dataList = Data.ProceduresGetData("sp_privilegelist", paramPro);
            if (!string.IsNullOrEmpty(Param.RegisterOrDeregister))
                UserID = Convert.ToInt32(Common.Decrypt(Param.RegisterOrDeregister));
            List<userprivilegerolemapping> PrivilegeList = new List<userprivilegerolemapping>();
            if (UserID > 0)
            {
                IDataLayer<userprivilegerolemapping> UserData = new DataLayer<userprivilegerolemapping>();
                PrivilegeList = UserData.GetDetailsWithCondition(row => row.UserID == UserID).ToList();
            }
            List<PrivilegeMaster> lstData = new List<PrivilegeMaster>();
            userprivilegerolemapping mapped = null;
            foreach (sp_privilegelist Master in dataList)
            {
                if (PrivilegeList.Count > 0)
                {
                    mapped = PrivilegeList.Where(row => row.PrivilegeID == Master.PrivilegeID).FirstOrDefault();
                }
                lstData.Add(new PrivilegeMaster()
                {
                    NavBarMasterMenuID = Master.NavBarMasterMenuID,
                    GroupName = Master.GroupName,
                    PrivilegeID = Master.PrivilegeID,
                    PrivilegeName = Master.PrivilegeName,
                    Add = mapped != null ? (mapped.Addrecord == 1) : false,
                    Delete = mapped != null ? (mapped.deleterecord == 1) : false,
                    Edit = mapped != null ? (mapped.Editrecord == 1) : false,
                    AsAdmin = mapped != null ? (mapped.AsAdmin == 1) : false,
                    DownloadData = mapped != null ? (mapped.DownloadData == 1) : false
                });
                if (mapped != null)
                {
                    ReleaseObject<userprivilegerolemapping>(ref mapped);
                }

            }
            PrivilegeListWithRoleid DataList = new PrivilegeListWithRoleid()
            {
                privilegeMaster = lstData,
                RoleID = model.RoleID.Value
            };
            return DataList;
        }
        public string SavePrivileges(PrivilegeMap param)
        {
            string response = "";
            int userID = Convert.ToInt32(Common.Decrypt(param.Rows[0].EuserID));
            param.Rows = param.Rows.Select(row => { row.UserID = userID; return row; }).ToList();
            UserModel model = CommonMethods.Common.DecodeToken(param.Token);
            IDataLayer<userprivilegerolemapping> mappingList = new DataLayer<userprivilegerolemapping>();
            var users = mappingList.GetDetailsWithCondition(row => row.UserID == param.Rows[0].UserID);
            //try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\UserModel.txt", JsonConvert.SerializeObject(model)); } catch { }
            List<userprivilegerolemapping> lstuserprivilegerolemapping = new List<userprivilegerolemapping>();
            foreach (PrvRow row in param.Rows)
            {
                lstuserprivilegerolemapping.Add(new userprivilegerolemapping()
                {
                    Addrecord = row.Addrecord,
                    AssignBy = model.UserID,
                    UserID = row.UserID,
                    AssignDate = DateTime.Now,
                    deleterecord = row.deleterecord,
                    Editrecord = row.Editrecord,
                    NavBarMasterMenuID = row.NavBarMasterMenuID,
                    PrivilegeID = row.PrivilegeID,
                    AsAdmin = row.AsAdmin,
                    DownloadData = row.DownloadData
                });
            }
            List<userprivilegerolemapping> Already = new List<userprivilegerolemapping>();
            foreach (userprivilegerolemapping map in lstuserprivilegerolemapping)
            {
                if (users != null)
                    Already = users.Where(row => row.PrivilegeID == map.PrivilegeID).ToList();
                IDataLayer<userprivilegerolemapping> dataLayer = new DataLayer<userprivilegerolemapping>();

                if (Already.Count() <= 0)
                {
                    response = dataLayer.InsertRecord(map);
                    dataLayer = null;
                }
                else
                {
                    map.ID = Already[0].ID;
                    response = dataLayer.Update(map);
                    dataLayer = null;
                }
                Already = null;
            }
            return response;
        }
        public string DeletePrivileges(PrivilegeMap param)
        {
            string Response = "";
            int userID = Convert.ToInt32(Common.Decrypt(param.Rows[0].EuserID));
            IDataLayer<userprivilegerolemapping> dataLayer = null;
            IDataLayer<userprivilegerolemapping> deleteLayer = null;
            foreach (var item in param.Rows)
            {
                dataLayer = new DataLayer<userprivilegerolemapping>();
                var row = dataLayer.GetSingelDetailWithCondition(row => row.UserID == userID && row.PrivilegeID == item.PrivilegeID);
                deleteLayer = new DataLayer<userprivilegerolemapping>();
                Response = deleteLayer.DeleteRecord(row);
                dataLayer = null;
                deleteLayer = null;
                try
                {
                    GC.SuppressFinalize(dataLayer);
                    GC.SuppressFinalize(deleteLayer);
                }
                catch (Exception ex) { }
            }
            return Response;
        }
        public string Deregister(User param)
        {
            UserModel UserModel = Common.DecodeToken(param.Token);
            string Response = "";
            try
            {
                IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
                var userdata = um.GetSingelDetailWithCondition(row => row.UserID == param.Userid);
                if (userdata != null)
                {
                    bool action = false;
                    if (param.RegisterOrDeregister == "Deregister")
                        action = false;
                    else if (param.RegisterOrDeregister == "Register")
                        action = true;

                    userdata.Active = action;
                    IDataLayer<UserMaster> updateum = new DataLayer<UserMaster>();
                    IDataLayer<Logtable> logtable = new DataLayer<Logtable>();
                    Response = updateum.Update(userdata);
                    logtable.InsertRecord(new Logtable() { ID = 0, Action = param.RegisterOrDeregister, Date = DateTime.Now, Who = UserModel.UserID, ForUser = userdata.UserID });
                    ReleaseObject<IDataLayer<Logtable>>(ref logtable);
                    ReleaseObject<IDataLayer<UserMaster>>(ref updateum);
                }
            }
            catch (Exception ex) { Response = ex.Message; }
            return Response;
        }
        public sp_ReferAndPosSeries ReferAndPosSeries(RoleTypeParam item)
        {
            sp_ReferAndPosSeries modelData = new sp_ReferAndPosSeries();
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            int ClientID = model.ClientID.Value;
            IDataLayer<sp_ReferAndPosSeries> data = new DataLayer<sp_ReferAndPosSeries>();
            Dictionary<string, string> paramPro = new Dictionary<string, string>();
            paramPro.Add("@p_RoleID", item.ID.ToString());
            paramPro.Add("@p_ClientID", ClientID.ToString());
            var dataList = data.ProceduresGetData("sp_ReferAndPosSeries", paramPro);
            if (dataList == null)
                return modelData;
            return dataList.ToList().FirstOrDefault();
        }
        public IEnumerable<sp_PriviligeList> PriviligeList(CommonParam Item)
        {
            UserModel User = Common.DecodeToken(Item.Token);
            IDataLayer<sp_PriviligeList> Data = new DataLayer<sp_PriviligeList>();
            Dictionary<string, string> paramPro = new Dictionary<string, string>();
            paramPro.Add("@p_Userid", User.UserID.ToString());
            var dataList = Data.ProceduresGetData("sp_PriviligeList", paramPro);
            return dataList;
        }
        public string GenerateLink(GenerateLinkParam item)
        {
            string Response = "";
            string CheckResponse = "";
            UserModel User = Common.DecodeToken(item.Token);
            string Unique = Guid.NewGuid().ToString().Replace("-", "").Replace("/", "");
            IDataLayer<GeneratedUniquePOSID> data = new DataLayer<GeneratedUniquePOSID>();
            GeneratedUniquePOSID datadetails = new GeneratedUniquePOSID()
            {
                CLientid = User.ClientID.Value,
                LinkType = item.LinkType,
                UniqueEmail = item.LinkType == "Affiliate" ? Unique + "@affiliate.com" : Unique + "@pos.com",
                UniqueID = Unique
            };
            CheckResponse = data.InsertRecord(datadetails);
            if (CheckResponse == "Data Save Successfully.")
            {
                CheckResponse = "";
                string Pass = Guid.NewGuid().ToString().ToUpper().Substring(0, 4);
                Pass = datadetails.UniqueEmail.Substring(0, 3) + "@" + Pass;
                Pass = Common.Encrypt(Pass);
                UserMaster merge = new UserMaster()
                {
                    Active = true,
                    CreatedDate = DateTime.Now,
                    EmailAddress = datadetails.UniqueEmail,
                    KeyAccountManager = User.UserID,
                    ClientID = User.ClientID,//Item.ClientID,
                    UserID = 0,
                    UserName = item.LinkType == "POS" ? "Affiliate-POS" : "Affiliate",
                    RoleId = item.LinkType == "Affiliate" ? 29 : 8,
                    CreatedBy = User.UserID,//Item.CreateBy.Value,
                    Password = Pass
                };
                IDataLayer<UserMaster> Userdata = new DataLayer<UserMaster>();
                CheckResponse = Userdata.InsertRecord(merge);
                Userdata = null;
                Userdata = new DataLayer<UserMaster>();
                var SingleUserData = Userdata.GetSingelDetailWithCondition(row => row.EmailAddress == datadetails.UniqueEmail && row.ClientID == User.ClientID);

                for (int i = 0; i < item.Rows.Count; i++)
                {
                    item.Rows[i].EuserID = SingleUserData.UserID.ToString();
                }

                if (CheckResponse == "Data Save Successfully.")
                    SavePrivileges(item);

                IDataLayer<ClientMaster> Clientdata = new DataLayer<ClientMaster>();
                var single = Clientdata.GetSingelDetailWithCondition(row => row.Id == User.ClientID);
                Response = single.companyURL + "/Myaccount/Account/LoginLink?Token=" + Unique;
                merge = null;
                Userdata = null;
            }
            User = null;
            data = null;
            datadetails = null;
            return Response;
        }
        public LoginResponseModel LogWithLinkiUser(Login Model)
        {
            IDataLayer<GeneratedUniquePOSID> data = new DataLayer<GeneratedUniquePOSID>();
            LoginResponseModel loginResponse = new LoginResponseModel();
            UserModel model = new UserModel();
            var isAvailable = data.GetSingelDetailWithCondition(row => row.UniqueID == Model.UserID);
            if (isAvailable != null)
            {
                IDataLayer<UserMaster> User = new DataLayer<UserMaster>();
                var UserData = User.GetSingelDetailWithCondition(row => row.EmailAddress == isAvailable.UniqueEmail && row.Active == true && row.ClientID == isAvailable.CLientid); ;
                if (UserData != null)
                {
                    loginResponse.Status = "Success";
                    model.status = "Success";
                    model.UserID = UserData.UserID;
                    model.mobileNo = "1234567890";
                    model.ClientID = UserData.ClientID;
                    model.UserName = UserData.UserName;
                    model.Email = UserData.EmailAddress;
                    model.ISActive = UserData.Active;
                    model.RoleID = UserData.RoleId;
                    //model.Createddate = UserData.CreatedDate;
                    loginResponse.Token = Authenticate(model);
                    loginResponse.UserName = model.UserName;
                    loginResponse.Status = model.status;
                }
                else
                {
                    loginResponse.Status = "Invalid Token";
                }
            }
            else
            {
                loginResponse.Status = "Invalid Token";
            }
            return loginResponse;
        }
        public IEnumerable<sp_AcitveUserList> ActiveUserList(CommonParam Item)
        {
            UserModel UM = Common.DecodeToken(Item.Token);
            IDataLayer<sp_AcitveUserList> Data = new DataLayer<sp_AcitveUserList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", UM.UserID.ToString());
            param.Add("@p_ClientID", UM.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_AcitveUserList", param);
            //var dataList = Data.ProceduresGetData("new_procedure", param);
            return dataList;
        }
        public string UploadLogoUrl(UserModel Um, string fileName)
        {
            string Response = "";
            try
            {
                IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
                var ClientData = data.GetSingelDetailWithCondition(row => row.Id == Um.ClientID);
                if (ClientData != null)
                {
                    data = null;
                    data = new DataLayer<ClientMaster>();
                    ClientData.CompanyLogo = ClientData.CoreAPIURL + "/Downloads/Logo/" + fileName;
                    Response = data.Update(ClientData);
                }
            }
            catch (Exception ex) { Response = ex.Message; }
            return Response;
        }
        public string UploadProfilePic(UserModel Um, string fileName)
        {
            string Response = "";
            try
            {
                IDataLayer<UserMaster> Umaster = new DataLayer<UserMaster>();
                var UserRow = Umaster.GetSingelDetailWithCondition(row => row.UserID == Um.UserID &&
                                                                   row.ClientID == row.ClientID);
                IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
                var ClientData = data.GetSingelDetailWithCondition(row => row.Id == Um.ClientID);
                if (ClientData != null && UserRow != null)
                {
                    Umaster = null;
                    Umaster = new DataLayer<UserMaster>();
                    UserRow.UserProfilePic = ClientData.CoreAPIURL + "/Downloads/UserPictures/" + fileName;
                    Response = Umaster.Update(UserRow);
                }
            }
            catch (Exception ex) { Response = ex.Message; }
            return Response;
        }
        public string UploadUserPIC(int UsreID, string path)
        {
            string Response = "";
            IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
            int user = UsreID;
            var CHeckExist = um.GetSingelDetailWithCondition(row => row.UserID == user);
            if (CHeckExist != null)
            {
                um = new DataLayer<UserMaster>();
                CHeckExist.UserProfilePic = path;
                Response = um.Update(CHeckExist);
            }
            else
            {
                Response = "Not Found";
            }
            return Response;
        }
        public string UnlockIncorrectAttempt(BaseParam Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            try
            {
                int Userid = Convert.ToInt32(Common.Decrypt(Item.UserID));
                if (um.RoleID == 1 || um.RoleID == 28)
                {
                    IDataLayer<UserMaster> UMas = new DataLayer<UserMaster>();
                    var Userdata = UMas.GetSingelDetailWithCondition(row => row.UserID == Userid && row.ClientID == um.ClientID);
                    GC.SuppressFinalize(UMas);
                    if (Userdata != null)
                    {
                        IDataLayer<LoginWithIncorrectAttempt> attempt = new DataLayer<LoginWithIncorrectAttempt>();
                        var attemptData = attempt.GetSingelDetailWithCondition(row => row.EmailId == Userdata.EmailAddress && row.ClientID == um.ClientID);
                        if (attemptData != null)
                        {
                            GC.SuppressFinalize(attempt);
                            attempt = null;
                            attempt = new DataLayer<LoginWithIncorrectAttempt>();
                            Response = attempt.DeleteRecord(attemptData);
                            if (Response == "Data Deleted Successfully.")
                                Response = "Unlocked Successfully.";
                        }
                    }
                }
                else
                {
                    Response = "Access Denied.";
                }
                GC.SuppressFinalize(um);
            }
            catch (Exception ex) { Response = "User not found."; }
            return Response;
        }
        public Myprofile GetUserDetilas(CommonParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            var data = Profile(um.UserID, um.ClientID.Value);
            data.userMaster.Password = "";
            return data;
        }
        public string UpdateUserinfo(UpdateUserParam Item)
        {
            string Response = "";
            try
            {
                UserModel Umodel = Common.DecodeToken(Item.Token);
                int userID = Convert.ToInt32(Common.Decrypt(Item.UserID));
                IDataLayer<UserMaster> umaster = new DataLayer<UserMaster>();
                IDataLayer<Logtable> logtable = new DataLayer<Logtable>();
                var data = umaster.GetSingelDetailWithCondition(row => row.UserID == userID && row.ClientID == Umodel.ClientID);
                if (data != null)
                {
                    if (data.EmailAddress != Item.Email)
                        data.EmailAddress = Item.Email;

                    data.UserName = Item.Name;
                    data.MobileNo = Item.MobileNo;
                    data.AdhaarNumber = Item.Adhaar;
                    umaster = null;
                    umaster = new DataLayer<UserMaster>();
                    Response = umaster.Update(data);
                    logtable.InsertRecord(new Logtable() { ID = 0, Action = "Update UserINFO", Date = DateTime.Now, Who = Umodel.UserID, ForUser = userID });
                }
                else
                {
                    Response = "User Not Found";
                }
                ReleaseObject<IDataLayer<UserMaster>>(ref umaster);
                ReleaseObject<IDataLayer<Logtable>>(ref logtable);
            }
            catch (Exception ex) { Response = "User is not valid"; }
            return Response;
        }

        public IEnumerable<dynamic> FilterUserByNameMobileEmail(FilterText item)
        {
            IEnumerable<UserMaster> userlist = null;
            UserModel Umod = Common.DecodeToken(item.Token);
            IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
            userlist = um.GetDetailsWithCondition(row => (row.EmailAddress.ToLower().Contains(item.FilterUser.ToLower()) ||
                                                        row.UserName.ToLower().Contains(item.FilterUser.ToLower()))
                                                        && row.ClientID == Umod.ClientID.Value);
            if (userlist.Count() > 0)
            {
                var dataList = userlist.Select(row => new
                {
                    row.UserName,
                    row.UserID
                });
                return dataList;
            }
            return userlist;
        }
        public string UpdateAlterNet(UpdateUserParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            int userID = Convert.ToInt32(Common.Decrypt(Item.UserID));
            IDataLayer<UserMaster> UMaster = new DataLayer<UserMaster>();
            var Exist = UMaster.GetSingelDetailWithCondition(row => row.UserID == userID && row.ClientID == Umodel.ClientID.Value);
            if (Exist != null)
            {
                UMaster = null;
                UMaster = new DataLayer<UserMaster>();
                Exist.Alternatecode = Item.Name;
                Response = UMaster.Update(Exist);
            }
            else
                Response = "Not Found.";
            return Response;
        }
        public string GetSampleBulkImportFle(CommonParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ClientMaster> clientmaster = new DataLayer<ClientMaster>();
            var Data = clientmaster.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID.Value);
            if (Data != null)
            {
                Response = Data.CoreAPIURL + "/Downloads/SampleBulkImport/SampleXL.csv";
            }
            else
                Response = "Not Found.";
            return Response;
        }
        public LICBroking GetAgencyCode(CommonParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<LICBroking> clientmaster = new DataLayer<LICBroking>();
            var Data = clientmaster.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value);
            return Data;
        }
        public string SaveAgencyCode(AgencyCodeParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<LICBroking> clientmaster = new DataLayer<LICBroking>();
            var Data = clientmaster.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value);
            if (Data != null)
            {
                Data.AgencyCode = Item.AgencyCode;
                Data.APIURL = Item.APIUrl;
                Data.Authorization = Item.AuthParam;
                Data.Username = Item.UserName;
                Data.Password = Item.PasswordParam;
                Data.RedirectionURL = Item.RedirectionUrl;
                Response = clientmaster.Update(Data);
            }
            else
                Response = clientmaster.InsertRecord(new LICBroking()
                {
                    ID = 0,
                    AgencyCode = Item.AgencyCode,
                    ClientID = umodel.ClientID.Value,
                    APIURL = Item.APIUrl,
                    Authorization = Item.AuthParam,
                    RedirectionURL = Item.RedirectionUrl,
                    Password = Item.PasswordParam,
                    Username = Item.UserName
                });
            return Response;
        }
        public string SaveSalesPersonCode(SalesPersonCodeParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<LicSalesPersonMaster> clientmaster = new DataLayer<LicSalesPersonMaster>();
            var Data = clientmaster.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value && row.UserID == Item.UserID);
            if (Data != null)
            {
                Data.SalesPersonCode = Item.SalesPersonCode;
                Data.UserID = Item.UserID;
                Response = clientmaster.Update(Data);
            }
            else
                Response = clientmaster.InsertRecord(new LicSalesPersonMaster()
                {
                    ID = 0,
                    ClientID = umodel.ClientID.Value,
                    SalesPersonCode = Item.SalesPersonCode,
                    UserID = Item.UserID
                });
            return Response;
        }
        public IEnumerable<sp_GetSalesPersonRecord> GetSalesPersonCode(CommonParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_GetSalesPersonRecord> um = new DataLayer<sp_GetSalesPersonRecord>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", umodel.ClientID.ToString());
            var ProceduresGetData = um.ProceduresGetData("sp_GetSalesPersonRecord", param);
            return ProceduresGetData;
        }
        public LicRedirectionModel ChecksumAPIForSpCode(CommonParam Item)
        {
            LicRedirectionModel licRedirectionModel = new LicRedirectionModel();
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<LICBroking> lICBroking = new DataLayer<LICBroking>();
            var data = lICBroking.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID);
            if (data != null)
            {
                //umodel.UserID = 2277;
                IDataLayer<LicSalesPersonMaster> licSalesPersonMaster = new DataLayer<LicSalesPersonMaster>();
                var dataSalesPerson = licSalesPersonMaster.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID && row.UserID == umodel.UserID);
                try
                {
                    var basickey = Convert.ToBase64String(Encoding.Default.GetBytes("" + data.Username + ":" + data.Password + ""));
                    var client = new RestClient(data.APIURL);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("authorization", "Basic " + basickey + "");
                    request.AddHeader("content-type", "application/json");
                    var ReqModel = new
                    {
                        agency_code = data.AgencyCode,
                        sp_code = dataSalesPerson.SalesPersonCode
                    };
                    request.AddParameter("application/json", JsonConvert.SerializeObject(ReqModel), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    var DataModel = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    if (DataModel.message != null)
                    {
                        licRedirectionModel.AgencyCode = data.AgencyCode;
                        licRedirectionModel.CheckSum = DataModel.checksum;
                        licRedirectionModel.SpCode = dataSalesPerson.SalesPersonCode;
                        licRedirectionModel.ActionURL = data.RedirectionURL;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return licRedirectionModel;
        }
        public string GetUserReport(int UserID, int Client, string Option, DateTime StartDate, DateTime EndDate)
        {
            string path = "";
            string Qry = "";
            if (Option == "noniib")
                Qry = "call sp_DownloadUserList(" + UserID + "," + Client + ",'" + StartDate.ToString("yyyy-MM-dd") + "','" + EndDate.ToString("yyyy-MM-dd") + "')";
            else if (Option == "iib")
                Qry = "call sp_DownloadUserListIIB(" + UserID + "," + Client + ",'" + StartDate.ToString("yyyy-MM-dd") + "','" + EndDate.ToString("yyyy-MM-dd") + "')";
            path = FileReturnUrl(GetTableData(Qry), UserID, Client, "UserData_");
            return path;
        }
        public string GetUserReportWithFilter(int UserID, int Client)
        {
            string path = "";
            string Qry = "call sp_DownloadNHUserList(" + UserID + "," + Client + ")";
            path = FileReturnUrl(Qry, UserID, Client, "UserData_");
            return path;
        }
        public IEnumerable<GetFilterUserList> GetUserFilterByRole(FilterRoleUser item)
        {
            List<GetFilterUserList> DataUsers = new List<GetFilterUserList>();
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<UserMaster> Um = new DataLayer<UserMaster>();
            var umList = Um.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID && row.RoleId == item.RoleID && row.Active == true);
            if (umList != null)
            {
                DataUsers = umList.Select(row => new GetFilterUserList()
                {
                    ClientID = row.ClientID,
                    Email = row.EmailAddress,
                    UserID = row.UserID,
                    UserName = row.UserName
                }).ToList();
            }
            return DataUsers;
        }
        public string GetCertificate(CommonParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ClientPosCertificateBase> ClientPosCertificateBase = new DataLayer<ClientPosCertificateBase>();
            IDataLayer<ClientMaster> ClientList = new DataLayer<ClientMaster>();
            var client = ClientList.GetSingelDetailWithCondition(ro => ro.Id == umodel.ClientID.Value);
            try
            {
                var exist = ClientPosCertificateBase.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value);
                if (exist != null)
                {
                    IDataLayer<UserMaster> User = new DataLayer<UserMaster>();
                    var userdata = User.GetSingelDetailWithCondition(row => row.UserID == umodel.UserID);
                    Response = exist.HTMLFormat;
                    Response = Response.Replace("[POSNAME]", userdata.UserName);
                    Response = Response.Replace("[POSDATE]", userdata.CreatedDate.ToString("dd/MM/yyyy"));
                    Response = Response.Replace("[address]", userdata.Address);
                    Response = Response.Replace("[ProfileImage]", userdata.UserProfilePic != null ? client.CoreAPIURL + userdata.UserProfilePic : "Not Uploaded");
                    Response = Response.Replace("[PANNo]", userdata.PANNumber == null ? "" : userdata.PANNumber);
                    Response = Response.Replace("[AadharNo]", userdata.AdhaarNumber == null ? "" : userdata.AdhaarNumber.Value.ToString());
                    Response = Response.Replace("[POS IdentificationNumber]", userdata.PosPrifix + userdata.PosVal);
                }
                else
                {
                    Response = "Not Found.";
                }
            }
            catch (Exception ex) { Response = "Not Found."; }
            return Response;
        }
        public string UserVerification(UserVerificationParam item)
        {
            string Resp = "";
            UserModel um = Common.DecodeToken(item.Token);
            try
            {
                IDataLayer<dynamic> Data = new DataLayer<dynamic>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_clientid", um.ClientID.ToString());
                param.Add("@p_userids", item.UserIDs);
                param.Add("@p_Status", item.VerifyMessage);
                param.Add("@p_IsDuplicate", item.IsDuplicate == true ? "1" : "0");
                var dataList = Data.ProcedureOutput("sp_Userverification", param);
                if (!dataList.Contains("Exception"))
                {
                    Resp = "Success";
                    string MailOption = item.IsDuplicate == true ? "UserVerification" : "RejectUser";
                    Thread Th = new Thread(() => VerificationEmail(item, um, MailOption));
                    Th.Start();
                }
                else Resp = "404";
            }
            catch (Exception ex) { Resp = "Error"; }
            return Resp;
        }
        private void VerificationEmail(UserVerificationParam Item, UserModel um, string MailOption)
        {
            string Message = "";
            try
            {
                IDataLayer<Register_as_pos> registerReq = null;
                var LstIDs = Item.UserIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                IDataLayer<MailServerOption> mailOpt = new DataLayer<MailServerOption>();
                var mailOptData = mailOpt.GetSingelDetailWithCondition(row => row.MailServiceMaster == MailOption);// "UserVerification");
                IDataLayer<NotificationEmail> notificationmail = new DataLayer<NotificationEmail>();
                var emailData = notificationmail.GetSingelDetailWithCondition(row => row.ForAction == MailOption && row.ClientID == um.ClientID);
                ReleaseObject<IDataLayer<NotificationEmail>>(ref notificationmail);
                ReleaseObject<IDataLayer<MailServerOption>>(ref mailOpt);
                if (emailData != null)
                {
                    IDataLayer<MailServer> mailserver = new DataLayer<MailServer>();
                    var MailServerExist = mailserver.GetSingelDetailWithCondition(row => row.MailserveroptionID == mailOptData.ID
                                                && row.ClientID == um.ClientID);
                    if (MailServerExist != null)
                    {
                        var MailBody = "";
                        foreach (var ID in LstIDs)
                        {
                            registerReq = new DataLayer<Register_as_pos>();
                            var Exist = registerReq.GetSingelDetailWithCondition(row => row.ID == Convert.ToInt32(ID));
                            MailBody = emailData.MailBody;
                            MailBody = MailBody.Replace("[Name]", Exist.FirstName + " " + Exist.LastName);
                            MailBody = MailBody.Replace("[Body]", Item.VerifyMessage);

                            Common.mailMaster(MailServerExist.FromEmail, Exist.Email, emailData.MailSubject, MailBody,
                                MailServerExist.UserName, MailServerExist.Password, MailServerExist.HostName, MailServerExist.Port, MailServerExist.UseDefaultCredential,
                                MailServerExist.EnableSsl, ref Message);
                            ReleaseObject<IDataLayer<Register_as_pos>>(ref registerReq);
                        }
                    }
                }
            }
            catch (Exception ex) { try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + "\\VerificationEmail.txt", ex.StackTrace.ToString()); } catch { } }
        }
        public IEnumerable<vw_userregiondata> RegionNDBranch(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<vw_userregiondata> Data = new DataLayer<vw_userregiondata>();
            return Data.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID.Value);
        }
        public UserModel UserInfoCheckWithStatus(CommonParam param)
        {
            UserModel Umodel = new UserModel();
            try
            {
                var Exist = Common.DecodeToken(param.Token);
                if (Exist != null)
                {
                    IDataLayer<UserMaster> umaster = new DataLayer<UserMaster>();
                    var UserData = umaster.GetSingelDetailWithCondition(row => row.UserID == Exist.UserID);
                    Umodel.status = UserData.Active == true ? "Active" : "DeactiveUser";
                    if (Umodel.status == "Active")
                    {
                        Umodel = Exist;
                        Umodel.status = "Active";
                    }
                }
                else
                {
                    Umodel.status = "User Not Found.";
                }
            }
            catch (Exception ex)
            {
                Umodel.status = "DeactiveUser";
            }
            return Umodel;
        }
        public vw_userregiondata GetUserRegionndBranch(User param)
        {
            UserModel Umodel = Common.DecodeToken(param.Token);
            IDataLayer<vw_userregiondata> Data = new DataLayer<vw_userregiondata>();
            return Data.GetSingelDetailWithCondition(row => row.Userid == param.Userid && row.ClientID == Umodel.ClientID);
        }
        public IEnumerable<BindPosExam> GetPosExamSummary(CommonParam param)
        {
            List<BindPosExam> data = new List<BindPosExam>();
            UserModel uModel = Common.DecodeToken(param.Token);
            IDataLayer<ClientMaster> cm = new DataLayer<ClientMaster>();
            var CmData = cm.GetSingelDetailWithCondition(row => row.Id == uModel.ClientID.Value);
            if (CmData != null)
            {
                IDataLayer<Posexamsummary> posexamsummary = new DataLayer<Posexamsummary>();
                var exdata = posexamsummary.GetDetailsWithCondition(row => row.ClientID == uModel.ClientID.Value);
                if (exdata.Count() > 0)
                {
                    data = exdata.Select(row => new BindPosExam()
                    {
                        DocID = row.ID,
                        Link = CmData.CoreAPIURL + row.DocName,
                        Name = row.FileName
                    }).ToList();
                }
            }
            return data;
        }
        public string RemovePosDoc(PosDocParam Item)
        {
            string Respo = "";
            UserModel uModel = Common.DecodeToken(Item.Token);
            IDataLayer<Posexamsummary> posexamsummary = new DataLayer<Posexamsummary>();
            var exdata = posexamsummary.GetSingelDetailWithCondition(row => row.ClientID == uModel.ClientID.Value && row.ID == Item.DocID);
            if (exdata != null)
            {
                posexamsummary = null;
                posexamsummary = new DataLayer<Posexamsummary>();
                Respo = posexamsummary.DeleteRecord(exdata);
            }
            else
                Respo = "Doc Not Found.";
            return Respo;
        }
        public string DocumentCheck(DocCHeckParam param)
        {
            string Respones = "";
            UserModel umodel = Common.DecodeToken(param.Token);
            IDataLayer<UserDocument> udoc = new DataLayer<UserDocument>();
            var UserDoc = udoc.GetSingelDetailWithCondition(row => row.UserID == param.UserID);
            udoc = null;
            udoc = new DataLayer<UserDocument>();
            if (UserDoc != null)
            {
                switch (param.DocName.ToLower())
                {
                    case "adhaar front":
                        UserDoc.IsAdharFrontCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "adhaar back":
                        UserDoc.IsAdharBackCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "pan card":
                        UserDoc.IsPanCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "highest education certificate":
                        UserDoc.IsQualificationCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "cancel cheque":
                        UserDoc.IsCancelChequeCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "pos certificate":
                        UserDoc.IsPosCertificateCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "gst certificate":
                        UserDoc.IsGSTCertificateCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                    case "terms & conditions":
                        UserDoc.IsTermCheck = param.IsCheck;
                        Respones = udoc.Update(UserDoc);
                        break;
                }
            }
            else
                Respones = "User not found.";
            return Respones;
        }
        public IEnumerable<sp_FeeddbackData> UsersFeedbackList(CommonParam Item)
        {
            UserModel UM = Common.DecodeToken(Item.Token);
            IDataLayer<sp_FeeddbackData> Data = new DataLayer<sp_FeeddbackData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_clientid", UM.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_FeeddbackData", param);
            return dataList;
        }
        public string CheckPinAvailable(CheckPinParam param)
        {
            IDataLayer<CrmCityState> crmCityState = new DataLayer<CrmCityState>();
            var Exist = crmCityState.GetSingelDetailWithCondition(row => row.Pin_code == param.PinCode);
            if (Exist == null)
                return "NotFound";
            else
                return "Found";

        }
        public string UpdatePincode(UpdatePinParam Item)
        {
            IDataLayer<CrmCityState> crmCityState = new DataLayer<CrmCityState>();
            return crmCityState.InsertRecord(new CrmCityState()
            {
                District_Name = Item.PinCity,
                State_Name = Item.PinState,
                ID = 0,
                Pin_code = Item.PinCode
            });
        }
        public CrmCityState GetStateCity(CheckPinParam Item)
        {
            IDataLayer<CrmCityState> crmCityState = new DataLayer<CrmCityState>();
            var Exist = crmCityState.GetSingelDetailWithCondition(row => row.Pin_code == Item.PinCode);
            return Exist;
        }
        #endregion UserController

        #region Account Controller
        public LoginResponseModel LoginUser(Login LoginModel)
        {
            var clientid = 0;
            UserModel model = new UserModel();
            LoginResponseModel loginResponse = new LoginResponseModel();
            try
            {
                IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
                var clientInfo = data.GetDetailsWithCondition(row => row.companyURL.Contains(LoginModel.ClientURL)).FirstOrDefault();
                clientid = clientInfo.Id;
                IDataLayer<LoginWithIncorrectAttempt> Incorrect = null;
                IDataLayer<LoginWithIncorrectAttempt> CheckIncorrectBeforeLogin = new DataLayer<LoginWithIncorrectAttempt>();
                var LoginCounter = CheckIncorrectBeforeLogin.GetSingelDetailWithCondition(row => row.EmailId == LoginModel.UserID && row.ClientID == clientid);

                int counter = 0;
                if (LoginCounter != null)
                    counter = LoginCounter.Counter;
                if (counter >= 5)
                {
                    model.status = "Password is expired, Please Reset.";
                    loginResponse.Status = model.status;
                }
                else
                {
                    var password = Common.Encrypt(LoginModel.Password);
                    IDataLayer<UserMaster> User = new DataLayer<UserMaster>();
                    var UserData = User.GetSingelDetailWithCondition(row => row.EmailAddress.ToLower() == LoginModel.UserID.ToLower() && row.Password == password && row.Active == true && row.ClientID == clientid);
                    if (UserData == null)
                    {
                        model.status = "Incorrect UserId and password.";
                        loginResponse.Status = model.status;
                        var CheckExist = User.GetSingelDetailWithCondition(row => row.EmailAddress == LoginModel.UserID && row.ClientID == clientid);
                        if (CheckExist != null)
                        {
                            Incorrect = new DataLayer<LoginWithIncorrectAttempt>();
                            var incrrectAttempt = Incorrect.GetSingelDetailWithCondition(row => row.EmailId == LoginModel.UserID && row.ClientID == clientid);
                            if (incrrectAttempt == null)
                            {
                                LoginWithIncorrectAttempt AttempCout = new LoginWithIncorrectAttempt()
                                {
                                    ClientID = clientid,
                                    Counter = 1,
                                    EmailId = LoginModel.UserID
                                };
                                Incorrect = null;
                                Incorrect = new DataLayer<LoginWithIncorrectAttempt>();
                                Incorrect.InsertRecord(AttempCout);
                                loginResponse.Status += (5 - AttempCout.Counter).ToString() + " Attempt Left";
                            }
                            else
                            {
                                incrrectAttempt.Counter += 1;
                                Incorrect = null;
                                Incorrect = new DataLayer<LoginWithIncorrectAttempt>();
                                Incorrect.Update(incrrectAttempt);
                                loginResponse.Status += (5 - incrrectAttempt.Counter).ToString() + " Attempt Left";
                            }
                        }
                    }
                    else
                    {
                        string status = LoginAttempFun(UserData.UserID);
                        if (status == "Ok")
                        {
                            if (counter > 0)
                            {
                                Incorrect = new DataLayer<LoginWithIncorrectAttempt>();
                                Incorrect.DeleteRecord(LoginCounter);
                            }
                            model.status = "Success";
                            model.UserID = UserData.UserID;
                            model.ClientID = UserData.ClientID;
                            model.UserName = UserData.UserName;
                            model.Email = UserData.EmailAddress;
                            model.ISActive = UserData.Active;
                            model.RoleID = UserData.RoleId;
                            //model.Createddate = UserData.CreatedDate;
                            model.mobileNo = UserData.MobileNo;
                            loginResponse.Token = Authenticate(model);
                            loginResponse.UserName = model.UserName;
                            loginResponse.Status = model.status;
                            //Make Login History
                            LogintimeSave(UserData.UserID, UserData.ClientID.Value, LoginModel.DeviceName);
                            //ENd
                        }
                        else
                        {
                            model.status = status;
                            loginResponse.Status = status;
                        }
                        /*
                          string start = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        string last = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        string TokenSTR = Common.Encrypt(start +JsonConvert.SerializeObject(token) + last);
                         */
                    }
                }
            }
            catch (Exception ex) { loginResponse.Status = ex.Message; model.status = ex.Message; }
            return loginResponse;
        }
        private void LogintimeSave(int userID, int Clientid, string loginDevice = "")
        {
            IDataLayer<LoginTimeHistory> timeHis = new DataLayer<LoginTimeHistory>();
            /*
            var LoginHis = timeHis.GetSingelDetailWithCondition(row => row.ClientID == Clientid && row.UserID == userID && row.Action == "Login");
            if (LoginHis == null)
            {
                ReleaseObject<IDataLayer<LoginTimeHistory>>(ref timeHis);
                timeHis = null;
                timeHis = new DataLayer<LoginTimeHistory>();*/
            LoginTimeHistory log = new LoginTimeHistory()
            {
                Action = "Login",
                ClientID = Clientid,
                UserID = userID,
                LoginTimeHis = DateTime.Now,
                LoginDevice = loginDevice
            };
            var Response = timeHis.InsertRecord(log);
            if (Response == "Data Save Successfully.")
            {
                ReleaseObject<LoginTimeHistory>(ref log);
            }
            ReleaseObject<IDataLayer<LoginTimeHistory>>(ref timeHis);
            timeHis = null;
            //}
        }

        private string LoginAttempFun(int UserID)
        {
            string Return = "Ok";
            IDataLayer<LoginAttempt> Attempt = new DataLayer<LoginAttempt>();
            var Logindata = Attempt.GetSingelDetailWithCondition(row => row.UserID == UserID);
            if (Logindata == null)
            {
                LoginAttempt LoginAttemptVal = new LoginAttempt()
                {
                    UserID = UserID,
                    LoginDate = DateTime.Now
                };
                IDataLayer<LoginAttempt> AddAttempt = new DataLayer<LoginAttempt>();
                AddAttempt.InsertRecord(LoginAttemptVal);
            }
            else
            {
                var dayCounter = (DateTime.Now - Logindata.LoginDate).TotalDays;
                if (dayCounter >= 90)
                {
                    Return = "Password Expired";
                }
                else
                {
                    var counter = 90 - Convert.ToInt32(dayCounter);
                    if (counter <= 7)
                    {
                        IDataLayer<OfflineQueryRelatedMessage> offlineQueryRelatedMessage
                            = new DataLayer<OfflineQueryRelatedMessage>();
                        var Exist = offlineQueryRelatedMessage.GetSingelDetailWithCondition(row => row.ToUser == UserID &&
                        row.Subject == counter.ToString() + " Days password expire notification.");
                        if (Exist == null)
                        {
                            OfflineQueryRelatedMessage obj = new OfflineQueryRelatedMessage()
                            {
                                FromUser = 0,
                                ID = 0,
                                Message = "Lets reset your password, as your password will be expired in " + counter.ToString() + " days!",
                                MessageDate = DateTime.Now,
                                ReadOrNot = false,
                                Subject = counter.ToString() + " Days password expire notification.",
                                ToUser = UserID
                            };
                            offlineQueryRelatedMessage = null;
                            offlineQueryRelatedMessage = new DataLayer<OfflineQueryRelatedMessage>();
                            offlineQueryRelatedMessage.InsertRecord(obj);
                        }
                    }
                }
            }
            return Return;
        }
        public LoginResponseModel LoginWithOTP(Login LoginModel)
        {
            var clientid = 0;
            UserModel model = new UserModel();
            LoginResponseModel loginResponse = new LoginResponseModel();
            try
            {
                IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
                var clientInfo = data.GetDetailsWithCondition(row => row.companyURL.Contains(LoginModel.ClientURL)).FirstOrDefault();
                clientid = clientInfo.Id;

                IDataLayer<sp_MobileLoginData> Data = new DataLayer<sp_MobileLoginData>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_clientid", clientid.ToString());
                param.Add("@p_mobileno", LoginModel.UserID.ToString());
                param.Add("@p_otp", LoginModel.OTP.ToString());
                var UserData = Data.ProceduresGetData("sp_MobileLoginData", param).ToList()[0];
                if (UserData == null)
                    model.status = "Incorrect UserId and password.";
                else
                {
                    string status = LoginAttempFun(UserData.UserID);
                    if (status == "Ok")
                    {
                        model.status = "Success";
                        model.UserID = UserData.UserID;
                        model.ClientID = UserData.ClientID;
                        model.UserName = UserData.UserName;
                        model.Email = UserData.EmailAddress;
                        model.ISActive = UserData.Active;
                        model.RoleID = UserData.RoleId;
                        //model.Createddate = UserData.CreatedDate;
                        model.mobileNo = UserData.MobileNo;
                        loginResponse.Token = Authenticate(model);
                        loginResponse.UserName = model.UserName;
                        loginResponse.Status = model.status;
                    }
                    else
                    {
                        model.status = status;
                    }
                }

            }
            catch (Exception ex) { }
            loginResponse.Status = model.status;
            return loginResponse;
        }
        private string Authenticate(UserModel user)
        {
            // generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Startup.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.RoleID.ToString()),
                    new Claim(ClaimTypes.MobilePhone, user.mobileNo==null?"":user.mobileNo),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.ClientID.ToString()),
                    new Claim(ClaimTypes.UserData, user.UserID.ToString())
                }),
                //Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Token = tokenHandler.WriteToken(token);
            // remove password before returning
            Usertoken obj = new Usertoken()
            {
                ID = 0,
                Token = Token,
                UserID = user.UserID
            };
            IDataLayer<Usertoken> ut = new DataLayer<Usertoken>();
            var Response = ut.InsertRecord(obj);
            return Token;
        }
        public string checkToken(string Token)
        {
            IDataLayer<Usertoken> ut = new DataLayer<Usertoken>();
            var IsExist = ut.GetSingelDetailWithCondition(row => row.Token == Token);
            ReleaseObject<IDataLayer<Usertoken>>(ref ut);
            if (IsExist != null)
            {
                IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
                var exist = um.GetSingelDetailWithCondition(row => row.UserID == IsExist.UserID && row.Active == true);
                if (exist != null)
                    return "Ok";
                else
                    return "Not";
            }
            else
                return "Not";
        }
        public string CheckExistOrNot(Login Model)
        {
            string Response = "";
            string SMSResponse = "";
            IDataLayer<UserMaster> userExist = new DataLayer<UserMaster>();
            var data = userExist.GetDetailsWithCondition(row => row.EmailAddress.ToLower() == Model.UserID.ToLower() || row.MobileNo == Model.UserID);
            if (data.Count() <= 0)
                Response = "Not Exist";
            else
            {
                Response = "Exist";
                var checkMobile = Model.UserID.ToCharArray().Where(row => !char.IsNumber(row));
                if (checkMobile.Count() <= 0)
                {
                    //Send OTP Stuff
                    IDataLayer<MailServerOption> mail = new DataLayer<MailServerOption>();
                    var ContainMail = mail.GetSingelDetailWithCondition(row => row.MailServiceMaster.ToLower().Contains("login"));

                    var clientid = 0;
                    IDataLayer<ClientMaster> _ClientMaster = new DataLayer<ClientMaster>();
                    var clientInfo = _ClientMaster.GetDetailsWithCondition(row => row.companyURL.Contains(Model.ClientURL)).FirstOrDefault();
                    clientid = clientInfo.Id;
                    if (ContainMail != null)
                    {
                        Random rn = new Random();
                        Model.OTP = rn.Next(10000, 50000).ToString();

                        IDataLayer<SmsServer> smsServer = new DataLayer<SmsServer>();
                        var smsserverExist = smsServer.GetSingelDetailWithCondition(row => row.ClientID == clientid && row.MailServerOptionID == ContainMail.ID);
                        ReleaseObject<IDataLayer<SmsServer>>(ref smsServer);
                        if (smsserverExist != null)
                        {
                            string URL = smsserverExist.SMSAPI.Replace("[to]", Model.UserID).Replace("[message]", "OTP is : " + Model.OTP);
                            if (Common.SendSms(URL, out SMSResponse))
                            {
                                SaveOTP(clientid, Model.UserID, Model.OTP);
                            }
                        }
                        IDataLayer<MailServer> mailserver = new DataLayer<MailServer>();
                        var mailserverExist = mailserver.GetSingelDetailWithCondition(row => row.ClientID == clientid && row.MailserveroptionID == ContainMail.ID);
                        ReleaseObject<IDataLayer<MailServer>>(ref mailserver);
                        if (mailserverExist != null)
                        {
                            string CheckResponse = "";
                            var dataUser = data.ToList()[0];
                            System.Threading.Thread Th = new System.Threading.Thread(() =>
                             Common.mailMaster(mailserverExist.FromEmail, dataUser.EmailAddress, "OTP", "OTP is : " + Model.OTP,
                                 mailserverExist.FromEmail, mailserverExist.Password, mailserverExist.HostName, mailserverExist.Port,
                                 mailserverExist.UseDefaultCredential, mailserverExist.EnableSsl, ref CheckResponse));

                            Th.Start();
                            while (Th.IsAlive) { }

                            if (CheckResponse == "Success")
                            {
                                SaveOTP(clientid, Model.UserID, Model.OTP);
                            }
                        }

                    }
                }
            }
            return Response;
        }
        private string SaveOTP(int clientid, string UserID, string OTP)
        {
            string Resp = "";
            try
            {
                IDataLayer<dynamic> Data = new DataLayer<dynamic>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_clientid", clientid.ToString());
                param.Add("@p_mobileno", UserID);
                param.Add("@p_otp", OTP);
                var dataList = Data.ProcedureOutput("sp_StoreMobileRec", param);
                if (!dataList.Contains("Exception")) Resp = "Success"; else Resp = "404";
            }
            catch (Exception ex) { Resp = "Error"; }
            return Resp;
        }
        public string ResetPass(ResetPass item)
        {
            string Response = "";
            UserModel model = CommonMethods.Common.DecodeToken(item.Token);
            IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
            string pass = Common.Encrypt(item.OldPass);
            var User = um.GetSingelDetailWithCondition(row => row.UserID == model.UserID && row.ClientID == model.ClientID && row.Password == pass);
            if (User is null)
            {
                um = null;
                um = new DataLayer<UserMaster>();
                User = um.GetSingelDetailWithCondition(row => row.UserID == model.UserID && row.ClientID == model.ClientID && row.Password == item.OldPass);
            }
            if (User != null)
            {
                var UpdateUser = User;
                UpdateUser.Password = Common.Encrypt(item.NewPass);
                IDataLayer<UserMaster> updateUm = new DataLayer<UserMaster>();
                Response = updateUm.Update(UpdateUser);
                if (Response == "Data Updated Successfully.")
                {
                    Response = Reset(model.UserID);
                    if (Response != null || Response != "0")
                    {
                        Response = "Reset Password Successfully.";
                    }
                }
            }
            else
            {
                Response = "Wrong Password.";
            }
            return Response;
        }
        private string Reset(int userid)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", userid.ToString());
            var Resp = Data.ProcedureOutput("sp_ResetPassword", param);
            return Resp;
        }
        public string LogOut(CommonParam item)
        {
            UserModel Um = Common.DecodeToken(item.Token);
            IDataLayer<Usertoken> TokenData = new DataLayer<Usertoken>();
            var userToken = TokenData.GetSingelDetailWithCondition(row => row.Token == item.Token);
            TokenData = null;
            TokenData = new DataLayer<Usertoken>();
            var Response = TokenData.DeleteRecord(userToken);
            try
            {
                GC.SuppressFinalize(TokenData);
                TokenData = null;
                IDataLayer<LoginTimeHistory> logTime = new DataLayer<LoginTimeHistory>();
                var rr = logTime.GetDetailsWithCondition(row => row.UserID == Um.UserID && row.ClientID == Um.ClientID.Value).OrderByDescending(row => row.LoginTimeHis).FirstOrDefault();
                if (rr != null)
                {
                    logTime = null;
                    logTime = new DataLayer<LoginTimeHistory>();
                    rr.LogOutTimeHis = DateTime.Now;
                    logTime.Update(rr);
                }
            }
            catch (Exception ex) { }
            return Response;
        }
        public string Renewalenquiry(FindEnquiryParam Item)
        {
            string Enquiry = "";
            if (!string.IsNullOrEmpty(Item.URL) || !string.IsNullOrEmpty(Item.PolicyNo))
            {
                IDataLayer<ClientMaster> client = new DataLayer<ClientMaster>();
                var Contain = client.GetSingelDetailWithCondition(row => row.companyURL.Contains(Item.URL));
                if (Contain != null)
                {
                    IDataLayer<Motorpolicydetails> policy = new DataLayer<Motorpolicydetails>();
                    var containPolicy = policy.GetSingelDetailWithCondition(row => row.PolicyNo == Item.PolicyNo);
                    if (containPolicy != null)
                    {
                        IDataLayer<Motorenquiry> me = new DataLayer<Motorenquiry>();
                        var containMe = me.GetSingelDetailWithCondition(row => row.MotorID == containPolicy.MotorID);
                        if (containMe != null)
                        {
                            Enquiry = containMe.EnquiryNo;
                        }
                    }
                }
            }
            return Enquiry;
        }
        public string Authorization(UserAuth Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Responses> Data = new DataLayer<Responses>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UseriD", Umodel.UserID.ToString());
            param.Add("@p_Url", Item.URl);
            var Response = Data.ProceduresGetData("sp_IsAuthorize", param);
            return Response.ToList()[0].Response;
        }
        public IEnumerable<ClientMaster> GetClientData()
        {
            IDataLayer<ClientMaster> Cleint = new DataLayer<ClientMaster>();
            return Cleint.GetDetails().OrderByDescending(row => row.Id);
        }
        public string CreateNewClient(CreateCLientParam Item)
        {
            string Response = "";
            IDataLayer<ClientMaster> CM = new DataLayer<ClientMaster>();
            var Exist = CM.GetSingelDetailWithCondition(row => row.clientId == Item.clientId);
            if (Exist == null)
            {
                CM = null;
                CM = new DataLayer<ClientMaster>();
                ClientMaster clientMaster = new ClientMaster()
                {
                    clientId = Item.clientId,
                    companyName = Item.companyName,
                    companyURL = Item.companyURL,
                    contactNo = Item.contactNo,
                    CoreAPIURL = Item.CoreAPIURL,
                    Create_Date = DateTime.Now,
                    emailAddress = Item.emailAddress,
                    Id = 0
                };
                Response = CM.InsertRecord(clientMaster);
            }
            else
                Response = "Already Exist.";
            return Response;
        }
        public string CreateSuperAdmin(SuperAdmin Item)
        {
            string Response = "";
            IDataLayer<UserMaster> CM = new DataLayer<UserMaster>();
            var Exist = CM.GetSingelDetailWithCondition(row => row.ClientID == Item.ClientID && row.RoleId == 28);
            if (Exist == null)
            {
                CM = null;
                CM = new DataLayer<UserMaster>();
                UserMaster clientMaster = new UserMaster()
                {
                    RoleId = 28,
                    ClientID = Item.ClientID,
                    UserType = "SUA",
                    Active = true,
                    CreatedDate = DateTime.Now,
                    Address = Item.Address,
                    EmailAddress = Item.EmailAddress,
                    MobileNo = Item.MobileNo,
                    Password = Common.Encrypt(Item.Password),
                    UserName = Item.UserName,
                    PinCode = Item.PinCode
                };
                Response = CM.InsertRecord(clientMaster);
                if (Response == "Data Save Successfully.")
                {
                    CM = null;
                    CM = new DataLayer<UserMaster>();
                    var insertedData = CM.GetSingelDetailWithCondition(row => row.ClientID == Item.ClientID && row.EmailAddress == Item.EmailAddress);
                    IDataLayer<dynamic> Data = new DataLayer<dynamic>();
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add("@p_userid", insertedData.UserID.ToString());
                    var dataList = Data.ProcedureOutput("sp_CreateSuperadminPrivileges", param);
                }
            }
            else
                Response = "Already Exist.";
            return Response;
        }
        public string CheckSuperAdmin(CheckSuperAdminParam Item)
        {
            string Response = "";
            IDataLayer<UserMaster> Umaster = new DataLayer<UserMaster>();
            var Exist = Umaster.GetDetailsWithCondition(row => row.ClientID == Item.ClientID && row.RoleId == 28);
            if (Exist.Count() > 0)
            {
                Response = "Already Exist";
            }
            else
                Response = "Create";
            return Response;
        }
        public string ResetBeforeLoginPass(ResetPassParam Item)
        {
            string Response = "";
            try
            {
                int clientid = 0;
                if (Item.ConfirmPassReset == Item.NewPassReset)
                {
                    string message = NewPasswordCondition(Item.NewPassReset);
                    if (message == "")
                    {
                        IDataLayer<ClientMaster> data = new DataLayer<ClientMaster>();
                        var clientInfo = data.GetDetailsWithCondition(row => row.companyURL.Contains(Item.ClientURL)).FirstOrDefault();
                        clientid = clientInfo.Id;
                        IDataLayer<UserMaster> userMaster = new DataLayer<UserMaster>();
                        var oldpass = Common.Encrypt(Item.OldPassReset);
                        var Exist = userMaster.GetSingelDetailWithCondition(row =>
                                                                row.EmailAddress.ToLower() == Item.EmailID.ToLower()
                                                                && row.Password == oldpass && row.ClientID == clientid);
                        if (Exist == null)
                        {
                            Response = "Old password not matched";
                        }
                        else
                        {
                            userMaster = null;
                            Exist.Password = Common.Encrypt(Item.NewPassReset);
                            userMaster = new DataLayer<UserMaster>();
                            IDataLayer<LoginAttempt> loginAttempt = new DataLayer<LoginAttempt>();
                            var ExistLogin = loginAttempt.GetSingelDetailWithCondition(row => row.UserID == Exist.UserID);
                            if (ExistLogin != null)
                            {
                                loginAttempt = null;
                                loginAttempt = new DataLayer<LoginAttempt>();
                                loginAttempt.DeleteRecord(ExistLogin);
                                ReleaseObject<IDataLayer<LoginAttempt>>(ref loginAttempt);
                                ReleaseObject<LoginAttempt>(ref ExistLogin);
                            }
                            IDataLayer<LoginWithIncorrectAttempt> CheckIncorrectBeforeLogin = new DataLayer<LoginWithIncorrectAttempt>();
                            var LoginCounter = CheckIncorrectBeforeLogin.GetSingelDetailWithCondition(row => row.EmailId.ToLower() == Item.EmailID.ToLower() && row.ClientID == clientid);
                            if (LoginCounter != null)
                            {
                                CheckIncorrectBeforeLogin = null;
                                CheckIncorrectBeforeLogin = new DataLayer<LoginWithIncorrectAttempt>();
                                CheckIncorrectBeforeLogin.DeleteRecord(LoginCounter);
                                ReleaseObject<IDataLayer<LoginWithIncorrectAttempt>>(ref CheckIncorrectBeforeLogin);
                                ReleaseObject<LoginWithIncorrectAttempt>(ref LoginCounter);
                            }
                            Response = userMaster.Update(Exist);
                        }
                    }
                    else
                        Response = message;
                }
                else
                    Response = "Confirm password not matched.";
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Response;
        }
        private string NewPasswordCondition(string Pass)
        {
            string Response = "";
            if (Pass.Length < 8)
            {
                Response = "Password length can't be less than 8.";
            }
            var checkNumeric = Pass.ToCharArray().Where(row => char.IsNumber(row));
            if (checkNumeric.Count() <= 0)
            {
                Response += "\nMinimum one numeric character required.";

            }
            var IsnotLetterOrDigit = Pass.ToCharArray().Where(row => !char.IsLetterOrDigit(row));
            if (IsnotLetterOrDigit.Count() <= 0)
            {
                Response += "\nMinimum one special character required.";

            }
            var IsUppercase = Pass.ToCharArray().Where(row => char.IsUpper(row));
            if (IsUppercase.Count() <= 0)
            {
                Response += "\nMinimum one Upper character required.";
            }
            var IsLower = Pass.ToCharArray().Where(row => char.IsLower(row));
            if (IsLower.Count() <= 0)
            {
                Response += "\nMinimum one Lower character required.";
            }
            return Response;
        }
        #endregion  Account Controller
        #region BusinessReport Controller
        public IEnumerable<ManageOnlineOfflineMotor> MotorBusinessReport(FromDateToDate paramModal)
        {
            List<ManageOnlineOfflineMotor> manageOnlineOfflineMotor = new List<ManageOnlineOfflineMotor>();
            #region Start Fetch Motor Only
            IDataLayer<SP_BusinessReportViewDetails> Data = new DataLayer<SP_BusinessReportViewDetails>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@P_userId", paramModal.Userid.ToString());
            param.Add("@P_clientID", paramModal.ClientID.ToString());
            param.Add("@P_Fromdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            param.Add("@p_ProductType", "motor");
            var dataList = Data.ProceduresGetData("SP_BusinessReportViewDetails", param);
            manageOnlineOfflineMotor.AddRange(dataList);
            #endregion End Fetch Motor Only

            #region Start Fetch GCVPCV Only
            Data = null;
            param = null;
            Data = new DataLayer<SP_BusinessReportViewDetails>();
            param = new Dictionary<string, string>();
            param.Add("@P_userId", paramModal.Userid.ToString());
            param.Add("@P_clientID", paramModal.ClientID.ToString());
            param.Add("@P_Fromdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            param.Add("@p_ProductType", "gcvpcv");
            var dataListGCV = Data.ProceduresGetData("SP_BusinessReportViewDetails", param);
            manageOnlineOfflineMotor.AddRange(dataListGCV);
            #endregion End Fetch GCVPCV Only

            #region Start Fetch Manual Offline only
            IDataLayer<sp_ManualOfflineBusiness> Datamanual = new DataLayer<sp_ManualOfflineBusiness>();
            param = new Dictionary<string, string>();
            param.Add("@p_UserID", paramModal.Userid.ToString());
            param.Add("@p_ClientID", paramModal.ClientID.ToString());
            param.Add("@P_Fromdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            var dataListdata = Datamanual.ProceduresGetData("sp_ManualOfflineBusiness", param);
            manageOnlineOfflineMotor.AddRange(dataListdata);
            #endregion End Fetch Manual Offline only
            return manageOnlineOfflineMotor;
        }
        public IEnumerable<HealthHeaderReport> HealthHeaderBusiness(CommonParam Item)
        {
            List<HealthHeaderReport> healthHeaderReports = new List<HealthHeaderReport>();
            UserModel ItemData = Common.DecodeToken(Item.Token);
            IDataLayer<sp_HealthbusinessReportHeader> Data = new DataLayer<sp_HealthbusinessReportHeader>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", ItemData.UserID.ToString());
            param.Add("@p_clientid", ItemData.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_HealthbusinessReportHeader", param);
            healthHeaderReports.Add(dataList.FirstOrDefault());

            IDataLayer<sp_HealthManualbusinessReportHeader> Datamanual = new DataLayer<sp_HealthManualbusinessReportHeader>();
            param = new Dictionary<string, string>();
            param.Add("@p_userid", ItemData.UserID.ToString());
            param.Add("@p_clientid", ItemData.ClientID.ToString());
            var dataListManual = Datamanual.ProceduresGetData("sp_HealthManualbusinessReportHeader", param);
            if (dataListManual != null)
            {
                healthHeaderReports[0].TodayCollection += dataListManual.FirstOrDefault().TodayCollection;
                healthHeaderReports[0].TodayNoPs += dataListManual.FirstOrDefault().TodayNoPs;
                healthHeaderReports[0].TotalCollection += dataListManual.FirstOrDefault().TotalCollection;
                healthHeaderReports[0].TotalNoPs += dataListManual.FirstOrDefault().TotalNoPs;
            }
            return healthHeaderReports;
        }
        public OnlineOfflineBusinessHeaderBase MotorHeaderBusiness(MotorHeader Item)
        {
            OnlineOfflineBusinessHeaderBase obj = new OnlineOfflineBusinessHeaderBase();
            UserModel ItemData = Common.DecodeToken(Item.Token);
            IDataLayer<sp_MotorBusinessHeader> Data = new DataLayer<sp_MotorBusinessHeader>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", ItemData.ClientID.ToString());
            param.Add("@p_userid", ItemData.UserID.ToString());
            param.Add("@p_ProductType", Item.MotorType);
            var dataList = Data.ProceduresGetData("sp_MotorBusinessHeader", param);
            if (dataList != null)
                if (dataList.Count() > 0)
                    obj = dataList.ToList()[0];

            IDataLayer<sp_MotorManualBusinessHeader> DataManual = new DataLayer<sp_MotorManualBusinessHeader>();
            param = new Dictionary<string, string>();
            param.Add("@p_ClientID", ItemData.ClientID.ToString());
            param.Add("@p_userid", ItemData.UserID.ToString());
            var dataListManual = Data.ProceduresGetData("sp_MotorManualBusinessHeader", param);
            if (dataListManual != null)
            {
                if (dataListManual.Count() > 0)
                {
                    var temp = dataListManual.FirstOrDefault();
                    obj.TodayCollection += temp.TodayCollection;
                    obj.TodayNoPS += temp.TodayNoPS;
                    obj.TotalCollection += temp.TotalCollection;
                    obj.TotalNOP += temp.TotalNOP;
                }
            }
            return obj;
        }
        public IEnumerable<HealthBusiness> HealthBusinessReport(FromDateToDate paramModal)
        {
            List<HealthBusiness> healthBusinesses = new List<HealthBusiness>();
            #region Start Online HealthBusiness
            IDataLayer<sp_HealthBusinessReport> Data = new DataLayer<sp_HealthBusinessReport>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@P_userId", paramModal.Userid.ToString());
            param.Add("@P_clientID", paramModal.ClientID.ToString());
            param.Add("@P_Fromdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_HealthBusinessReport", param);
            healthBusinesses.AddRange(dataList);
            #endregion End Online HealthBusiness

            #region Start Manual Offline
            IDataLayer<sp_ManualOfflineHLTBusiness> DataOffline = new DataLayer<sp_ManualOfflineHLTBusiness>();
            param = new Dictionary<string, string>();
            param.Add("@p_UserID", paramModal.Userid.ToString());
            param.Add("@p_ClientID", paramModal.ClientID.ToString());
            param.Add("@P_Fromdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            var dataListOffline = DataOffline.ProceduresGetData("sp_ManualOfflineHLTBusiness", param);
            healthBusinesses.AddRange(dataListOffline);
            #endregion End Manual Offline
            return healthBusinesses;
        }
        public IEnumerable<vw_RegionZone> RegionZones()
        {
            throw new NotImplementedException();
        }
        public string DownloadMotorBusinessReport(BusinessReportReq param)
        {
            try
            {
                string GetTableDataRec = "";
                string Query = "";
                UserModel model = Common.DecodeToken(param.Token);
                FromDateToDate paramModal = new FromDateToDate()
                {
                    ClientID = model.ClientID.Value,
                    FromDate = param.FromDate,
                    ToDate = param.ToDate,
                    Userid = model.UserID
                };
                if (param.Product.ToLower() == "motor")
                {
                    Query = "call sp_DownloadMotorpolicyReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec = GetTableData(Query);
                    Query = "call sp_DOwnloadMotorManualOfflieReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec += GetTableData(Query, "Block");
                    Query = "call sp_DownloadComMotorpolicyReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec += GetTableData(Query, "Block");
                }
                else if (param.Product.ToLower() == "heath")
                {
                    Query = "call sp_DownloadHealthpolicyReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec = GetTableData(Query);
                    Query = "call sp_DownloadManualOfflineHLTBusiness(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec += GetTableData(Query, "Block");
                }
                return FileReturnUrl(GetTableDataRec, model.UserID, model.ClientID.Value, param.Product + "_");
                /*
                if (param.Product.ToLower() == "motor")
                    Query = "call sp_DownloadMotorpolicyReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                else if (param.Product.ToLower() == "heath")
                    Query = "call sp_DownloadHealthpolicyReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                else if (param.Product.ToLower() == "manualoffline")
                    Query = "call sp_DOwnloadMotorManualOfflieReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                else if (param.Product.ToLower() == "gcvpcv")
                    Query = "call sp_DownloadComMotorpolicyReport(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                else if (param.Product.ToLower() == "manualofflinehlt")
                    Query = "call sp_DownloadManualOfflineHLTBusiness(" + paramModal.Userid + "," + paramModal.ClientID + ",'" + paramModal.FromDate.ToString("yyyy-MM-dd") + "','" + paramModal.ToDate.ToString("yyyy-MM-dd") + "')";
                return FileReturnUrl(GetTableData(Query), model.UserID, model.ClientID.Value, param.Product + "_");
            */
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string GetTableData(string Query, string BlockHeader = null)
        {
            var data = DbHelper.ExecuteSQLQuery(Query);
            string[] Header = data.Columns.Cast<DataColumn>().
                    Select(row => row.ColumnName).ToArray();
            StringBuilder Table = new StringBuilder();
            if (BlockHeader == null)
            {
                counter = 0;
                foreach (string head in Header)
                {
                    Table.Append(head + ",");
                }
                Table.Append("\n");
            }
            foreach (DataRow dr in data.Rows)
            {
                counter += 1;
                foreach (string head in Header)
                {
                    if (head == "Sno")
                        Table.Append((counter).ToString() + ",");
                    else
                    {
                        var value = dr[head].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                        }
                        Table.Append(value.Replace("\n", "").Replace("\r", "") + ",");
                    }
                    //Table.Append(dr[head] + ",");
                }
                Table.Append("\n");
            }
            return Table.ToString();
        }
        private string FileReturnUrl(string Table, int UserID, int ClientID, string product)
        {

            string filePath = Directory.GetCurrentDirectory() + @"\Downloads\" + product + UserID + "_Policy.csv";
            File.WriteAllText(filePath, Table);
            IDataLayer<ClientMaster> Data = new DataLayer<ClientMaster>();
            var ClientUrl = Data.GetSingelDetailWithCondition(row => row.Id == ClientID);
            return ClientUrl.CoreAPIURL + "/Downloads/" + product + +UserID + "_Policy.csv";
        }
        public IEnumerable<sp_PaymentFail> PaymentFailData(CommonParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<sp_PaymentFail> Data = new DataLayer<sp_PaymentFail>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", um.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_PaymentFail", param);
            return dataList;
        }

        public string DownloadMotorPolicyPDF(MotorPolicyPDF param)
        {
            UserModel Um = Common.DecodeToken(param.Token);
            GetPDF ResponseProposal = null;
            IDataLayer<ClientMaster> clientlayer = new DataLayer<ClientMaster>();
            var Model = clientlayer.GetSingelDetailWithCondition(row => row.Id == Um.ClientID);
            param.clienturl = Model.clientURL;

            GetPDF PolicyPdf = new GetPDF();
            PolicyPdf.value = param.PolicyNo;
            PolicyPdf.getpolicyby = param.DownloadAction;
            PolicyPdf.policytype = "";
            PolicyPdf.product = param.Product == "Health" ? "healthapi" : "api";
            PolicyPdf.clienturl = Model.companyURL;

            var data = CommomMethod.GetResponse(param.clienturl + "/" + PolicyPdf.product + "/api/InsurerMotor/GetPolicyPdf", JsonConvert.SerializeObject(PolicyPdf));
            try
            {
                ResponseProposal = JsonConvert.DeserializeObject<GetPDF>(data);
            }
            catch (Exception ex)
            { }
            string Path = "";
            if (ResponseProposal != null && ResponseProposal.path != null && ResponseProposal.path != "")
            {
                Path= ResponseProposal.path;
                if (ResponseProposal.insurer == "126" || ResponseProposal.insurer == "101")
                {
                    ResponseProposal.policyno = ResponseProposal.policyno.Replace("/", "_");
                    ResponseProposal.path = param.clienturl + "/" + PolicyPdf.product + "/Policies/" + ResponseProposal.policyno + ".pdf";

                }
                else if (ResponseProposal.insurer == "147")
                {
                    ResponseProposal.policyno = ResponseProposal.policyno.Replace("/", "_");
                    ResponseProposal.path = param.clienturl + "/" + PolicyPdf.product + "/Policies/" + ResponseProposal.policyno + ".pdf";
                }
                else if (ResponseProposal.insurer == "118")
                {
                    ResponseProposal.path = ResponseProposal.path;
                }
                else if (ResponseProposal.insurer == "107")
                {
                    ResponseProposal.path = ResponseProposal.path;
                }
                else
                {
                    ResponseProposal.path = param.clienturl + "/" + PolicyPdf.product + "/Policies/" + ResponseProposal.policyno + ".pdf";
                }
            }
            Path = ResponseProposal.insurer == "118" || ResponseProposal.insurer == "107" ? ResponseProposal.path : param.clienturl + "/" + PolicyPdf.product + "/Policy/Download?policyno=" + Common.Encrypt(ResponseProposal.policyno); //param.clienturl + "/" + PolicyPdf.product + "/Policies?policyno=" + Common.Encrypt(ResponseProposal.policyno);
            return Path;//ResponseProposal.path;
        }
        public IEnumerable<vw_Missingpolicyreport> MissingPolicies(CommonParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<vw_Missingpolicyreport> policy = new DataLayer<vw_Missingpolicyreport>();
            var data = policy.GetDetailsWithCondition(row => row.ClientID == um.ClientID);
            return data;
        }
        public string SaveMissingPolicies(MissingPolicyParam item)
        {
            UserModel Um = Common.DecodeToken(item.Token);
            IDataLayer<MissingPolicyReport> report = new DataLayer<MissingPolicyReport>();
            MissingPolicyReport obj = new MissingPolicyReport()
            {
                ChasisNo = item.ChasisNo,
                ClientID = Um.ClientID.Value,
                EnquiryStatus = item.EnquiryStatus,
                Insurer = item.Insurer.Value,
                PolicyNo = item.PolicyNo,
                VehicleNo = item.VehicleNo,
                ID = 0
            };
            return report.InsertRecord(obj);
        }
        public IEnumerable<sp_GetProcessPaymentGetway> GetProcessPaymentGetway(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_GetProcessPaymentGetway> Data = new DataLayer<sp_GetProcessPaymentGetway>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
            var dataList = Data.ProceduresGetData("sp_GetProcessPaymentGetway", param);
            return dataList;
        }
        public IEnumerable<sp_GetProcessHLTPaymentGetway> GetProcessHLTPaymentGetway(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_GetProcessHLTPaymentGetway> Data = new DataLayer<sp_GetProcessHLTPaymentGetway>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
            var dataList = Data.ProceduresGetData("sp_GetProcessHLTPaymentGetway", param);
            return dataList;
        }
        public string OfflineUpdatePolicy(OfflineUpdatePolicy Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_MotorID", Item.MotorID.ToString());
            param.Add("@p_UserID", Item.UserID.ToString());
            param.Add("@p_MotorType", Item.MotorType.ToString());
            param.Add("@p_PolicyType", Item.PolicyType.ToString());
            param.Add("@p_BasicOD", Item.BasicOD.ToString());
            param.Add("@p_BasicTP", Item.BasicTP.ToString());
            param.Add("@p_GrossPremium", Item.GrossPremium.ToString());
            param.Add("@p_NetPremium", Item.NetPremium.ToString());
            param.Add("@p_TotalPremium", Item.TotalPremium.ToString());
            param.Add("@p_ServiceTax", Item.ServiceTax.ToString());
            param.Add("@p_PolicyNo", Item.PolicyNo.ToString());
            param.Add("@p_EngineNo", Item.EngineNo.ToString());
            param.Add("@p_ChesisNo", Item.ChesisNo.ToString());
            param.Add("@p_VehicleNo", Item.VehicleNo.ToString());
            param.Add("@p_IDV", Item.IDV.ToString());
            var dataList = Data.ProcedureOutput("sp_SuccessOfflinePaymentGetway", param);
            return dataList;
        }
        public string ManualUploadOflineBusiness(ManualOfflineParam Item)
        {
            string Response = "";
            if (Item.CustomerEmail == null && Item.CustomerMobile == null)
            {
                Response = "Customer Email or Customer Mobile no required.";
                return Response;
            }
            if (Item.CustomerMobile != null)
            {
                if (Item.CustomerMobile.Length > 10 || Item.CustomerMobile.Length < 10)
                {
                    Response = "Mobile number is not valid.";
                    return Response;
                }
            }
            if (Item.CustomerEmail != null)
            {
                if (!Item.CustomerEmail.Contains("@"))
                {
                    Response = "Email is not valid.";
                    return Response;
                }
            }
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Manualofflinepolicy> manualofflinepolicy = new DataLayer<Manualofflinepolicy>();
            var Exist = manualofflinepolicy.GetSingelDetailWithCondition(row => row.PolicyNo == Item.PolicyNo && row.ClientID == Umodel.ClientID);
            manualofflinepolicy = null;
            manualofflinepolicy = new DataLayer<Manualofflinepolicy>();
            if (Exist == null)
            {
                Manualofflinepolicy tblObj = new Manualofflinepolicy()
                {
                    BasicOD = Item.BasicOD,
                    BasicTP = Item.BasicTP,
                    ChesisNo = Item.ChesisNo,
                    ClientID = Umodel.ClientID.Value,
                    CreatedID = Umodel.UserID,
                    UserID = Item.UserID,
                    EngineNo = Item.EngineNo,
                    Entrydate = DateTime.Now,
                    FilePath = "",
                    GrossPremium = Item.GrossPremium,
                    ID = 0,
                    IDV = Item.IDV,
                    MotorType = Item.MotorType,
                    NetPremium = Item.NetPremium,
                    PolicyNo = Item.PolicyNo,
                    PolicyType = Item.PolicyType,
                    ServiceTax = Item.ServiceTax,
                    TotalPremium = Item.TotalPremium,
                    VehicleNo = Item.VehicleNo,
                    BusinessType = Item.BusinessType,
                    ChecqueBank = Item.ChecqueBank,
                    ChecqueDate = Item.ChecqueDate,
                    ChecqueNo = Item.ChecqueNo,
                    CustomerName = Item.CustomerName,
                    Fuel = Item.Fuel,
                    Insurer = Item.Insurer,
                    Make = Item.Make,
                    ManufacturingMonth = Item.ManufacturingMonth,
                    ManufacturingYear = Item.ManufacturingYear,
                    NCB = Item.NCB,
                    PolicyEndDate = Item.PolicyEndDate,
                    PolicyIssuedate = Item.PolicyIssuedate,
                    PolicyStartDate = Item.PolicyStartDate,
                    Variant = Item.Variant,
                    Vehicle = Item.Vehicle,
                    CustomerEmail = Item.CustomerEmail,
                    CustomerMobile = Item.CustomerMobile,
                    CPA = Item.CPA,
                    CubicCapicity = Item.CubicCapicity,
                    CustomerAddress = Item.CustomerAddress,
                    CustomerCityID = Item.CustomerCityID,
                    CustomerDOB = Item.CustomerDOB,
                    CustomerFax = Item.CustomerFax,
                    CustomerPANNo = Item.CustomerPANNo,
                    CustomerPinCode = Item.CustomerPin,
                    GrossDiscount = Item.GrossDiscount,
                    NillDep = Item.NillDep,
                    PreviousNCB = Item.PreviousNCB,
                    PrevPolicyNO = Item.PrevPolicyNO,
                    RTOID = Item.SelectRTO,
                    Period = Item.Period,
                    InsuranceType = Item.InsuranceType,
                    IsPospProduct = Item.IsPospProduct,
                    AddOnPremium = Item.AddOnPremium,
                    GVW = Item.GVW,
                    SeatingCapacity = Item.SeatingCapacity
                    //sea_cap = Item.SeatingCapacity
                };
                Response = manualofflinepolicy.InsertRecord(tblObj);
            }
            else
            {
                manualofflinepolicy = null;
                manualofflinepolicy = new DataLayer<Manualofflinepolicy>();
                Exist.CreatedID = Umodel.UserID;
                Exist.UserID = Item.UserID;
                Exist.TotalPremium = Item.TotalPremium;
                Exist.BasicOD = Item.BasicOD;
                Exist.BasicTP = Item.BasicTP;
                Exist.ChesisNo = Item.ChesisNo;
                Exist.ClientID = Umodel.ClientID.Value;
                Exist.CreatedID = Umodel.UserID;
                Exist.UserID = Item.UserID;
                Exist.EngineNo = Item.EngineNo;
                Exist.Entrydate = DateTime.Now;
                Exist.FilePath = "";
                Exist.GrossPremium = Item.GrossPremium;
                Exist.IDV = Item.IDV;
                Exist.MotorType = Item.MotorType;
                Exist.NetPremium = Item.NetPremium;
                Exist.PolicyType = Item.PolicyType;
                Exist.ServiceTax = Item.ServiceTax;
                Exist.TotalPremium = Item.TotalPremium;
                Exist.VehicleNo = Item.VehicleNo;
                Exist.BusinessType = Item.BusinessType;
                Exist.ChecqueBank = Item.ChecqueBank;
                Exist.ChecqueDate = Item.ChecqueDate;
                Exist.ChecqueNo = Item.ChecqueNo;
                Exist.CustomerName = Item.CustomerName;
                Exist.Fuel = Item.Fuel;
                Exist.Insurer = Item.Insurer;
                Exist.Make = Item.Make;
                Exist.ManufacturingMonth = Item.ManufacturingMonth;
                Exist.ManufacturingYear = Item.ManufacturingYear;
                Exist.NCB = Item.NCB;
                Exist.PolicyEndDate = Item.PolicyEndDate;
                Exist.PolicyIssuedate = Item.PolicyIssuedate;
                Exist.PolicyStartDate = Item.PolicyStartDate;
                Exist.Variant = Item.Variant;
                Exist.Vehicle = Item.Vehicle;
                Exist.CustomerEmail = Item.CustomerEmail;
                Exist.CustomerMobile = Item.CustomerMobile;
                Exist.CPA = Item.CPA;
                Exist.CubicCapicity = Item.CubicCapicity;
                Exist.CustomerAddress = Item.CustomerAddress;
                Exist.CustomerCityID = Item.CustomerCityID;
                Exist.CustomerDOB = Item.CustomerDOB;
                Exist.CustomerFax = Item.CustomerFax;
                Exist.CustomerPANNo = Item.CustomerPANNo;
                Exist.CustomerPinCode = Item.CustomerPin;
                Exist.GrossDiscount = Item.GrossDiscount;
                Exist.NillDep = Item.NillDep;
                Exist.PreviousNCB = Item.PreviousNCB;
                Exist.PrevPolicyNO = Item.PrevPolicyNO;
                Exist.RTOID = Item.SelectRTO;
                Exist.Period = Item.Period;
                Exist.InsuranceType = Item.InsuranceType;
                Exist.IsPospProduct = Item.IsPospProduct;
                Exist.AddOnPremium = Item.AddOnPremium;
                Response = manualofflinepolicy.Update(Exist);
            }
            return Response;
        }
        public string OfflineUpdateHLTPolicy(OffLineUpdateHLTPolicyParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", Item.Getway.Userid.ToString());
            param.Add("@p_EnquiryNo", Item.Getway.EnquiryNo);
            param.Add("@p_CompanyID", Item.Getway.SelectedIsurer.ToString());
            param.Add("@p_TotalPremium", Item.TotalPremium.ToString());
            param.Add("@p_PolicyNo", Item.Policyno);
            param.Add("@p_PlanName", Item.PlanName);
            param.Add("@p_CoverAmount", Item.CoverAmount.ToString());
            param.Add("@p_BasePremium", Item.BasePremium.ToString());
            param.Add("@p_Term", Item.Term.ToString());
            var dataList = Data.ProcedureOutput("sp_SuccessOfflineHLTPaymentGetway", param);
            return dataList;
        }
        public string LifePolicy(LifePolicyParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EnquiryNo", Item.EnquiryNo);
            param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
            param.Add("@p_AddressLine1", Item.AddressLine1);
            param.Add("@p_AddressLine2", Item.AddressLine2);
            param.Add("@p_AddressLine3", Item.AddressLine3);
            param.Add("@p_Cityid", Item.Cityid.ToString());
            param.Add("@p_StateID", Item.StateID.ToString());
            param.Add("@p_PinCode", Item.PinCode.ToString());
            param.Add("@p_ProposalNo", Item.ProposalNo);
            param.Add("@p_CompanyID", Item.CompanyID.ToString());
            param.Add("@p_YourAge", Item.YourAge.ToString());
            param.Add("@p_Gender", Item.Gender.ToString());
            param.Add("@p_SmokeStaus", Item.SmokeStaus.ToString());
            param.Add("@p_AnnualInCome", Item.AnnualInCome.ToString());
            param.Add("@p_PreferredCover", Item.PreferredCover.ToString());
            param.Add("@p_CoverageAge", Item.CoverageAge.ToString());
            param.Add("@p_PaymentID", Item.PaymentID.ToString());
            param.Add("@p_SumAsured", Item.SumAsured.ToString());
            param.Add("@p_BasePremium", Item.BasePremium.ToString());
            param.Add("@p_GrossPremium", Item.GrossPremium.ToString());
            param.Add("@p_TaxAmount", Item.TaxAmount.ToString());
            param.Add("@p_TotalPremium", Item.TotalPremium.ToString());
            param.Add("@p_Discount", Item.Discount.ToString());
            param.Add("@p_DiscountPercentage", Item.DiscountPercentage.ToString());
            param.Add("@p_PolicyTerm", Item.PolicyTerm.ToString());
            param.Add("@p_PolicyNo", Item.PolicyNo.ToString());
            param.Add("@p_PolicyRemark", Item.PolicyRemark.ToString());
            var dataList = Data.ProcedureOutput("sp_LifePolicy", param);
            if (!dataList.Contains("Exception"))
            {
                if (Convert.ToInt32(dataList) > 0)
                {
                    Response = "Data Updated.";
                }
            }
            else
            {
                Response = "Data Not Found.";
            }
            return Response;
        }
        public IEnumerable<Enquiry> FilterLifeEnquiryWithNumber(FilterEnquiry Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Enquiry> objData = new DataLayer<Enquiry>();
            var data = objData.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID.Value && row.MobileNo == Item.MobileNumber && row.EnquiryType == "Lif");
            return data;
        }
        public IEnumerable<sp_Consolidate> GetConsolidate(ConsolidateParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_Consolidate> Data = new DataLayer<sp_Consolidate>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", Umodel.UserID.ToString());
            param.Add("@p_clientid", Umodel.ClientID.Value.ToString());
            param.Add("@p_from", Item.From.ToString("yyyy-MM-dd"));
            param.Add("@p_to", Item.To.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_Consolidate", param);
            return dataList;
        }
        public userprivilegerolemapping CheckDownloadOption(CommonParam item)
        {
            UserModel umodel = Common.DecodeToken(item.Token);
            IDataLayer<userprivilegerolemapping> dataLayer = new DataLayer<userprivilegerolemapping>();
            var check = dataLayer.GetSingelDetailWithCondition(row => row.UserID == umodel.UserID && row.PrivilegeID == 35);
            return check;
        }
        public IEnumerable<sp_CreateByListOfflineBusines> OfflineUserList(BaseParam item)
        {
            IDataLayer<sp_CreateByListOfflineBusines> Data = new DataLayer<sp_CreateByListOfflineBusines>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", item.UserID.ToString());
            param.Add("@p_clientid", item.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_CreateByListOfflineBusines", param);

            return dataList;
        }
        public IEnumerable<sp_ManualOfflineBusiness> ManualOfflineBusinessReport(FromDateToDate paramModal)
        {
            IDataLayer<sp_ManualOfflineBusiness> Data = new DataLayer<sp_ManualOfflineBusiness>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@P_userId", paramModal.Userid.ToString());
            param.Add("@P_clientID", paramModal.ClientID.ToString());
            param.Add("@P_Fromdate", paramModal.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", paramModal.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_ManualOfflineBusiness", param);
            return dataList;
        }
        public IEnumerable<sp_YearConsolidate> GetYearWiseCosolidate(YearConsolidateParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<sp_YearConsolidate> Data = new DataLayer<sp_YearConsolidate>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_userid", um.UserID.ToString());
            param.Add("@p_clientid", um.ClientID.ToString());
            param.Add("@p_year", Item.Year);
            var dataList = Data.ProceduresGetData("sp_YearConsolidate", param);
            return dataList;
        }
        public IEnumerable<sp_ShowAboutConsolidate> ShowAboutConsolidate(ShowAboutConsolidateParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<sp_ShowAboutConsolidate> Data = new DataLayer<sp_ShowAboutConsolidate>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_clientid", um.ClientID.ToString());
            param.Add("@p_Region", Item.Region);
            param.Add("@p_Enquirydate", Item.Date);
            var dataList = Data.ProceduresGetData("sp_ShowAboutConsolidate", param);
            return dataList;
        }
        public IEnumerable<sp_UserProgresive> UserProgresive(ConsolidateParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_UserProgresive> Data = new DataLayer<sp_UserProgresive>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", umodel.UserID.ToString());
            param.Add("@p_ClientID", umodel.ClientID.ToString());
            param.Add("@p_From", Item.From.ToString("yyyy-MM-dd"));
            param.Add("@p_To", Item.To.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_UserProgresive", param);
            return dataList;
        }
        public string OfflineHealthPolicy(OfflineHealthPolicyParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ManualOfflineHealthPolicy> ManualOfflineHealthPolicy = new DataLayer<ManualOfflineHealthPolicy>();
            var Exist = ManualOfflineHealthPolicy.GetSingelDetailWithCondition(row => row.PolicyNo == Item.ManualPolicyNo && row.ClientID == umodel.ClientID.Value);
            ManualOfflineHealthPolicy = null;
            ManualOfflineHealthPolicy = new DataLayer<ManualOfflineHealthPolicy>();
            if (Exist == null)
            {
                Response = ManualOfflineHealthPolicy.InsertRecord(new ManualOfflineHealthPolicy()
                {
                    AdultCount = Item.ManualAdultCount,
                    BasePremium = Item.ManualBasePremium,
                    ChecqueBank = Item.ManualChecqueBank,
                    ChecqueDate = Item.ManualChecqueDate,
                    ChecqueNo = Item.ManualChecqueNo,
                    ChildCount = Item.ManualChildCount,
                    ClientID = umodel.ClientID.Value,
                    CoverAmount = Item.ManualCoverAmount,
                    CreatedDate = DateTime.Now,
                    CreatedID = umodel.UserID,
                    CustomerEmail = Item.ManualCustomerEmail,
                    UserID = Item.ManualselectUser,
                    CustomerMobile = Item.ManualCustomerMobile,
                    CustomerName = Item.ManualCustomerName,
                    ID = 0,
                    InsurerID = Item.ManualInsurer,
                    PlanName = Item.ManualPlanName,
                    PolicyNo = Item.ManualPolicyNo,
                    Policytype = Item.ManualPolicytype,
                    ServiceTax = Item.ManualServiceTax,
                    TotalPremium = Item.ManualTotalPremium,
                    CustomerAddress = Item.CustomerAddress,
                    CustomerDOB = Item.CustomerDOB,
                    EndDate = Item.EndDate,
                    InsuranceType = Item.InsuranceType,
                    IsPospProduct = Item.IsPospProduct,
                    Pincode = Item.Pincode,
                    PolicyIssueDate = Item.PolicyIssueDate,
                    ProductName = Item.ProductName,
                    ProductType = Item.ProductType,
                    SelectBusinessType = Item.SelectBusinessType,
                    SelectCity = Item.SelectCity,
                    SelectPolicyTerm = Item.SelectPolicyTerm,
                    SelectState = Item.SelectState,
                    StartDate = Item.StartDate
                });
            }
            else
            {
                Exist.PlanName = Item.ManualPlanName;
                Exist.AdultCount = Item.ManualAdultCount;
                Exist.ChildCount = Item.ManualChildCount;
                Exist.TotalPremium = Item.ManualTotalPremium;
                Exist.ServiceTax = Item.ManualServiceTax;
                Exist.BasePremium = Item.ManualBasePremium;
                Exist.ChecqueBank = Item.ManualChecqueBank;
                Exist.ChecqueDate = Item.ManualChecqueDate;
                Exist.ChecqueNo = Item.ManualChecqueNo;
                Exist.CoverAmount = Item.ManualCoverAmount;
                Exist.CustomerEmail = Item.ManualCustomerEmail;
                Exist.CustomerMobile = Item.ManualCustomerMobile;
                Exist.CustomerName = Item.ManualCustomerName;
                Exist.InsurerID = Item.ManualInsurer;
                Exist.PolicyNo = Item.ManualPolicyNo;
                Exist.Policytype = Item.ManualPolicytype;
                Exist.CustomerAddress = Item.CustomerAddress;
                Exist.CustomerDOB = Item.CustomerDOB;
                Exist.EndDate = Item.EndDate;
                Exist.InsuranceType = Item.InsuranceType;
                Exist.IsPospProduct = Item.IsPospProduct;
                Exist.Pincode = Item.Pincode;
                Exist.PolicyIssueDate = Item.PolicyIssueDate;
                Exist.ProductName = Item.ProductName;
                Exist.ProductType = Item.ProductType;
                Exist.SelectBusinessType = Item.SelectBusinessType;
                Exist.SelectCity = Item.SelectCity;
                Exist.SelectPolicyTerm = Item.SelectPolicyTerm;
                Exist.SelectState = Item.SelectState;
                Exist.StartDate = Item.StartDate;
                Response = ManualOfflineHealthPolicy.Update(Exist);
            }
            return Response;
        }
        public HeaderFooter GetCertificateHeader(CommonParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ClientMaster> cm = new DataLayer<ClientMaster>();
            var Exist = cm.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID);
            HeaderFooter model = new HeaderFooter()
            {
                Footer = Exist.CoreAPIURL + "/downloads/Certificate/Header/" + Exist.Id.ToString() + ".jpg",
                Header = Exist.CoreAPIURL + "/downloads/Certificate/Footer/" + Exist.Id.ToString() + ".jpg"
            };
            return model;
        }
        public IEnumerable<sp_ManualOfflineHLTBusiness> OfflineHealthHLTPolicy(BusinessReportReq Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_ManualOfflineHLTBusiness> Data = new DataLayer<sp_ManualOfflineHLTBusiness>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", umodel.UserID.ToString());
            param.Add("@p_ClientID", umodel.ClientID.ToString());
            param.Add("@P_Fromdate", Item.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", Item.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_ManualOfflineHLTBusiness", param);
            return dataList;
        }
        public string UploadBulkManualOfflineMotorBusiness(BulkOfflineManualMotorParam item)
        {
            string Response = "";
            StringBuilder ResponseData = new StringBuilder();
            UserModel umodel = Common.DecodeToken(item.Token);
            IDataLayer<ClientMaster> client = new DataLayer<ClientMaster>();
            var Clientinfo = client.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID);
            List<dynamic> PrivateCarMakeList = null;
            List<dynamic> TwoWheelerMakeList = null;
            int userID = 0;
            foreach (var blkfile in item.bulkMotorBusinessList)
            {
                userID = 0;
                IDataLayer<UserMaster> umaster = new DataLayer<UserMaster>();
                var CheckUser = umaster.GetSingelDetailWithCondition(row => row.EmailAddress == blkfile.UserEmail);
                if (CheckUser != null)
                {
                    userID = CheckUser.UserID;
                }
                switch (blkfile.MotorType)
                {
                    case "PrivateCar":
                        if (PrivateCarMakeList == null)
                        {
                            var PrivateCarMakeListstr = CommomMethod.Get(Clientinfo.companyURL + "/centralapi/api/Vehicle/GetVehiclesByType?type=Car");
                            PrivateCarMakeList = JsonConvert.DeserializeObject<List<dynamic>>(PrivateCarMakeListstr);
                        }
                        Response = SaveMotorManual(PrivateCarMakeList, Clientinfo.companyURL, blkfile.Make, blkfile.Fuel, blkfile.Variant, blkfile, umodel, userID);
                        break;
                    case "TwoWheeler":
                        if (TwoWheelerMakeList == null)
                        {
                            var TwoWheelerMakeListstr = CommomMethod.Get(Clientinfo.companyURL + "/centralapi/api/Vehicle/GetVehiclesByType?type=Two Wheeler");
                            TwoWheelerMakeList = JsonConvert.DeserializeObject<List<dynamic>>(TwoWheelerMakeListstr);
                        }
                        Response = SaveMotorManual(TwoWheelerMakeList, Clientinfo.companyURL, blkfile.Make, blkfile.Fuel, blkfile.Variant, blkfile, umodel, userID);
                        break;
                }
                ResponseData.Append(blkfile.PolicyNo + " :" + Response + "\n");
                Thread.Sleep(100);
            }
            return ResponseData.ToString();
        }
        private string SaveMotorManual(List<dynamic> item, string Domain, string Make, string Fuel,
            string VariantName, BulkMotorBusinessList Bulkitem, UserModel Umodel, int UserID)
        {
            string Response = "";
            int manufactureID = 0;
            List<dynamic> FuelList = null;
            List<dynamic> VariantList = null;
            try
            {
                var obj = item.Where(row => row.Manu_Vehicle.Value.Trim().ToLower() == Make.Trim().ToLower()).FirstOrDefault();
                if (obj != null)
                {
                    manufactureID = obj.ManufacturerID;
                    var FuelListstr = CommomMethod.Get(Domain + "/centralapi/api/Vehicle/GetFuelByVehicleID/" + obj.VehicleID);
                    FuelList = JsonConvert.DeserializeObject<List<dynamic>>(FuelListstr);
                    var fuelObj = FuelList.Where(row => row.FuelName == Fuel).FirstOrDefault();
                    if (fuelObj != null)
                    {
                        var VariantListstr = CommomMethod.Get(Domain + "/centralapi/api/Vehicle/GetVariantsByVehicleAndFuel?VehicleID=" + obj.VehicleID + "&FuelID=" + fuelObj.FuelID);
                        VariantList = JsonConvert.DeserializeObject<List<dynamic>>(VariantListstr);
                        var Variantobj = VariantList.Where(row => row.VariantName.Value.Replace("  ", " ").Trim().ToLower() == VariantName.Trim().ToLower()).FirstOrDefault();
                        if (Variantobj != null)
                        {
                            IDataLayer<Companies> companies = new DataLayer<Companies>();
                            var Comp = companies.GetSingelDetailWithCondition(row => row.CompanyName.ToLower() == Bulkitem.Insurer.ToLower());
                            IDataLayer<Rtos> rtodata = new DataLayer<Rtos>();
                            var RTOData = rtodata.GetSingelDetailWithCondition(row => row.RTOCode.Trim().ToLower().Replace("  ", " ") == Bulkitem.RTOCode.Trim().ToLower().Replace("  ", " "));
                            if (RTOData != null)
                            {
                                IDataLayer<Cities> cityLayer = new DataLayer<Cities>();
                                var cityData = cityLayer.GetSingelDetailWithCondition(row => row.CityName.ToLower().Trim() == Bulkitem.City.ToLower().Trim());
                                if (cityData != null)
                                {
                                    IDataLayer<Manualofflinepolicy> manualofflinepolicy = new DataLayer<Manualofflinepolicy>();
                                    var tblModel = new Manualofflinepolicy()
                                    {
                                        BasicOD = Convert.ToDecimal(Bulkitem.BasicOD),
                                        BasicTP = Convert.ToDecimal(Bulkitem.BasicTP),
                                        BusinessType = Bulkitem.BusinessType,
                                        ChecqueBank = Bulkitem.ChecqueBank,
                                        ChecqueDate = Convert.ToDateTime(Bulkitem.ChecqueDate),
                                        ChecqueNo = Bulkitem.ChecqueNo,
                                        ChesisNo = Bulkitem.ChesisNo,
                                        ClientID = Umodel.ClientID,
                                        CreatedID = Umodel.UserID,
                                        UserID = UserID,
                                        CustomerEmail = Bulkitem.CustomerEmail,
                                        CustomerMobile = Bulkitem.CustomerMobile,
                                        CustomerName = Bulkitem.CustomerName,
                                        EngineNo = Bulkitem.EngineNo,
                                        Entrydate = DateTime.Now,
                                        FilePath = "",
                                        Fuel = fuelObj.FuelID,
                                        ID = 0,
                                        IDV = Convert.ToDecimal(Bulkitem.IDV),
                                        Insurer = Comp.CompanyID,
                                        Make = obj.ManufacturerID,
                                        ManufacturingMonth = Convert.ToInt32(Bulkitem.ManufacturingMonth),
                                        ManufacturingYear = Convert.ToInt32(Bulkitem.ManufacturingYear),
                                        MotorType = Bulkitem.MotorType,
                                        NCB = Convert.ToInt32(Bulkitem.NCB),
                                        NetPremium = Convert.ToInt32(Bulkitem.NetPremium),
                                        PolicyEndDate = Convert.ToDateTime(Bulkitem.PolicyEndDate),
                                        PolicyIssuedate = Convert.ToDateTime(Bulkitem.PolicyIssuedate),
                                        PolicyNo = Bulkitem.PolicyNo,
                                        PolicyStartDate = Convert.ToDateTime(Bulkitem.PolicyStartDate),
                                        PolicyType = Bulkitem.PolicyType,
                                        ServiceTax = Convert.ToDecimal(Bulkitem.ServiceTax),
                                        TotalPremium = Convert.ToDecimal(Bulkitem.TotalPremium),
                                        Variant = Variantobj.VariantID,
                                        Vehicle = obj.VehicleID,
                                        VehicleNo = Bulkitem.VehicleNo,
                                        AddOnPremium = Bulkitem.AddOnPremium == null ? 0 : Convert.ToDecimal(Bulkitem.AddOnPremium),
                                        CPA = Bulkitem.CPA == null ? 0 : Convert.ToDecimal(Bulkitem.CPA),
                                        CubicCapicity = Bulkitem.CubicCapacity,
                                        CustomerAddress = Bulkitem.CustomerAddress,
                                        CustomerCityID = cityData.CityID,
                                        CustomerDOB = Convert.ToDateTime(Bulkitem.CustomerDOB),
                                        CustomerFax = Bulkitem.FaxNo,
                                        CustomerPANNo = Bulkitem.PanNo,
                                        CustomerPinCode = Bulkitem.PinCode,
                                        GrossDiscount = Bulkitem.GrossDiscount == null ? 0 : Convert.ToDecimal(Bulkitem.GrossDiscount),
                                        GrossPremium = Bulkitem.GrossPremium == null ? 0 : Convert.ToDecimal(Bulkitem.GrossPremium),
                                        InsuranceType = Bulkitem.InsuranceType,
                                        IsPospProduct = Bulkitem.IsPOSpProduct.ToLower() == "yes" ? true : false,
                                        NillDep = Bulkitem.NillDep.ToLower() == "yes" ? true : false,
                                        Period = Convert.ToInt32(Bulkitem.Period),
                                        PreviousNCB = Convert.ToInt32(Bulkitem.PreviosNCB),
                                        PrevPolicyNO = Bulkitem.PreviosPolicyNo,
                                        RTOID = RTOData.RTOID
                                    };
                                    Response = manualofflinepolicy.InsertRecord(tblModel);
                                }
                                else
                                    Response = "City not found.";
                            }
                            else
                                Response = "RTO not found.";
                        }
                        else
                        {
                            Response = "Variant not found.";
                        }
                    }
                    else
                    {
                        Response = "Fuel Not Found.";
                    }
                }
                else
                {
                    Response = "Make not Found.";
                }
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Response;
        }

        public string UploadBulkManualOfflineHLTBusiness(BulkOfflineManualHLTParam item)
        {
            string Response = "";
            StringBuilder ResponseData = new StringBuilder();
            UserModel umodel = Common.DecodeToken(item.Token);
            IDataLayer<ClientMaster> client = new DataLayer<ClientMaster>();
            var Clientinfo = client.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID);
            int userID = 0;
            IDataLayer<ManualOfflineHealthPolicy> manualOfflineHealthPolicy = null;
            foreach (var blkfile in item.bulkHLTBusinessList)
            {
                IDataLayer<UserMaster> umaster = new DataLayer<UserMaster>();
                var CheckUser = umaster.GetSingelDetailWithCondition(row => row.EmailAddress == blkfile.UserEmail);
                if (CheckUser != null)
                    userID = CheckUser.UserID;
                IDataLayer<Companies> companies = new DataLayer<Companies>();
                var Comp = companies.GetSingelDetailWithCondition(row => row.CompanyName.ToLower() == blkfile.InsurerName.ToLower());
                manualOfflineHealthPolicy = new DataLayer<ManualOfflineHealthPolicy>();
                IDataLayer<Cities> cityLayer = new DataLayer<Cities>();
                var cityData = cityLayer.GetSingelDetailWithCondition(row => row.CityName.ToLower().Trim() == blkfile.City.ToLower().Trim());
                if (cityData != null)
                {
                    try
                    {
                        Response = manualOfflineHealthPolicy.InsertRecord(new ManualOfflineHealthPolicy()
                        {
                            AdultCount = Convert.ToInt32(blkfile.AdultCount),
                            BasePremium = Convert.ToDecimal(blkfile.BasePremium),
                            ChecqueBank = blkfile.ChecqueBank,
                            ChecqueDate = Convert.ToDateTime(blkfile.ChecqueDate),
                            ChecqueNo = blkfile.ChecqueNo,
                            ChildCount = Convert.ToInt32(blkfile.ChildCount),
                            ClientID = umodel.ClientID.Value,
                            CoverAmount = Convert.ToDecimal(blkfile.CoverAmount),
                            CreatedDate = DateTime.Now,
                            CreatedID = umodel.UserID,
                            UserID = userID,
                            CustomerEmail = blkfile.CustomerEmail,
                            CustomerMobile = blkfile.CustomerMobile,
                            CustomerName = blkfile.CustomerName,
                            InsurerID = Comp.CompanyID,
                            PlanName = blkfile.PlanName,
                            PolicyNo = blkfile.PolicyNo,
                            Policytype = blkfile.Policytype,
                            ServiceTax = Convert.ToDecimal(blkfile.ServiceTax),
                            TotalPremium = Convert.ToDecimal(blkfile.TotalPremium),
                            ID = 0,
                            CustomerAddress = blkfile.Address,
                            CustomerDOB = Convert.ToDateTime(blkfile.CustomerDOB),
                            EndDate = Convert.ToDateTime(blkfile.EndDate),
                            InsuranceType = blkfile.BusinessType,
                            IsPospProduct = blkfile.IsPosProduct.ToLower().Trim() == "yes" ? true : false,
                            Pincode = blkfile.Pincode,
                            PolicyIssueDate = Convert.ToDateTime(blkfile.PolicyIssueDate),
                            ProductName = blkfile.ProductName,
                            ProductType = blkfile.ProductType,
                            SelectBusinessType = blkfile.Product,
                            SelectCity = cityData.CityID,
                            SelectState = cityData.StateID,
                            SelectPolicyTerm = Convert.ToInt32(blkfile.Term),
                            StartDate = Convert.ToDateTime(blkfile.StartDate)
                        });
                    }
                    catch (Exception ex)
                    {
                        Response = ex.Message;
                    }
                }
                else
                    Response = "City Not Found.";
                ResponseData.Append(blkfile.PolicyNo + " :" + Response + "\n");
                Thread.Sleep(100);
            }
            return ResponseData.ToString();
        }
        public IEnumerable<States> GetStateList(CommonParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            if (umodel != null)
            {
                IDataLayer<States> stateList = new DataLayer<States>();
                var lstStates = stateList.GetDetailsWithCondition(row => row.IsActive == true);
                return lstStates;
            }
            return null;
        }
        public Cities GetStateThroughCityID(GetStateParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            if (umodel != null)
            {
                IDataLayer<Cities> stateList = new DataLayer<Cities>();
                var lstCities = stateList.GetSingelDetailWithCondition(row => row.CityID == Item.CityID);
                return lstCities;
            }
            return null;
        }
        public IEnumerable<Cities> GetCityList(GetCitiesParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            if (umodel != null)
            {
                IDataLayer<Cities> citiesList = new DataLayer<Cities>();
                var lstcities = citiesList.GetDetailsWithCondition(row => row.StateID == Item.StateID && row.IsActive == true);
                return lstcities;
            }
            return null;
        }
        public IEnumerable<Rtos> GetRTOList(CommonParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            if (umodel != null)
            {
                IDataLayer<Rtos> rtosList = new DataLayer<Rtos>();
                var lstrtos = rtosList.GetDetailsWithCondition(row => row.IsActive == true);
                return lstrtos;
            }
            return null;
        }
        public string DeleteOfflinePolicy(OfflinePolicy Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            if (Item.Product.ToLower() == "mot")
            {
                IDataLayer<Manualofflinepolicy> manualofflinepolicy = new DataLayer<Manualofflinepolicy>();
                var Exist = manualofflinepolicy.GetSingelDetailWithCondition(row => row.PolicyNo == Item.PolicyNo);
                if (Exist != null)
                {
                    Response = manualofflinepolicy.DeleteRecord(Exist);
                }
                else
                    Response = "Invalid PolicyNo.";
            }
            else if (Item.Product.ToLower() == "hlt")
            {
                IDataLayer<ManualOfflineHealthPolicy> manualofflinepolicyhlt = new DataLayer<ManualOfflineHealthPolicy>();
                var Exist = manualofflinepolicyhlt.GetSingelDetailWithCondition(row => row.PolicyNo == Item.PolicyNo);
                if (Exist != null)
                {
                    Response = manualofflinepolicyhlt.DeleteRecord(Exist);
                }
                else
                    Response = "Invalid PolicyNo.";
            }
            else if (Item.Product.ToLower() == "life")
            {
                IDataLayer<ManualOfflineLifePolicy> manualofflinepolicy = new DataLayer<ManualOfflineLifePolicy>();
                var Exist = manualofflinepolicy.GetSingelDetailWithCondition(row => row.PolicyNumber == Item.PolicyNo);
                if (Exist != null)
                {
                    Response = manualofflinepolicy.DeleteRecord(Exist);
                }
                else
                {
                    Response = "Invalid PolicyNo.";
                }
            }
            return Response;
        }
        public dynamic GetPolicyInfo(OfflinePolicy Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            if (Item.Product.ToLower() == "hlt")
            {
                IDataLayer<ManualOfflineHealthPolicy> manualofflinehltpolicy = new DataLayer<ManualOfflineHealthPolicy>();
                var data = manualofflinehltpolicy.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value && row.PolicyNo == Item.PolicyNo);
                return data;
            }
            else
            {
                IDataLayer<Manualofflinepolicy> manualofflinepolicy = new DataLayer<Manualofflinepolicy>();
                var data = manualofflinepolicy.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value && row.PolicyNo == Item.PolicyNo);
                return data;
            }
        }
        #endregion BusinessReport Controller

        #region Products
        public IList<EnquiryTypeMaster> BindEnquiryTypeMaster()
        {
            IDataLayer<EnquiryTypeMaster> Idata = new DataLayer<EnquiryTypeMaster>();
            return Idata.GetDetails().ToList();
        }
        public IEnumerable<ReqAndResponseBase> GetReqResData(ReqResParam item)
        {
            UserModel model = Common.DecodeToken(item.Token);
            IDataLayer<sp_ReqAndResponse> Data = new DataLayer<sp_ReqAndResponse>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_clientid", model.ClientID.ToString());
            param.Add("@p_filterText", item.FilterText);
            param.Add("@p_FilterOption", item.FilterOption);
            param.Add("@p_ProductType", item.ProductType);
            var dataList = Data.ProceduresGetData("sp_ReqAndResponse", param);
            if (dataList.Count() <= 0)
            {
                if (item.FilterText.Contains("ENQ/"))
                {
                    param = null;
                    IDataLayer<sp_QuoteReqAndResponse> Data1 = new DataLayer<sp_QuoteReqAndResponse>();
                    param = new Dictionary<string, string>();
                    param.Add("@p_EnquiryNo", item.FilterText);
                    param.Add("@p_ProductType", item.ProductType);
                    var dataList1 = Data1.ProceduresGetData("sp_QuoteReqAndResponse", param);
                    return dataList1;
                }
                else { return null; }
            }
            else
            {
                return dataList;
            }
        }
        public IEnumerable<sp_RenewData> RenewData(RenewDataList Item)
        {
            UserModel model = Common.DecodeToken(Item.Token);
            IDataLayer<sp_RenewData> Data = new DataLayer<sp_RenewData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            IEnumerable<sp_RenewData> dataReturn = null;
            param.Add("@p_UserID", Item.UserList);
            param.Add("@p_clientid", model.ClientID.ToString());
            param.Add("@p_startDate", Item.StartDate.ToString("yyyy-MM-dd"));
            param.Add("@p_EndDate", Item.EndDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_RenewData", param);
            if (dataList != null)
            {
                dataReturn = dataList.Select(row => { row.EnquiryNo = Common.Encrypt(row.EnquiryNo); return row; });
            }
            return dataReturn;
        }
        public IEnumerable<sp_RenewHealthData> RenewHealthData(RenewDataList Item)
        {
            UserModel model = Common.DecodeToken(Item.Token);
            IDataLayer<sp_RenewHealthData> Data = new DataLayer<sp_RenewHealthData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", Item.UserList);
            param.Add("@p_clientid", model.ClientID.ToString());
            param.Add("@p_startDate", Item.StartDate.ToString("yyyy-MM-dd"));
            param.Add("@p_EndDate", Item.EndDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_RenewHealthData", param);
            return dataList;
        }
        public IEnumerable<sp_ReqResponse> GetAllReqstAndResponse(CallRequestResponseParam item)
        {
            UserModel model = Common.DecodeToken(item.Token);
            IDataLayer<sp_ReqResponse> Data = new DataLayer<sp_ReqResponse>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", model.ClientID.ToString());
            param.Add("@p_Action", item.Action == null ? "" : item.Action);
            param.Add("@p_FromDate", item.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@p_Todate", item.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_ReqResponse", param);
            return dataList;
        }
        #endregion Products

        #region BookingPolicy
        public ClientMaster GetCRMCurrentUrl(CommonParam item)
        {
            try
            {
                var Model = CommonMethods.Common.DecodeToken(item.Token);
                IDataLayer<UserMaster> um = new DataLayer<UserMaster>();
                var exist = um.GetSingelDetailWithCondition(row => row.UserID == Model.UserID && row.Active == true);
                if (exist != null)
                {
                    IDataLayer<ClientMaster> dataLayer = new DataLayer<ClientMaster>();
                    var data = dataLayer.GetSingelDetailWithCondition(row => row.Id == Model.ClientID);
                    return data;
                }
                else
                    return null;
            }
            catch { return null; }
        }
        #endregion BookingPolicy

        #region Setup
        public IEnumerable<UserCreationResponse> ImportUser(UserCreationList Item)
        {
            IDataLayer<testData> testDatas = null;
            //testData ss = null;
            List<UserCreationResponse> lstResponseMessage = new List<UserCreationResponse>();

            UserModel model = new UserModel();
            model = Common.DecodeToken(Item.Token);
            UserMaster um = null;
            DateTime? value = null;
            int accountManager = 0;
            IDataLayer<UserMaster> userm = null;
            UserRegionBranch regionsBranch = null;
            IDataLayer<UserRegionBranch> UR = null;
            string Pass = "";
            foreach (UserCreationItem item in Item.UserCreations)
            {

                try
                {
                    value = null;
                    userm = new DataLayer<UserMaster>();
                    var mm = userm.GetSingelDetailWithCondition(row => row.EmailAddress == item.KeyAccountManager && row.ClientID == model.ClientID.Value);
                    accountManager = mm.UserID;
                    Pass = Guid.NewGuid().ToString().ToUpper().Substring(0, 4);
                    Pass = item.EmailAddress.Substring(0, 3) + "@" + Pass;
                    Pass = Common.Encrypt(Pass);
                    try { value = Convert.ToDateTime(item.DOB); } catch { value = null; }
                    um = new UserMaster()
                    {
                        Active = item.Active == "Y" ? true : false,
                        Address = item.Address,
                        AdhaarNumber = long.Parse(item.AdhaarNumber),
                        BankAccountNo = item.BankAccountNo,
                        ClientID = model.ClientID,
                        CreatedBy = model.UserID,
                        UserID = 0,
                        EmailAddress = item.EmailAddress,
                        Gender = Convert.ToInt32(item.Gender),
                        DOB = value,
                        CreatedDate = DateTime.Now,
                        IFSC_Code = item.IFSC_Code,
                        PANNumber = item.PANNumber,
                        PosPrifix = item.PosPrifix,
                        PosVal = item.PosVal != null ? Convert.ToInt32(item.PosVal) : 0,
                        MobileNo = item.MobileNo,
                        PinCode = item.PinCode,
                        ReferPrifix = item.ReferPrifix,
                        ReferVal = item.ReferVal != null ? Convert.ToInt32(item.ReferVal) : 0,
                        UserName = item.UserName,
                        KeyAccountManager = accountManager,
                        Password = Pass,
                        RoleId = Convert.ToInt32(item.RoleId)
                    };
                    userm = null;
                    userm = new DataLayer<UserMaster>();
                    var savedata = userm.InsertRecord(um);
                    lstResponseMessage.Add(new UserCreationResponse()
                    {
                        ResponseMessage = savedata,
                        UserEmail = item.EmailAddress
                    });

                    userm = null;
                    userm = new DataLayer<UserMaster>();
                    if (savedata == "Data Save Successfully.")
                    {
                        var user = userm.GetSingelDetailWithCondition(row => row.EmailAddress == item.EmailAddress && row.ClientID == model.ClientID);
                        regionsBranch = new UserRegionBranch()
                        {
                            BranchID = item.BranchID != null ? Convert.ToInt32(item.BranchID) : 0,
                            RegionID = item.BranchID != null ? Convert.ToInt32(item.RegionId) : 0,
                            UserID = user.UserID
                        };
                        UR = new DataLayer<UserRegionBranch>();
                        UR.InsertRecord(regionsBranch);
                        regionsBranch = null;
                        um = null;
                    }
                    ReleaseObject(ref userm);
                }
                catch (Exception ex)
                {
                    lstResponseMessage.Add(new UserCreationResponse()
                    {
                        ResponseMessage = ex.Message,
                        UserEmail = item.EmailAddress
                    });
                }
            }
            return lstResponseMessage;
        }
        public dynamic Endusermapping(EnduserMapping Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ActionByUser", um.UserID.ToString());
            param.Add("@p_Userid", Item.MappUserid.ToString());
            param.Add("@p_Clientid", um.ClientID.ToString());
            param.Add("@p_FilterEnquiry", Item.FilterEnquiry);
            if (Item.Product.ToLower() == "mot")
                Response = Data.ProcedureOutput("sp_MapUnMappUser", param);
            else if (Item.Product.ToLower() == "hlt")
                Response = Data.ProcedureOutput("sp_MapUnMapHltpUser", param);
            if (!Response.Contains("Exception"))
            {
                Response = "Update Successfully.";
            }
            else
            {
                Response = "Contact to support.";
            }
            return Response;
        }
        public IEnumerable<UserDetailBase> GetEndUserDetailWithMPD(EnduserMapping Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Clientid", um.ClientID.ToString());
            param.Add("@p_FilterEnquiry", Item.FilterEnquiry.ToString());
            if (Item.Product.ToLower() == "mot")
            {
                IDataLayer<sp_UserDetailViaMPD> Data = new DataLayer<sp_UserDetailViaMPD>();
                var Response = Data.ProceduresGetData("sp_UserDetailViaMPD", param);
                return Response;
            }
            else if (Item.Product.ToLower() == "hlt")
            {
                IDataLayer<sp_UserDetailViaHPD> Data = new DataLayer<sp_UserDetailViaHPD>();
                var Response = Data.ProceduresGetData("sp_UserDetailViaHPD", param);
                return Response;
            }
            return null;
        }
        public IEnumerable<UserMaster> GetUserListWithRoleid(RoleTypeParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<UserMaster> data = new DataLayer<UserMaster>();
            var DataResponse = data.GetDetailsWithCondition(row => row.RoleId == Item.ID
                                    && row.ClientID == um.ClientID && row.Active == true);
            return DataResponse;
        }
        public string SetupUserPrivilege(MergeUserPrivilege Item)
        {
            UserModel model = Common.DecodeToken(Item.Token);
            string[] userList = Item.SeprateUsers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] PrivList = Item.SepratePriveles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IDataLayer<userprivilegerolemapping> data = null;
            userprivilegerolemapping obj = null;
            string Response = "";
            foreach (string user in userList)
            {
                foreach (string priv in PrivList)
                {
                    obj = new userprivilegerolemapping()
                    {
                        ID = 0,
                        PrivilegeID = Convert.ToInt32(priv.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0]),
                        NavBarMasterMenuID = Convert.ToInt32(priv.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[1]),
                        AssignBy = model.UserID,
                        UserID = Convert.ToInt32(user),
                        Addrecord = 1,
                        AssignDate = DateTime.Now,
                        deleterecord = 0,
                        Editrecord = 0
                    };
                    data = new DataLayer<userprivilegerolemapping>();
                    Response = data.InsertRecord(obj);
                    data = null;
                    obj = null;
                }
            }
            return Response;
        }
        public string SetupUserRoleType(MergeUserPrivilege Item)
        {
            UserModel model = Common.DecodeToken(Item.Token);
            string[] userList = Item.SeprateUsers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string Response = "";

            sp_ReferAndPosSeries ReferAndPosSeries = null;
            IDataLayer<UserMaster> um = null;
            IDataLayer<RoleType> RT = new DataLayer<RoleType>();
            var RoleData = RT.GetSingelDetailWithCondition(row => row.RoleID == Item.RoleID);

            IDataLayer<sp_ReferAndPosSeries> data = null;
            Dictionary<string, string> paramPro = null;

            foreach (string user in userList)
            {
                if (Item.RoleID == 8)
                {
                    data = new DataLayer<sp_ReferAndPosSeries>();
                    paramPro = new Dictionary<string, string>();
                    paramPro.Add("@p_RoleID", Item.RoleID.ToString());
                    paramPro.Add("@p_ClientID", model.ClientID.ToString());
                    var dataList = data.ProceduresGetData("sp_ReferAndPosSeries", paramPro);
                    if (dataList != null)
                        ReferAndPosSeries = dataList.FirstOrDefault();
                }
                um = new DataLayer<UserMaster>();
                UserMaster userM = null;
                userM = um.GetSingelDetailWithCondition(row => row.UserID == Convert.ToInt32(user) && row.ClientID == model.ClientID);
                userM.RoleId = Item.RoleID;
                userM.UserType = RoleData.USERTYPE;
                if (Item.RoleID == 8)
                {
                    um = null;
                    um = new DataLayer<UserMaster>();
                    var Exist = um.GetDetailsWithCondition(row => row.ClientID == model.ClientID.Value && row.PosPrifix == ReferAndPosSeries.PosPrifix && row.PosVal == ReferAndPosSeries.PosVal);
                    if (Exist.Count() > 0)
                    {
                        Response = "Poscode Exist";
                        continue;
                    }
                }
                userM.PosPrifix = ReferAndPosSeries != null ? ReferAndPosSeries.PosPrifix : null;
                userM.PosVal = ReferAndPosSeries != null ? ReferAndPosSeries.PosVal : null;
                um = null;
                um = new DataLayer<UserMaster>();
                Response = um.Update(userM);

            }
            return Response;
        }
        public IEnumerable<RegionZone> RegionlistWithUserID(RoleTypeParam Item)
        {
            List<RegionZone> lst = new List<RegionZone>();
            IDataLayer<UserRegionBranch> data = new DataLayer<UserRegionBranch>();
            var lstUserRegion = data.GetDetailsWithCondition(row => row.UserID == Item.ID);
            if (lstUserRegion != null)
            {
                IDataLayer<RegionZone> dataregion = null;
                foreach (UserRegionBranch user in lstUserRegion)
                {
                    dataregion = new DataLayer<RegionZone>();
                    var rg = dataregion.GetSingelDetailWithCondition(row => row.ID == user.RegionID);
                    lst.Add(rg);
                    dataregion = null;
                }
            }
            return lst;
        }
        public string UpdateReportingManager(ManageUserMapp Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            string Response = "";
            string[] userList = Item.SeprateUsers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IDataLayer<UserMaster> um = null;
            IDataLayer<UserRegionBranch> urb = null;
            IDataLayer<UserMaster> RmData = new DataLayer<UserMaster>();
            var rmData = RmData.GetSingelDetailWithCondition(row => row.UserID == Item.ReportingManagerID);
            foreach (string user in userList)
            {
                um = new DataLayer<UserMaster>();
                var userM = um.GetSingelDetailWithCondition(row => row.UserID == Convert.ToInt32(user) && row.ClientID == umodel.ClientID);
                userM.KeyAccountManager = rmData.UserID;
                userM.RmCode = rmData.RmCode;
                um = null;
                um = new DataLayer<UserMaster>();
                Response = um.Update(userM);
                urb = new DataLayer<UserRegionBranch>();
                var UrbModel = urb.GetSingelDetailWithCondition(row => row.UserID == Convert.ToInt32(user));
                urb = null;
                urb = new DataLayer<UserRegionBranch>();
                if (UrbModel == null)
                {
                    UserRegionBranch obj = new UserRegionBranch()
                    {
                        ID = 0,
                        BranchID = Item.BranchID,
                        RegionID = Item.RegionID,
                        UserID = Convert.ToInt32(user)
                    };
                    Response = urb.InsertRecord(obj);
                    obj = null;
                }
                else
                {
                    UrbModel.BranchID = Item.BranchID;
                    UrbModel.RegionID = Item.RegionID;
                    Response = urb.Update(UrbModel);
                }
            }
            return Response;
        }
        public string UpdateRegionWithTeam(ManageUserMapp Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", umodel.ClientID.ToString());
            param.Add("@p_userid", Item.SeprateUsers);
            param.Add("@p_moveregionid", Item.RegionID.ToString());
            param.Add("@p_movebranchid", Item.BranchID.ToString());
            Response = Data.ProcedureOutput("SP_UPDATEUSERREGION", param);
            if (!Response.Contains("Exception"))
            {
                Response = "Record Updated.";
            }
            else
            {
                Response = "Contact To support";
            }
            return Response;
        }
        public IEnumerable<Manufacturer> MotorList(MotorParam item)
        {
            IDataLayer<Manufacturer> dataLayer = new DataLayer<Manufacturer>();
            if (item.Type.ToLower() == "car")
                return dataLayer.GetDetailsWithCondition(row => row.IsCar == true && row.IsActive == true);
            else if (item.Type.ToLower() == "two")
                return dataLayer.GetDetailsWithCondition(row => row.IsCar == true && row.IsActive == true);
            return null;
        }
        public IEnumerable<Vehicles> ModelList(MotorParam item)
        {
            IDataLayer<Vehicles> dataLayer = new DataLayer<Vehicles>();
            if (item.Type == "car")
                return dataLayer.GetDetailsWithCondition(row => row.IsActive == true && row.VehicleType == "PrivateCar" && row.ManufacturerID == item.ID);
            else if (item.Type == "two")
                return dataLayer.GetDetailsWithCondition(row => row.IsActive == true && row.VehicleType == "TwoWheeler" && row.ManufacturerID == item.ID);
            return null;
        }
        public IEnumerable<Variants> Variants(MotorParam item)
        {
            IDataLayer<Variants> dataLayer = new DataLayer<Variants>();
            return dataLayer.GetDetailsWithCondition(row => row.VehicleID == item.ID && row.IsActive == true);
        }
        public IEnumerable<Companies> Companies(CompaniesparamWithProduct item)
        {
            IDataLayer<Companies> data = new DataLayer<Companies>();
            if (item.Product.ToLower() == "motor")
                return data.GetDetailsWithCondition(row => (row.CarInsurance == true || row.TwowheelerInsurance == true) && row.IsActive == true);
            else if (item.Product.ToLower() == "health")
                return data.GetDetailsWithCondition(row => row.HealthInsurance == true && row.IsActive == true);
            else
                return null;
        }
        public IEnumerable<sp_MapOrUnmapList> MappOrUnmaplist(MappParam Item)
        {
            IDataLayer<sp_MapOrUnmapList> Data = new DataLayer<sp_MapOrUnmapList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_CompanyID", Item.CompanyID.ToString());
            param.Add("@p_Variantid", Item.Variantid.ToString());
            param.Add("@p_variantname", Item.Variantname);
            param.Add("@p_modelname", Item.Modelname);
            param.Add("@p_makename", Item.Makename);
            var Response = Data.ProceduresGetData("sp_MapOrUnmapList", param);
            return Response;
        }
        public string GetMapped(MappVariant Item)
        {
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_CompanyID", Item.CompanyID.ToString());
            param.Add("@p_variantid", Item.Variantid.ToString());
            param.Add("@p_Companyvariantid", Item.CompVrID.ToString());
            var Response = Data.ProcedureOutput("sp_GetMappendVarinat", param);
            if (!Response.Contains("Exception"))
                Response = "Mapped SuccessFully.";
            else
                Response = "Not Mapped Contact to Support.";
            return Response;
        }
        public IEnumerable<Manufacturer> Getmanufacturer()
        {
            IDataLayer<Manufacturer> data = new DataLayer<Manufacturer>();
            return data.GetDetails();
        }
        public string AddManufacturer(AddManufacturerParam Item)
        {
            string Response = "";
            IDataLayer<Manufacturer> manu = new DataLayer<Manufacturer>();
            var Contain = manu.GetSingelDetailWithCondition(row => row.Manufacturername.ToLower() == Item.ManufacturerName.ToLower());
            if (Contain == null)
            {
                Manufacturer obj = new Manufacturer()
                {
                    Manufacturername = Item.ManufacturerName,
                    IsActive = Item.IsActive,
                    IsBike = Item.IsBike,
                    IsCar = Item.IsCar,
                    ManufacturerID = 0
                };
                manu = null;
                manu = new DataLayer<Manufacturer>();
                Response = manu.InsertRecord(obj);
            }
            else { Response = "Already Exist."; }
            return Response;
        }
        public string AddVehicle(AddVehicleParam Item)
        {
            string Response = "";
            IDataLayer<Vehicles> manu = new DataLayer<Vehicles>();
            var Contain = manu.GetSingelDetailWithCondition(row =>
            row.VehicleName.ToLower() == Item.VehicleName.ToLower() && row.ManufacturerID == Item.ManufacturerID);
            if (Contain == null)
            {
                Vehicles obj = new Vehicles()
                {
                    ManufacturerID = Item.ManufacturerID,
                    IsActive = Item.IsActive,
                    VehicleName = Item.VehicleName,
                    VehicleType = Item.VehicleType,
                    VehicleID = 0
                };
                manu = null;
                manu = new DataLayer<Vehicles>();
                Response = manu.InsertRecord(obj);
            }
            else { Response = "Already Exist."; }
            return Response;
        }
        public IEnumerable<vw_vehicles> GetVehicles()
        {
            IDataLayer<vw_vehicles> data = new DataLayer<vw_vehicles>();
            return data.GetDetails();
        }
        public IEnumerable<Fuels> GetFuels()
        {
            IDataLayer<Fuels> obje = new DataLayer<Fuels>();
            return obje.GetDetails();
        }
        public string AddVariant(AddVariantParam Item)
        {
            string Response = "";
            IDataLayer<Variants> manu = new DataLayer<Variants>();
            var Contain = manu.GetSingelDetailWithCondition(row =>
            row.VariantName.ToLower() == Item.VariantName.ToLower() && row.VehicleID == Item.VehicleID
            && row.VehicleCC == Item.VehicleCC && row.SeatingCapacity == Item.SeatingCapacity
            && row.FuelID == Item.FuelID);
            if (Contain == null)
            {
                Variants obj = new Variants()
                {
                    ExShowroomPrice = Item.ExShowroomPrice,
                    FuelID = Item.FuelID,
                    SeatingCapacity = Item.SeatingCapacity,
                    VehicleCC = Item.VehicleCC,
                    VehicleID = Item.VehicleID,
                    VariantName = Item.VariantName,
                    IsActive = Item.IsActive,
                    VariantID = 0
                };
                manu = null;
                manu = new DataLayer<Variants>();
                Response = manu.InsertRecord(obj);
            }
            else { Response = "Already Exist."; }
            return Response;
        }
        public IEnumerable<vw_Variants> GetVariants()
        {
            IDataLayer<vw_Variants> varinats = new DataLayer<vw_Variants>();
            return varinats.GetDetails();
        }
        public string Campaigns(CamapignsParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            string Response = "";
            IDataLayer<MailServerOption> mailserver = new DataLayer<MailServerOption>();
            var MailOptContain = mailserver.GetSingelDetailWithCondition(row => row.MailServiceMaster.ToLower().Contains("campaign"));
            if (MailOptContain != null)
            {
                IDataLayer<MailServer> maildata = new DataLayer<MailServer>();
                var Mailserv = maildata.GetSingelDetailWithCondition(row => row.MailserveroptionID == MailOptContain.ID && row.ClientID == umodel.ClientID.Value);
                if (Mailserv != null)
                {
                    string mailMessage = "";
                    foreach (string id in Item.Users)
                    {
                        Common.mailMaster(Mailserv.FromEmail, id, Item.Subject, Item.NotifationMessage,
                            Mailserv.FromEmail, Mailserv.Password, Mailserv.HostName, Mailserv.Port, Mailserv.UseDefaultCredential, Mailserv.EnableSsl, ref mailMessage);
                        Thread.Sleep(70);
                        if (mailMessage.StartsWith("Exception :"))
                            break;
                    }
                    Response = mailMessage;
                }
                else
                {
                    Response = "Campaign Mail not in master";
                }
            }
            else
                Response = "Campaign Mail not configured";
            return Response;
        }
        public string ImportRenewal(ImportRenewal item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<ClientMaster> CM = new DataLayer<ClientMaster>();
            IDataLayer<Enquiry> Enq = null;
            var ClientModel = CM.GetSingelDetailWithCondition(row => row.Id == Umodel.ClientID.Value);
            foreach (var renew in item.Renewals)
            {
                IDataLayer<Manufacturer> manufacturer = new DataLayer<Manufacturer>();
                var manufactureExist = manufacturer.GetSingelDetailWithCondition(row => row.Manufacturername.ToLower() == renew.ManufactureID.ToLower());
                if (manufactureExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<Vehicles> vehicles = new DataLayer<Vehicles>();
                var vehicleExist = vehicles.GetSingelDetailWithCondition(row => row.VehicleName.ToLower() == renew.VehicleID.ToLower() && row.ManufacturerID == manufactureExist.ManufacturerID);
                if (vehicleExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<Variants> variants = new DataLayer<Variants>();
                var variantsExist = variants.GetSingelDetailWithCondition(row => row.VariantName.ToLower() == renew.VariantID.ToLower() && row.VehicleID == vehicleExist.VehicleID);
                if (vehicleExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<Rtos> rtos = new DataLayer<Rtos>();
                var rtosExist = rtos.GetSingelDetailWithCondition(row => row.RTOName.ToLower() == renew.RTOID.ToLower());
                if (rtosExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<Companies> companies = new DataLayer<Companies>();
                var companiesExist = companies.GetSingelDetailWithCondition(row => row.CompanyName.ToLower() == renew.CompanyID.ToLower());
                if (companiesExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<Fuels> fuels = new DataLayer<Fuels>();
                var fuelsExist = fuels.GetSingelDetailWithCondition(row => row.FuelName.ToLower() == renew.FuelID.ToLower());
                if (fuelsExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<Cities> cities = new DataLayer<Cities>();
                var citiesExist = cities.GetSingelDetailWithCondition(row => row.CityName.ToLower() == renew.CityID.ToLower());
                if (fuelsExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }
                IDataLayer<States> states = new DataLayer<States>();
                var statesExist = states.GetSingelDetailWithCondition(row => row.Statename.ToLower() == renew.StateID.ToLower());
                if (statesExist == null)
                {
                    Response += "EngineNo(" + renew.EngineNo + ") not saved.\n";
                    continue;
                }

                var FileModel = new
                {
                    FuelID = fuelsExist.FuelID,
                    ManufactureID = manufactureExist.ManufacturerID,
                    RTOID = rtosExist.RTOID,
                    VariantID = variantsExist.VariantID,
                    VehicleID = vehicleExist.VehicleID,
                    CityID = citiesExist.CityID,
                    StateID = statesExist.StateID,
                    CompanyID = companiesExist.CompanyID
                };

                Enq = new DataLayer<Enquiry>();
                string EnPrifix = ClientModel.clientId + "/ENQ/" + DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                Enquiry ImportEnq = new Enquiry()
                {
                    ClientID = Umodel.ClientID.Value,
                    EnquiryDate = DateTime.Now,
                    EnquiryNo = EnPrifix,
                    LeadSource = "Renewal",
                    MobileNo = renew.MobileNo,
                    EnquiryType = renew.MotorType.ToLower() == "car" || renew.MotorType.ToLower() == "car" ? "MOT" : "",
                    UserID = 0,
                    ID = 0,
                    Status = true
                };
                var resp = Enq.InsertRecord(ImportEnq);
                if (resp == "Data Save Successfully.")
                {
                    IDataLayer<Motorenquiry> Medata = new DataLayer<Motorenquiry>();
                    Motorenquiry meModel = new Motorenquiry()
                    {
                        EnquiryNo = EnPrifix,
                        FuelID = FileModel.FuelID,
                        ManufactureID = FileModel.ManufactureID,
                        MotorID = 0,
                        MotorType = renew.MotorType,
                        PolicyType = renew.PolicyType.ToArray()[0].ToString(),
                        RTOID = FileModel.RTOID,
                        Status = 1,
                        RegistartionYear = renew.RegistartionYear,
                        VariantID = FileModel.VariantID,
                        VehicleID = FileModel.VehicleID,
                        ClientID = Umodel.ClientID.Value
                    };
                    var meResp = Medata.InsertRecord(meModel);
                    if (meResp == "Data Save Successfully.")
                    {
                        IDataLayer<Users> userData = new DataLayer<Users>();
                        Users users = new Users()
                        {
                            UserID = 0,
                            ClientID = Umodel.ClientID.Value,
                            DateOfBirth = renew.DateOfBirth,
                            Email = renew.Email,
                            EnquiryNo = EnPrifix,
                            FirstName = renew.FirstName,
                            LastName = renew.LastName,
                            Mobile = renew.MobileNo,
                            EntryDate = DateTime.Now
                        };
                        var userRes = userData.InsertRecord(users);
                        if (userRes == "Data Save Successfully.")
                        {
                            userData = null;
                            userData = new DataLayer<Users>();
                            var GetUser = userData.GetSingelDetailWithCondition(row => row.EnquiryNo == EnPrifix);
                            IDataLayer<Useraddresses> addressData = new DataLayer<Useraddresses>();
                            Useraddresses useraddresses = new Useraddresses()
                            {
                                AddressID = 0,
                                AddressLine1 = renew.AddressLine1,
                                AddressLine2 = renew.AddressLine2,
                                AddressLine3 = renew.AddressLine3,
                                AddressLine4 = "",
                                CityID = FileModel.CityID,
                                StateID = FileModel.StateID,
                                IsPrimary = true,
                                UserID = GetUser.UserID,
                                PinCode = renew.PinCode
                            };
                            var addResp = addressData.InsertRecord(useraddresses);
                            if (addResp == "Data Save Successfully.")
                            {
                                addressData = null;
                                Medata = null;
                                addressData = new DataLayer<Useraddresses>();
                                Medata = new DataLayer<Motorenquiry>();
                                var getMotordata = Medata.GetSingelDetailWithCondition(row => row.EnquiryNo == EnPrifix);
                                var GetAddress = addressData.GetSingelDetailWithCondition(row => row.UserID == GetUser.UserID);
                                IDataLayer<Motorpolicydetails> motordata = new DataLayer<Motorpolicydetails>();
                                Motorpolicydetails motorpolicydetails = new Motorpolicydetails()
                                {
                                    AddressID = GetAddress.AddressID,
                                    BasicOD = renew.BasicOD,
                                    BasicTP = renew.BasicTP,
                                    ChesisNo = renew.ChesisNo,
                                    CompanyID = FileModel.CompanyID,
                                    ClientID = Umodel.ClientID,
                                    EngineNo = renew.EngineNo,
                                    TotalPremium = renew.TotalPremium,
                                    UserID = GetUser.UserID,
                                    MotorID = getMotordata.MotorID,
                                    MotorType = renew.MotorType,
                                    VehicleNo = renew.VehicleNo,
                                    MotorPolicyID = 0
                                };
                                var motorResp = motordata.InsertRecord(motorpolicydetails);
                                if (motorResp == "Data Save Successfully.")
                                {
                                    motordata = null;
                                    motordata = new DataLayer<Motorpolicydetails>();
                                    var Motordetails = motordata.GetSingelDetailWithCondition(row => row.MotorID == getMotordata.MotorID);
                                    IDataLayer<Motorpolicydetailsothersdata> motorOther = new DataLayer<Motorpolicydetailsothersdata>();
                                    Motorpolicydetailsothersdata motorpolicydetailsothersdata = new Motorpolicydetailsothersdata()
                                    {
                                        ID = 0,
                                        MotorpolicyID = Motordetails.MotorPolicyID,
                                        PolicyStartDate = renew.PolicyStartDate,
                                        PolicyEndDate = renew.PolicyExpiryDate
                                    };
                                    motorOther.InsertRecord(motorpolicydetailsothersdata);
                                    motorOther = null;
                                    Response += "EngineNo(" + renew.EngineNo + ") saved.\n";
                                }
                                motordata = null;
                            }
                            else
                                Response += "Useraddresses : EngineNo(" + renew.EngineNo + ") not saved.\n";
                            addressData = null;
                        }
                        else
                            Response += "Users : EngineNo(" + renew.EngineNo + ") not saved.\n";
                        userData = null;
                    }
                    else
                        Response += "Motorenquiry : EngineNo(" + renew.EngineNo + ") not saved.\n";
                    Medata = null;
                }
                else
                    Response += "Enquiry : EngineNo(" + renew.EngineNo + ") not saved.\n";
                Enq = null;
            }
            return Response;
        }
        public string UpdateAlterNateCode(MergeUserAlterNateCode Item)
        {
            string Response = "";
            UserModel model = Common.DecodeToken(Item.Token);
            string[] userList = Item.SeprateUsers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IDataLayer<UserMaster> UMaster = new DataLayer<UserMaster>();
            foreach (string umaster in userList)
            {
                var Exist = UMaster.GetSingelDetailWithCondition(row => row.UserID == Convert.ToInt32(umaster) && row.ClientID == model.ClientID.Value);
                if (Exist != null)
                {
                    UMaster = null;
                    UMaster = new DataLayer<UserMaster>();
                    Exist.Alternatecode = Item.AlterNateCode;
                    Response = UMaster.Update(Exist);
                }
            }
            if (UMaster != null)
                ReleaseObject<IDataLayer<UserMaster>>(ref UMaster);
            return Response;
        }
        public string UpdateRMCode(MergeUserRMCode Item)
        {
            string Response = "";
            UserModel model = Common.DecodeToken(Item.Token);
            string[] userList = Item.SeprateUsers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IDataLayer<UserMaster> UMaster = new DataLayer<UserMaster>();
            foreach (string umaster in userList)
            {
                var Exist = UMaster.GetSingelDetailWithCondition(row => row.UserID == Convert.ToInt32(umaster) && row.ClientID == model.ClientID.Value);
                if (Exist != null)
                {
                    UMaster = null;
                    UMaster = new DataLayer<UserMaster>();
                    Exist.RmCode = Item.RMCode;
                    Response = UMaster.Update(Exist);
                }
            }
            if (UMaster != null)
                ReleaseObject<IDataLayer<UserMaster>>(ref UMaster);
            return Response;
        }
        #endregion Setup
        #region Master Setup
        public IEnumerable<sp_GetReferPosCode> GetCodePrifix(FilterPrifix Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<sp_GetReferPosCode> Data = new DataLayer<sp_GetReferPosCode>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_clientid", um.ClientID.ToString());
            param.Add("@p_roleid", Item.RoleID.ToString());
            var Response = Data.ProceduresGetData("sp_GetReferPosCode", param);
            return Response;
        }
        public string updateOrInsertPosRefer(CodePrifixParam Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<ReferPosCodeMaster> data = new DataLayer<ReferPosCodeMaster>();
            var COdeData = data.GetSingelDetailWithCondition(row => row.ClientID == um.ClientID && row.RoleID == Item.RoleID && row.Options == Item.Option);
            data = null;
            data = new DataLayer<ReferPosCodeMaster>();
            if (COdeData == null)
            {
                ReferPosCodeMaster obj = new ReferPosCodeMaster()
                {
                    ClientID = um.ClientID.Value,
                    ReferPrifix = Item.Prifix,
                    CodeVal = Item.CodeVal,
                    RoleID = Item.RoleID,
                    Create_Date = DateTime.Now,
                    ID = 0,
                    IsParent = Item.IsParent,
                    Options = Item.Option
                };
                Response = data.InsertRecord(obj);
            }
            else
            {
                COdeData.CodeVal = Item.CodeVal;
                COdeData.ReferPrifix = Item.Prifix;
                Response = data.Update(COdeData);
            }
            return Response;
        }
        public string PosDuration(PosDuration Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<PosUserActiveDuration> Data = new DataLayer<PosUserActiveDuration>();
            var containdata = Data.GetSingelDetailWithCondition(row => row.ClientID == um.ClientID);
            Data = null;
            if (containdata == null)
            {
                Data = new DataLayer<PosUserActiveDuration>();
                PosUserActiveDuration duration = new PosUserActiveDuration()
                {
                    ID = 0,
                    ClientID = um.ClientID.Value,
                    HourDuration = Item.Hours,
                    RoleID = 8
                };
                Response = Data.InsertRecord(duration);
            }
            else
            {
                containdata.HourDuration = Item.Hours;
                Data = new DataLayer<PosUserActiveDuration>();
                Response = Data.Update(containdata);
            }
            return Response;
        }
        public IEnumerable<sp_MapRoleList> GetRoleForMap(CommonParam Item)
        {
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<sp_MapRoleList> Data = new DataLayer<sp_MapRoleList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Clientid", Um.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_MapRoleList", param);
            return dataList;
        }
        public string SaveRoleList(MapRole Item)
        {
            string Response = "";

            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<RoletypeClientActive> data = null;
            foreach (RoleList role in Item.Roles)
            {
                data = new DataLayer<RoletypeClientActive>();
                var Contain = data.GetSingelDetailWithCondition(row => row.ClientID == um.ClientID && row.RoleID == role.RoleID);
                data = null;
                data = new DataLayer<RoletypeClientActive>();
                if (Contain == null)
                {
                    RoletypeClientActive obj = new RoletypeClientActive()
                    {
                        RoleID = role.RoleID,
                        ClientID = um.ClientID.Value,
                        ID = 0,
                        IsActive = role.IsActive
                    };
                    Response = data.InsertRecord(obj);
                    GC.SuppressFinalize(obj);
                    obj = null;
                }
                else
                {
                    Contain.IsActive = role.IsActive;
                    Response = data.Update(Contain);
                }
                GC.SuppressFinalize(data);
                if (Contain != null)
                    GC.SuppressFinalize(Contain);
                data = null;
                Contain = null;
            }
            return Response;
        }
        public string IRDAparam(IRDAparam item)
        {
            string Response = "";
            UserModel Um = Common.DecodeToken(item.Token);
            IDataLayer<IRDAHiddenProcess> irda = new DataLayer<IRDAHiddenProcess>();
            irda = null;
            if (Um.RoleID == 28 || Um.RoleID == 1)
            {
                irda = new DataLayer<IRDAHiddenProcess>();
                var Contain = irda.GetSingelDetailWithCondition(row => row.ClientID == Um.ClientID.Value);
                if (Contain != null)
                {
                    Contain.IsHidden = item.IsApply;
                    Response = irda.Update(Contain);
                    ReleaseObject<IRDAHiddenProcess>(ref Contain);
                    Contain = null;
                }
                else
                {
                    IRDAHiddenProcess obj = new IRDAHiddenProcess()
                    {
                        ClientID = Um.ClientID.Value,
                        CreateDate = DateTime.Now,
                        ID = 0,
                        IsHidden = item.IsApply,
                        UserID = Um.UserID
                    };
                    Response = irda.InsertRecord(obj);
                    ReleaseObject<IRDAHiddenProcess>(ref obj);
                    obj = null;
                }
            }
            return Response;
        }
        public bool CheckIRDA(CommonParam Item)
        {
            bool response = false;
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<IRDAHiddenProcess> irda = new DataLayer<IRDAHiddenProcess>();
            irda = null;
            if (Um.RoleID == 28 || Um.RoleID == 1)
            {
                irda = new DataLayer<IRDAHiddenProcess>();
                var Contain = irda.GetSingelDetailWithCondition(row => row.ClientID == Um.ClientID.Value);
                if (Contain != null)
                {
                    response = Contain.IsHidden;
                }
            }
            return response;
        }
        public IEnumerable<RegionZone> GetAllRegionList(CommonParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<RegionZone> rz = new DataLayer<RegionZone>();
            return rz.GetDetailsWithCondition(row => row.ClientID == um.ClientID.Value);
        }
        public IEnumerable<sp_Regionbranchwithclient> GetAllBranchListByClient(CommonParam Item)
        {
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<sp_Regionbranchwithclient> cities = new DataLayer<sp_Regionbranchwithclient>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", Um.ClientID.Value.ToString());
            var Response = cities.ProceduresGetData("sp_Regionbranchwithclient", param);
            return Response;
        }
        public string SaveRegionWithBranch(ListRegionWithBranch Item)
        {
            string Response = "";
            UserModel Um = Common.DecodeToken(Item.Token);
            IDataLayer<BranchForClient> bf = null;
            foreach (RegionWithBranch model in Item.LstRegionWithBranch)
            {
                bf = new DataLayer<BranchForClient>();
                var contain = bf.GetSingelDetailWithCondition(row => row.ClientID == Um.ClientID.Value && row.RegionID == model.RegionID && row.BranchID == model.BranchID);
                if (contain == null)
                {
                    bf = null;
                    BranchForClient obj = new BranchForClient()
                    {
                        BranchID = model.BranchID,
                        ClientID = Um.ClientID.Value,
                        RegionID = model.RegionID
                    };
                    bf = new DataLayer<BranchForClient>();
                    Response = bf.InsertRecord(obj);
                    GC.SuppressFinalize(obj);
                    obj = null;
                }
                GC.SuppressFinalize(bf);
                bf = null;
            }
            return Response;
        }
        public IEnumerable<MailServerOption> GetMailServerOption()
        {
            IDataLayer<MailServerOption> OPtions = new DataLayer<MailServerOption>();
            return OPtions.GetDetails();
        }
        public string SaveMailSetup(MailserverSetupParam item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<MailServer> mail = new DataLayer<MailServer>();
            MailServer Contain = null;
            if (item.Mailserveroption == "CreateUser")
            {
                Contain = mail.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value &&
                                              row.MailserveroptionID == item.MailserveroptionID && row.RoleID == item.RoleID);
            }
            else
            {
                Contain = mail.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value &&
                                                                row.MailserveroptionID == item.MailserveroptionID);
            }
            IDataLayer<MailServerOption> mailoption = new DataLayer<MailServerOption>();
            var option = mailoption.GetSingelDetailWithCondition(row => row.MailServiceMaster == "CreateUser");
            if (option.ID == item.MailserveroptionID)
            {
                if (item.RoleID == null)
                {
                    Response = "Please select role for configure email.";
                    return Response;
                }
                IDataLayer<sp_UserCreationMailConfig> cities = new DataLayer<sp_UserCreationMailConfig>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
                param.Add("@p_RoleID", item.RoleID.ToString());
                param.Add("@p_MailserveroptionID", item.MailserveroptionID.ToString());
                var ResponseData = cities.ProceduresGetData("sp_UserCreationMailConfig", param);
                if (ResponseData != null)
                {
                    if (ResponseData.FirstOrDefault().Message == "Email is configured for all Role.")
                    {
                        Response = ResponseData.FirstOrDefault().Message;
                        return Response;
                    }
                }
            }
            mail = null;
            mail = new DataLayer<MailServer>();
            if (Contain != null)
            {
                Contain.HostName = item.HostName;
                Contain.Password = item.PasswordVal;
                Contain.Port = item.Port;
                Contain.UseDefaultCredential = item.UseDefaultCredential;
                Contain.FromEmail = item.FromEmail;
                Contain.UserName = item.UserName;
                Contain.EnableSsl = item.EnableSsl;
                Response = mail.Update(Contain);
            }
            else
            {
                MailServer mm = new MailServer()
                {
                    EnableSsl = item.EnableSsl,
                    FromEmail = item.FromEmail,
                    UseDefaultCredential = item.UseDefaultCredential,
                    Port = item.Port,
                    ClientID = Umodel.ClientID.Value,
                    HostName = item.HostName,
                    MailserveroptionID = item.MailserveroptionID,
                    Password = item.PasswordVal,
                    ID = 0,
                    UserName = item.UserName,
                    RoleID = item.Mailserveroption == "CreateUser" ? item.RoleID : null
                };
                Response = mail.InsertRecord(mm);
            }
            return Response;
        }
        public MailServer GetSelectedMailOption(MailSelectedOption item)
        {
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<MailServer> mail = new DataLayer<MailServer>();
            return mail.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value &&
                                                    row.MailserveroptionID == item.MailserveroptionID);
        }
        public sp_MailserverData GetSelectedMailOptionWithRole(MailSelectedOptionWithRole item)
        {
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<sp_MailserverData> MailserverData = new DataLayer<sp_MailserverData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", Umodel.ClientID.Value.ToString());
            param.Add("@p_RoleID", item.RoleID.ToString());
            var Response = MailserverData.ProceduresGetData("sp_MailserverData", param);
            if (Response != null)
                return Response.FirstOrDefault();
            return null;
        }
        public string SaveSMSSetup(SMSServerSetupParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<SmsServer> mail = new DataLayer<SmsServer>();
            var Contain = mail.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value &&
                                                            row.MailServerOptionID == Item.MailserveroptionID);
            mail = null;
            mail = new DataLayer<SmsServer>();
            if (Contain == null)
            {
                SmsServer objSms = new SmsServer()
                {
                    ClientID = Umodel.ClientID.Value,
                    MailServerOptionID = Item.MailserveroptionID,
                    SMSAPI = Item.APIURL,
                    ID = 0
                };
                Response = mail.InsertRecord(objSms);
            }
            else
            {
                Contain.SMSAPI = Item.APIURL;
                Response = mail.Update(Contain);
            }
            return Response;
        }
        public SmsServer GetSelectedSmsSeverOption(SMSServerSetupParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<SmsServer> mail = new DataLayer<SmsServer>();
            var Contain = mail.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value &&
                                                            row.MailServerOptionID == Item.MailserveroptionID);
            return Contain;
        }
        public IEnumerable<CompanyDetails> VehicleMappingData(VehicleVariantMapping Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ClientDBConnection> Dbs = new DataLayer<ClientDBConnection>();
            var data = Dbs.GetSingelDetailWithCondition(row => row.ID == 1);

            string query = "getCompanyNameBymodel '" + Item.make + "','" + Item.model.ToString().Remove(Item.model.ToString().Length - 1) + "'," + Item.varientId + "," + Item.companyId + ",'" + Item.fuel + "','" + Item.vehiletypeId + "'";
            DataTable dt = SqlDBHelper.ExecuteSQLQuery(query, data.ConnectionString);
            var lstData = dt.AsEnumerable().Select(row => new CompanyDetails()
            {
                isMapp = Convert.ToInt16(row["isMapp"]),
                Id = Convert.ToInt32(row["Id"]),
                Make = row["Make"].ToString(),
                Model = row["Model"].ToString(),
                CubicCapacity = row["CubicCapacity"].ToString(),
                VehicleType = row["VehicleType"].ToString(),
                Variant = row["Variant"].ToString(),
                fueltype = row["fueltype"].ToString()
            }).ToList();
            return lstData;
        }
        public string MapVehicle(MapUnmap Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ClientDBConnection> Dbs = new DataLayer<ClientDBConnection>();
            var data = Dbs.GetDetails();
            foreach (var db in data)
            {
                string query = "exec uspMappUnMappVarient '" + Item.companyId + "','" + Item.BrokerId + "'," + Item.varientId + "," + Item.isMapp + "," + Umodel.ClientID.Value + "";
                DataTable dt = SqlDBHelper.ExecuteSQLQuery(query, db.ConnectionString);
                if (dt != null)
                {
                    Response += dt.Rows[0]["result"].ToString() == "1" ? "Success" : "Not Mapped";
                }
                else
                {
                    Response += " : Not Mapped " + Item.varientId;
                }
            }
            return Response;
        }
        public IEnumerable<Companies> GetCompanies()
        {
            IDataLayer<Companies> comp = new DataLayer<Companies>();
            return comp.GetDetails().OrderBy(row => row.CompanyName);
        }
        public string SavePayoutData(SavePayoutDataParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Insurerpayout> insurerpayout = new DataLayer<Insurerpayout>();
            List<Insurerpayout> Insurerlist = new List<Insurerpayout>();
            foreach (var payouts in Item.payoutList)
            {
                Insurerlist.Add(new Insurerpayout()
                {
                    UserID = payouts.User,
                    ClientID = Umodel.ClientID.Value,
                    CompanyID = payouts.Insurer,
                    FromRange = payouts.RangeFrom,
                    ToRange = payouts.RangeTo,
                    ID = 0,
                    Payout = payouts.Payout,
                    TypeOfPayout = payouts.IRDA,
                    Product = payouts.Product,
                    ProductOption = payouts.ProductOption
                });
            }
            Response = insurerpayout.InsertRecordList(Insurerlist);
            return Response;
        }
        private string AddUpdateData(Insurerpayout data)
        {
            string Response = "";
            IDataLayer<Insurerpayout> insurerpayout = new DataLayer<Insurerpayout>();
            var DataExist = insurerpayout.GetSingelDetailWithCondition(row =>
                row.ClientID == data.ClientID && row.Product == data.Product &&
                row.FromRange == data.FromRange && row.ToRange == data.ToRange
            );
            insurerpayout = null;
            insurerpayout = new DataLayer<Insurerpayout>();
            if (DataExist == null)
            {
                Response = insurerpayout.InsertRecord(data);
            }
            else
            {
                DataExist.FromRange = data.FromRange;
                DataExist.ToRange = data.ToRange;
                DataExist.Payout = data.Payout;
                Response = insurerpayout.Update(DataExist);
            }
            ReleaseObject<IDataLayer<Insurerpayout>>(ref insurerpayout);
            ReleaseObject<Insurerpayout>(ref data);
            return Response;
        }
        public string DelPayoutData(RemovePayoutData Item)
        {
            string Response = "";
            IDataLayer<Insurerpayout> Data = new DataLayer<Insurerpayout>();
            Response = Data.DeleteRecord(Item.PayoutData);
            return Response;
        }
        public IEnumerable<sp_GetPayoutData> GetPayoutData(GetPayoutData Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_GetPayoutData> Data = new DataLayer<sp_GetPayoutData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_ClientID", Umodel.ClientID.ToString());
            var Response = Data.ProceduresGetData("sp_GetPayoutData", param);
            return Response;
        }
        public string PosExam(QuestionData Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            string Response = "";
            IDataLayer<PosQuestions> Qstndata = new DataLayer<PosQuestions>();
            foreach (Question q in Item.question)
            {
                var CheckExist = Qstndata.GetSingelDetailWithCondition(row => row.Qstn == q.Qstn && row.ClientID == Umodel.ClientID);
                if (CheckExist == null)
                {
                    ReleaseObject<IDataLayer<PosQuestions>>(ref Qstndata);
                    Qstndata = new DataLayer<PosQuestions>();
                    PosQuestions obj = new PosQuestions()
                    {
                        Qstn = q.Qstn,
                        ClientID = Umodel.ClientID.Value,
                        Answer = q.Answer,
                        Option1 = q.Option1,
                        Option2 = q.Option2,
                        Option3 = q.Option3,
                        Option4 = q.Option4
                    };
                    Response += "Question => " + q.Qstn + " : " + Qstndata.InsertRecord(obj) + "\n";
                    ReleaseObject<IDataLayer<PosQuestions>>(ref Qstndata);
                }
                else
                    Response += "Question => " + q.Qstn + " : Question already exist.";
            }
            return Response;
        }
        public IEnumerable<PosQuestions> SavedPosQstnList(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<PosQuestions> Qstndata = new DataLayer<PosQuestions>();
            return Qstndata.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID.Value);
        }
        public string DelPosQstnList(PosQuestionID Item)
        {
            string Response = "";
            IDataLayer<PosQuestions> posQuestions = new DataLayer<PosQuestions>();
            var PosQuesdata = posQuestions.GetSingelDetailWithCondition(row => row.ID == Item.QuestionID);
            if (PosQuesdata != null)
            {
                posQuestions = null;
                posQuestions = new DataLayer<PosQuestions>();
                Response = posQuestions.DeleteRecord(PosQuesdata);
            }
            else
                Response = "Not Found.";
            return Response;
        }
        public string TestMailserver(MailserverSetupParam Item)
        {
            string Message = "";
            Message = Common.mailMaster(Item.FromEmail, Item.FromEmail, "Test Connection", "It is just Test",
                            Item.UserName, Item.PasswordVal, Item.HostName, Item.Port,
                            Item.UseDefaultCredential, Item.EnableSsl, ref Message);
            return Message;
        }
        public string SaveRegion(ManageRegion Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            if (Item.Option.ToLower() == "region")
            {
                IDataLayer<RegionZone> dataLayer = new DataLayer<RegionZone>();
                dataLayer = new DataLayer<RegionZone>();
                if (Item.Action == "Save")
                {
                    var data = dataLayer.GetSingelDetailWithCondition(row => row.Region == Item.Name && row.ClientID == um.ClientID.Value);
                    dataLayer = null;
                    if (data == null)
                    {
                        dataLayer = new DataLayer<RegionZone>();
                        Response = dataLayer.InsertRecord(new RegionZone()
                        {
                            Region = Item.Name,
                            ClientID = um.ClientID.Value,
                            IsActive = true,
                            ID = 0
                        });
                    }
                    else
                        Response = "Already Exist";
                }
                else if (Item.Action == "Update")
                {
                    dataLayer = null;
                    dataLayer = new DataLayer<RegionZone>();
                    var data = dataLayer.GetSingelDetailWithCondition(row => row.ID == Item.RegionID && row.ClientID == um.ClientID.Value);
                    if (data != null)
                    {

                        data.Region = Item.Name;
                        Response = dataLayer.Update(data);
                    }
                    else
                        Response = "Not Found.";
                }
            }
            else if (Item.Option.ToLower() == "branch")
            {
                if (Item.RegionID > 0)
                {
                    IDataLayer<Branchcity> dataLayer = new DataLayer<Branchcity>();

                    dataLayer = new DataLayer<Branchcity>();
                    if (Item.Action == "Save")
                    {
                        var data = dataLayer.GetSingelDetailWithCondition(row => row.CityName == Item.Name && row.ClientID == um.ClientID.Value);
                        dataLayer = null;
                        if (data == null)
                        {
                            dataLayer = new DataLayer<Branchcity>();
                            Response = dataLayer.InsertRecord(new Branchcity()
                            {
                                CityName = Item.Name,
                                ClientID = um.ClientID.Value,
                                RegionID = Item.RegionID,
                                ID = 0
                            }); ;
                        }
                        else
                            Response = "Already Exist";
                    }
                    else if (Item.Action == "Update")
                    {
                        dataLayer = null;
                        dataLayer = new DataLayer<Branchcity>();
                        var data = dataLayer.GetSingelDetailWithCondition(row => row.ID == Item.BranchID && row.ClientID == um.ClientID.Value);
                        data.CityName = Item.Name;
                        Response = dataLayer.Update(data);
                    }
                }
                else
                    Response = "Selecte Region";
            }
            else
                Response = "404 Not Found";
            return Response;

        }
        public IEnumerable<sp_BranchListByRegion> BranchList(ManageRegion Item)
        {
            UserModel model = Common.DecodeToken(Item.Token);
            IDataLayer<sp_BranchListByRegion> Data = new DataLayer<sp_BranchListByRegion>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_RegionID", Item.RegionID.ToString());
            param.Add("@p_ClientID", model.ClientID.ToString());
            var dataList = Data.ProceduresGetData("sp_BranchListByRegion", param);
            return dataList;
        }
        public string Getfile(GetOffilePDF item)
        {
            string Response = "";
            UserModel Um = Common.DecodeToken(item.Token);
            string PolicyNo = "";
            if (item.Product.ToLower() == "mot")
            {
                IDataLayer<Manualofflinepolicy> dataLayer = new DataLayer<Manualofflinepolicy>();
                var Exist = dataLayer.GetSingelDetailWithCondition(row => row.ClientID == Um.ClientID.Value && row.PolicyNo == item.PolicyNo);
                PolicyNo = Exist.PolicyNo;
            }
            else if (item.Product.ToLower() == "hlt")
            {
                IDataLayer<ManualOfflineHealthPolicy> dataLayer = new DataLayer<ManualOfflineHealthPolicy>();
                var Exist = dataLayer.GetSingelDetailWithCondition(row => row.ClientID == Um.ClientID.Value && row.PolicyNo == item.PolicyNo);
                PolicyNo = Exist.PolicyNo;
            }
            if (PolicyNo == "")
                Response = "Not Found.";
            else
            {
                string Path = GetCurrentPath() + "\\Downloads\\ManualOffline\\" + PolicyNo + ".pdf";
                var Check = System.IO.File.Exists(Path);
                if (Check)
                {
                    IDataLayer<ClientMaster> cliendata = new DataLayer<ClientMaster>();
                    var data = cliendata.GetSingelDetailWithCondition(row => row.Id == Um.ClientID.Value);
                    Response = data.CoreAPIURL + "/Downloads/ManualOffline/" + PolicyNo + ".pdf";
                }
                else
                    Response = "File Not Found.";
            }
            return Response;
        }
        public IEnumerable<sp_InactiveUserList> InactivePosList(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_InactiveUserList> Data = new DataLayer<sp_InactiveUserList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", Umodel.UserID.ToString());
            param.Add("@p_ClientID", Umodel.ClientID.ToString());
            param.Add("@p_WithDoc", "0");
            var dataList = Data.ProceduresGetData("sp_InactiveUserList", param);
            return dataList;
        }
        public string MakeActivePos(PosSeparater Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<dynamic> Data = new DataLayer<dynamic>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_PosList", Item.Poses.ToString());
            param.Add("@p_ClientID", Umodel.ClientID.ToString());
            var dataList = Data.ProcedureOutput("sp_ActivePosData", param);
            if (dataList != null)
            {
                if (Convert.ToInt32(dataList) > 0)
                {
                    Response = "Data Updated Successfully.";
                }
                else
                    Response = "Not Found.";
            }
            else
                Response = "Not Found.";
            return Response;
        }
        public string BasePosCertification(BasePosCertificationParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<CertificateBack> certificateBack = new DataLayer<CertificateBack>();
            IDataLayer<ClientMaster> clientMaster = new DataLayer<ClientMaster>();
            var ClientData = clientMaster.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID.Value);
            var baseCertificate = certificateBack.GetDetails().OrderByDescending(row => row.ID).FirstOrDefault();
            var Base = baseCertificate.HTMLData;
            Base = Base.Replace("[Certificatedescriptop1]", Item.Certificatedescriptop1);
            Base = Base.Replace("[Certificatedescriptop2]", Item.Certificatedescriptop2);
            Base = Base.Replace("[Certificatedescriptop3]", Item.Certificatedescriptop3);
            Base = Base.Replace("[Certificatedescriptop4]", Item.Certificatedescriptop4);
            Base = Base.Replace("[Certificatedescriptop5]", Item.Certificatedescriptop5);
            Base = Base.Replace("[HeaderHeight]", Item.HeaderHeight.ToString());
            Base = Base.Replace("[Company]", ClientData.companyName);
            Base = Base.Replace("[YourTruly]", Item.YourTruly);
            Base = Base.Replace("[AuthorizeSign]", ClientData.CoreAPIURL + "/downloads/Certificate/AuthorizeSign/" + ClientData.Id.ToString() + ".jpg");
            Base = Base.Replace("[AuthorisedSignatory]", Item.AuthorisedSignatory);
            Base = Base.Replace("[Headerimage]", ClientData.CoreAPIURL + "/downloads/Certificate/Header/" + ClientData.Id.ToString() + ".jpg");
            Base = Base.Replace("[FooterImage]", ClientData.CoreAPIURL + "/downloads/Certificate/Footer/" + ClientData.Id.ToString() + ".jpg");
            IDataLayer<ClientPosCertificateBase> clientPosCertificateBase = new DataLayer<ClientPosCertificateBase>();
            var exist = clientPosCertificateBase.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value);
            clientPosCertificateBase = null;
            clientPosCertificateBase = new DataLayer<ClientPosCertificateBase>();
            if (exist == null)
            {
                Response = clientPosCertificateBase.InsertRecord(new ClientPosCertificateBase()
                {
                    ClientID = umodel.ClientID.Value,
                    HTMLFormat = Base,
                    ID = 0
                });
            }
            else
            {
                exist.HTMLFormat = Base;
                Response = clientPosCertificateBase.Update(exist);
            }
            return Response;
        }
        public string SMSTestIng(SmsTestParam Item)
        {
            string Response = "";
            string Url = Item.APIUrl.Replace("[to]", Item.MobileNo).Replace("[message]", Item.Message);
            if (Common.SendSms(Url, out Response))
                Response = "Success";
            else
                Response = "Fail : " + Response;
            return Response;
        }
        public IEnumerable<sp_InactiveUserList> InactivePosListWithDocReq(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_InactiveUserList> Data = new DataLayer<sp_InactiveUserList>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", Umodel.UserID.ToString());
            param.Add("@p_ClientID", Umodel.ClientID.ToString());
            param.Add("@p_WithDoc", "1");
            var dataList = Data.ProceduresGetData("sp_InactiveUserList", param);
            return dataList;
        }
        public DocumentWithAPI GetUploadedDoc(UserDocParam Item)
        {
            DocumentWithAPI documentWithAPI = new DocumentWithAPI();
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<UserDocument> userDocument = new DataLayer<UserDocument>();
            var DocData = userDocument.GetSingelDetailWithCondition(row => row.UserID == Item.Userid);
            IDataLayer<OfflineQueryRelatedMessage> offlineQueryRelatedMessage = new DataLayer<OfflineQueryRelatedMessage>();
            var exist = offlineQueryRelatedMessage.GetDetailsWithCondition(row => row.ToUser == Item.Userid && row.Subject == "Document Verification");
            documentWithAPI.offlineQueryRelatedMessages = exist == null ? null : exist.ToList();
            if (DocData != null)
            {
                IDataLayer<UserMaster> umas = new DataLayer<UserMaster>();
                documentWithAPI.ProfilePic = umas.GetSingelDetailWithCondition(row => row.UserID == Item.Userid).UserProfilePic;
                IDataLayer<ClientMaster> clientMaster = new DataLayer<ClientMaster>();
                documentWithAPI.UserDocuments = DocData;
                documentWithAPI.Api = clientMaster.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID.Value).CoreAPIURL;
            }
            return documentWithAPI;
        }
        public string ShareMessage(SendMessageModel Item)
        {
            UserModel userModel = Common.DecodeToken(Item.Token);
            var Respose = SendMessage(userModel.UserID, Item.Message, Item.Subject, Item.ToUserID);
            Respose += "\n" + DocumentRelatedMessage(userModel.ClientID.Value, Item.ToUserID);
            return Respose;
        }
        public string VerfyDocument(User Item)
        {
            string Response = "";
            UserModel userModel = Common.DecodeToken(Item.Token);
            IDataLayer<UserDocument> userDocumentnew = new DataLayer<UserDocument>();
            var ExistDOc = userDocumentnew.GetSingelDetailWithCondition(row => row.UserID == Item.Userid);
            if (ExistDOc != null)
            {
                ExistDOc.DocVerified = true;
                userDocumentnew = null;
                userDocumentnew = new DataLayer<UserDocument>();
                Response = userDocumentnew.Update(ExistDOc);
            }
            else
                Response = "Not Found";
            return Response;
        }
        public string SetupActive(RegionActiveParam Item)
        {
            string Response = "";
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<RegionZone> regionZone = new DataLayer<RegionZone>();
            var Exist = regionZone.GetSingelDetailWithCondition(row => row.ID == Item.RegionID && row.ClientID == um.ClientID.Value);
            if (Exist != null)
            {
                Exist.IsActive = Item.IsAcitve;
                regionZone = null;
                regionZone = new DataLayer<RegionZone>();
                Response = regionZone.Update(Exist);
            }
            else
                Response = "Not Found";
            return Response;
        }
        public string RenewNotificationConfig(RenewNotificationConfigParam Item)
        {
            string Response = "";

            UserModel userModel = Common.DecodeToken(Item.Token);
            IDataLayer<Renewnotificationduration> renewnotificationduration =
                new DataLayer<Renewnotificationduration>();
            List<Renewnotificationduration> lstRenewnotificationduration = new List<Renewnotificationduration>();
            renewnotificationduration.DeleteRecordConditional(row => row.ClientID == userModel.ClientID.Value);

            foreach (var lst in Item.Emails)
            {
                lstRenewnotificationduration.Add(new Renewnotificationduration()
                {
                    ActionWith = "Email",
                    ClientID = userModel.ClientID.Value,
                    Duration = lst.Day,
                    ID = 0,
                    RenewBody = lst.Body
                });
            }
            foreach (var lst in Item.SMSs)
            {
                lstRenewnotificationduration.Add(new Renewnotificationduration()
                {
                    ActionWith = "SMS",
                    ClientID = userModel.ClientID.Value,
                    Duration = lst.Day,
                    ID = 0,
                    RenewBody = lst.Body
                });
            }
            Response = renewnotificationduration.InsertRecordList(lstRenewnotificationduration);

            return Response;
        }
        public IEnumerable<Renewnotificationduration> GetRenewNotificationConfig(CommonParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Renewnotificationduration> renewnotificationduration =
                new DataLayer<Renewnotificationduration>();
            var data = renewnotificationduration.GetDetailsWithCondition(row => row.ClientID == umodel.ClientID.Value);
            return data;
        }
        public NotificationEmail GetNotificationBody(GetNotificationBodyParam Item)
        {
            NotificationEmail obj = null;
            UserModel Mode = Common.DecodeToken(Item.Token);
            IDataLayer<MailServerOption> mailOPT = new DataLayer<MailServerOption>();
            var Exist = mailOPT.GetSingelDetailWithCondition(row => row.ID == Item.MailOptionID);
            if (Exist != null)
            {
                IDataLayer<NotificationEmail> notificationEmail = new DataLayer<NotificationEmail>();
                obj = notificationEmail.GetSingelDetailWithCondition(row => row.ClientID == Mode.ClientID.Value && row.ForAction == Exist.MailServiceMaster);
            }
            return obj;
        }
        public NotificationEmail GetNotificationBodyWithRoleID(GetNotificationBodyWithRoleParam Item)
        {
            NotificationEmail obj = null;
            UserModel Mode = Common.DecodeToken(Item.Token);
            IDataLayer<MailServerOption> mailOPT = new DataLayer<MailServerOption>();
            var Exist = mailOPT.GetSingelDetailWithCondition(row => row.ID == Item.MailOptionID);
            if (Exist != null)
            {
                IDataLayer<NotificationEmail> notificationEmail = new DataLayer<NotificationEmail>();
                obj = notificationEmail.GetSingelDetailWithCondition(row => row.ClientID == Mode.ClientID.Value &&
                                            row.ForAction == Exist.MailServiceMaster && row.RoleID == Item.RoleID);
            }
            return obj;
        }
        public string SaveMailNotificationBody(SaveMailNotificationBodyParam Item)
        {
            string Response = "";
            UserModel Mode = Common.DecodeToken(Item.Token);
            IDataLayer<MailServerOption> mailOPT = new DataLayer<MailServerOption>();
            var Exist = mailOPT.GetSingelDetailWithCondition(row => row.ID == Item.MailOptionID);
            if (Exist != null)
            {
                IDataLayer<NotificationEmail> notificationEmail = new DataLayer<NotificationEmail>();
                NotificationEmail obj = null;
                if (Exist.MailServiceMaster == "CreateUser")
                {
                    if (Item.RoleID == null)
                    {
                        return "Please select role.";
                    }
                    obj = notificationEmail.GetSingelDetailWithCondition(row => row.ClientID == Mode.ClientID.Value
                                                                            && row.ForAction == Exist.MailServiceMaster && row.RoleID == Item.RoleID.Value);
                }
                else
                {
                    obj = notificationEmail.GetSingelDetailWithCondition(row => row.ClientID == Mode.ClientID.Value
                                                                            && row.ForAction == Exist.MailServiceMaster);
                }
                notificationEmail = null;
                notificationEmail = new DataLayer<NotificationEmail>();
                if (obj != null)
                {
                    obj.MailBody = Item.MailBody;
                    obj.MailSubject = Item.Subject;
                    Response = notificationEmail.Update(obj);
                }
                else
                {
                    if (Item.RoleID != null)
                    {
                        NotificationEmail notification = new NotificationEmail()
                        {
                            ClientID = Mode.ClientID.Value,
                            ForAction = Exist.MailServiceMaster,
                            ID = 0,
                            MailBody = Item.MailBody,
                            MailSubject = Item.Subject,
                            RoleID = Item.RoleID.Value
                        };
                        Response = notificationEmail.InsertRecord(notification);
                    }
                    else
                    {
                        NotificationEmail notification = new NotificationEmail()
                        {
                            ClientID = Mode.ClientID.Value,
                            ForAction = Exist.MailServiceMaster,
                            ID = 0,
                            MailBody = Item.MailBody,
                            MailSubject = Item.Subject,
                        };
                        Response = notificationEmail.InsertRecord(notification);
                    }

                }
            }
            else
                Response = "Option Not Found.";
            return Response;
        }
        public string ReplaceUserInfo(ReplaceUserParam Item)
        {
            string Replace = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<UserMaster> umaster = new DataLayer<UserMaster>();
            var UserMasterData = umaster.GetSingelDetailWithCondition(row => row.EmailAddress == Item.ReplaceEmail
                                                                       && row.ClientID == Umodel.ClientID);
            if (UserMasterData != null)
            {
                IDataLayer<UsermasterHistory> userhis = new DataLayer<UsermasterHistory>();
                Replace = userhis.InsertRecord(new UsermasterHistory()
                {
                    CreateDate = DateTime.Now,
                    UserName = UserMasterData.UserName,
                    JoiningDate = UserMasterData.CreatedDate,
                    MobileNo = UserMasterData.MobileNo,
                    ID = 0,
                    RmCode = UserMasterData.RmCode,
                    RoleID = UserMasterData.RoleId,
                    UserEmail = UserMasterData.EmailAddress,
                    UserID = UserMasterData.UserID
                });
                if (Replace == "Data Save Successfully.")
                {
                    UserMasterData.UserName = Item.ReplaceUserName;
                    UserMasterData.CreatedDate = DateTime.Now;
                    UserMasterData.MobileNo = Item.ReplaceMobileNo;
                    UserMasterData.RmCode = Item.ReplaceRmCode;
                    UserMasterData.EmailAddress = Item.ReplaceEmail;
                    umaster = null;
                    umaster = new DataLayer<UserMaster>();
                    Replace = umaster.Update(UserMasterData);
                }
            }
            else
            {
                Replace = "Bad Request";
            }
            return Replace;
        }
        public string UploadPosExamData(string DocName, string Token, IFormFile file, string SummaryName)
        {
            string Response = "";
            UserModel model = Common.DecodeToken(Token);
            switch (DocName)
            {
                case "PosExamFiles":
                    Response = PosExamFile(DocName, file, SummaryName, model);
                    break;
                case "WelcomePOS":
                    Response = WelcomePOS(file, model, DocName);
                    break;
            }
            return Response;
        }
        private string WelcomePOS(IFormFile file, UserModel model, string DocName)
        {
            string Response = "";
            try
            {
                string Filename = "";
                Filename = model.ClientID.ToString() + "_WelcomePOS";
                var GetExtention = Path.GetExtension(file.FileName);
                var fileName = Filename + GetExtention;
                string Url = "/Downloads/" + DocName + "/" + fileName;
                using (var stream = new FileStream(GetCurrentPath() + Url, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                IDataLayer<ClientMaster> client = new DataLayer<ClientMaster>();
                Response = client.GetSingelDetailWithCondition(row => row.Id == model.ClientID.Value).CoreAPIURL + Url;
            }
            catch { }
            return Response;
        }
        private string PosExamFile(string DocName, IFormFile file, string SummaryName, UserModel model)
        {
            string Response = "";
            string Filename = "";
            IDataLayer<Posexamsummary> posexam = new DataLayer<Posexamsummary>();
            var Exist = posexam.GetDetailsWithCondition(row => row.ClientID == model.ClientID);
            if (Exist.Count() > 0)
            {
                Filename = model.ClientID.ToString() + "_PosExam_" +
                    Exist.OrderByDescending(row => row.ID).FirstOrDefault().ID.ToString();
            }
            else
                Filename = model.ClientID.ToString() + "_PosExam_1";
            var GetExtention = Path.GetExtension(file.FileName);
            var fileName = Filename + GetExtention;
            string Url = "/Downloads/" + DocName + "/" + fileName;
            using (var stream = new FileStream(GetCurrentPath() + Url, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            posexam = new DataLayer<Posexamsummary>();
            Response = posexam.InsertRecord(new Posexamsummary()
            {
                ClientID = model.ClientID.Value,
                DocName = Url,
                CreatedDate = DateTime.Now,
                ID = 0,
                FileName = SummaryName
            });
            return Response;
        }
        public IEnumerable<ReturnPageUrls> GetPageURLs(CommonParam item)
        {
            UserModel Umodel = Common.DecodeToken(item.Token);
            List<ReturnPageUrls> Pagedata = new List<ReturnPageUrls>();
            IDataLayer<vw_pageurl> vw_pageurls = new DataLayer<vw_pageurl>();
            Pagedata = vw_pageurls.GetDetails().Select(row => new ReturnPageUrls()
            {
                IsChecked = false,
                Privilegename = row.Privilegename,
                SrNo = row.SrNo,
                URL = row.URL
            }).ToList();
            IDataLayer<ScriptPagePrivilege> scriptPagePrivilege = new DataLayer<ScriptPagePrivilege>();
            var dataPage = scriptPagePrivilege.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID.Value);
            if (dataPage.Count() > 0)
            {
                foreach (var itemdata in Pagedata)
                {
                    itemdata.Script = dataPage.ToList()[0].Script;
                    int count = dataPage.Where(row => row.PageUrl == itemdata.URL).Count();
                    itemdata.IsChecked = count > 0 ? true : false;
                }
            }
            return Pagedata;
        }
        public string SetPageScript(PageScriptParam item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<ScriptPagePrivilege> scriptPagePrivilege = new DataLayer<ScriptPagePrivilege>();
            var dataPage = scriptPagePrivilege.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID.Value);
            foreach (string url in item.Url)
            {
                if (dataPage.Count() > 0)
                {
                    var ContainPage = dataPage.Where(row => row.PageUrl.ToLower() == url.ToLower()).FirstOrDefault();
                    scriptPagePrivilege = null;
                    scriptPagePrivilege = new DataLayer<ScriptPagePrivilege>();
                    if (ContainPage == null)
                    {
                        Response = scriptPagePrivilege.InsertRecord(new ScriptPagePrivilege()
                        {
                            ClientID = Umodel.ClientID.Value,
                            ID = 0,
                            PageUrl = url,
                            Script = item.Script
                        });
                    }
                    else
                    {
                        ContainPage.Script = item.Script;
                        Response = scriptPagePrivilege.Update(ContainPage);
                    }
                }
                else
                {
                    scriptPagePrivilege = null;
                    scriptPagePrivilege = new DataLayer<ScriptPagePrivilege>();
                    Response = scriptPagePrivilege.InsertRecord(new ScriptPagePrivilege()
                    {
                        ClientID = Umodel.ClientID.Value,
                        ID = 0,
                        PageUrl = url,
                        Script = item.Script
                    });
                }
            }
            return Response;
        }
        public string DelPageScript(PageScriptParam item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<ScriptPagePrivilege> scriptPagePrivilege = null;
            foreach (var itemd in item.Url)
            {
                scriptPagePrivilege = new DataLayer<ScriptPagePrivilege>();
                Response = scriptPagePrivilege.DeleteRecordConditional(row => row.PageUrl == itemd);
            }
            return Response;
        }
        public IEnumerable<ScriptPagePrivilege> GetPageScript(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ScriptPagePrivilege> scriptPagePrivilege = new DataLayer<ScriptPagePrivilege>();
            var Response = scriptPagePrivilege.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID);
            return Response;
        }
        public string ConfigSupport(CnfigSupprtParam item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            var Exist = GetConfig(Umodel.ClientID.Value);
            IDataLayer<ConfigureSupport> configureSupport = new DataLayer<ConfigureSupport>();
            if (Exist != null)
            {
                Exist.IsActive = item.IsActive;
                Exist.Link = item.Link;
                Response = configureSupport.Update(Exist);
            }
            else
            {
                Response = configureSupport.InsertRecord(new ConfigureSupport()
                {
                    Link = item.Link,
                    ClientID = Umodel.ClientID.Value,
                    ID = 0,
                    IsActive = item.IsActive
                });
            }
            return Response;
        }
        private ConfigureSupport GetConfig(int ClientID)
        {
            IDataLayer<ConfigureSupport> configureSupport = new DataLayer<ConfigureSupport>();
            var Exist = configureSupport.GetSingelDetailWithCondition(row => row.ClientID == ClientID);
            return Exist;
        }
        public ConfigureSupport CheckConfigSupport(CommonParam item)
        {
            UserModel Umodel = Common.DecodeToken(item.Token);
            var Exist = GetConfig(Umodel.ClientID.Value);
            return Exist;
        }
        public PosUserActiveDuration GetTrainingHours(CommonParam Item)
        {
            UserModel um = Common.DecodeToken(Item.Token);
            IDataLayer<PosUserActiveDuration> posUserActiveDuration = new DataLayer<PosUserActiveDuration>();
            var Response = posUserActiveDuration.GetSingelDetailWithCondition(row => row.ClientID == um.ClientID && row.RoleID == 8);
            return Response;
        }
        public IEnumerable<FeedbackOption> GetFadBackOptions(CommonParam Item)
        {
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<FeedbackOption> feedbackOption = new DataLayer<FeedbackOption>();
            return feedbackOption.GetDetailsWithCondition(row => row.ClientID == Umodel.ClientID);
        }
        public string ConfigureFeedbackOption(FeedbackOptionParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<FeedbackOption> feedbackOption = new DataLayer<FeedbackOption>();
            var Exist = feedbackOption.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID &&
                                                row.Options.Trim().ToLower() == Item.Option.Trim().ToLower());
            feedbackOption = null;
            if (Exist == null)
            {
                feedbackOption = new DataLayer<FeedbackOption>();
                Response = feedbackOption.InsertRecord(new FeedbackOption()
                {
                    ClientID = Umodel.ClientID.Value,
                    ID = 0,
                    Options = Item.Option
                });
            }
            else
                Response = "Option Already Exist.";
            return Response;
        }
        public string RemoveFeedbackOption(FeedbackOptionIDParam Item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(Item.Token);
            IDataLayer<FeedbackOption> feedbackOption = new DataLayer<FeedbackOption>();
            var Exist = feedbackOption.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID &&
                                                row.ID == Item.FeedID);
            feedbackOption = null;
            if (Exist != null)
            {
                feedbackOption = new DataLayer<FeedbackOption>();
                Response = feedbackOption.DeleteRecord(Exist);
            }
            else
                Response = "Option not found.";
            return Response;
        }
        public string SaveDigitalSign(SaveDigitalSignParam item)
        {
            string Response = "";
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<DigitalSignMaster> digitalSignMaster = new DataLayer<DigitalSignMaster>();
            var exist = digitalSignMaster.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value);
            digitalSignMaster = new DataLayer<DigitalSignMaster>();
            if (exist != null)
            {
                exist.SignatureBody = item.DigitalSignBody;
                Response = digitalSignMaster.Update(exist);
            }
            else
            {
                Response = digitalSignMaster.InsertRecord(new DigitalSignMaster()
                {
                    ClientID = Umodel.ClientID.Value,
                    ID = 0,
                    SignatureBody = item.DigitalSignBody
                });
            }
            return Response;
        }
        public DigitalSignMaster GetSaveDigitalSign(CommonParam item)
        {
            UserModel Umodel = Common.DecodeToken(item.Token);
            IDataLayer<DigitalSignMaster> digitalSignMaster = new DataLayer<DigitalSignMaster>();
            var exist = digitalSignMaster.GetSingelDetailWithCondition(row => row.ClientID == Umodel.ClientID.Value);
            return exist;
        }
        #endregion  Master Setup

        #region PortalMasterSetup
        public string SavePosMotorQuoteSetup(SaveMotoRestrictQuoteLstParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_SavePosMotorQuoteSetup> proData = null;
            StringBuilder strMessage = new StringBuilder();
            foreach (var Data in Item.saveRestrictQuote)
            {
                proData = new DataLayer<sp_SavePosMotorQuoteSetup>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_Product", "MOT");
                param.Add("@p_ClientID", umodel.ClientID.Value.ToString());
                param.Add("@p_BrokerName", Data.BrokerName);
                param.Add("@p_OnlyPOS", Data.OnlyPOS ? "1" : "0");
                param.Add("@p_UserOnly", Data.UserOnly ? "1" : "0");
                param.Add("@p_BothUser", Data.Both ? "1" : "0");
                var dataList = proData.ProceduresGetData("sp_SavePosMotorQuoteSetup", param);
                strMessage.Append(dataList.FirstOrDefault().Response);
            }
            return strMessage.ToString();
        }
        public IEnumerable<Apilistpvc> GetClientInsurer(BrokerFetchParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Apilistpvc> apilistpvc = new DataLayer<Apilistpvc>();
            return apilistpvc.GetDetailsWithCondition(row => row.ClientID == umodel.ClientID.Value && row.IsAcitve == true);
        }
        public IEnumerable<Apilisthlt> GetHLTClientInsurer(BrokerFetchParam item)
        {
            UserModel umodel = Common.DecodeToken(item.Token);
            IDataLayer<Apilisthlt> apilisthlt = new DataLayer<Apilisthlt>();
            return apilisthlt.GetDetailsWithCondition(row => row.ClientID == umodel.ClientID.Value && row.IsActive == true);
        }
        public string SavePosHLTQuoteSetup(SaveHLTRestrictQuoteLstParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_SavePosMotorQuoteSetup> proData = null;
            StringBuilder strMessage = new StringBuilder();
            foreach (var Data in Item.saveRestrictQuote)
            {
                proData = new DataLayer<sp_SavePosMotorQuoteSetup>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_Product", "HLT");
                param.Add("@p_ClientID", umodel.ClientID.Value.ToString());
                param.Add("@p_BrokerName", Data.BrokerName);
                param.Add("@p_OnlyPOS", Data.OnlyPOS ? "1" : "0");
                param.Add("@p_UserOnly", Data.UserOnly ? "1" : "0");
                param.Add("@p_BothUser", Data.Both ? "1" : "0");
                param.Add("@p_Suminsured", Data.Suminsured == null ? "0" : Data.Suminsured.ToString());
                var dataList = proData.ProceduresGetData("sp_SavePosHLTQuoteSetup", param);
                strMessage.Append(dataList.FirstOrDefault().Response);
            }
            return strMessage.ToString();
        }
        public IEnumerable<Saveposquotesetup> GetHLTQuoteSetup(BrokerFetchParam Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<Saveposquotesetup> saveposquotesetup = new DataLayer<Saveposquotesetup>();
            return saveposquotesetup.GetDetailsWithCondition(row => row.ClientID == umodel.ClientID.Value && row.Product == Item.Product);
        }
        #endregion PortalMasterSetup

        #region AutoService
        public string SendNotificationForRenew()
        {
            IDataLayer<Renewnotificationduration> renewnotificationduration = new DataLayer<Renewnotificationduration>();
            var dataList = renewnotificationduration.GetDetails();
            List<Thread> lstth = new List<Thread>();
            object obj = new object(); ;
            foreach (var data in dataList)
            {
                lstth.Add(new Thread(() => SendRenewNotification(data)));
            }
            foreach (var thread in lstth)
                thread.Start();
            return "Running Start";
        }
        private void SendRenewNotification(Renewnotificationduration renewnotificationduration)
        {
            string message = "";
            IDataLayer<sp_RenewUserData> Data = new DataLayer<sp_RenewUserData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_Clientid", renewnotificationduration.ClientID.ToString());
            param.Add("@p_durationday", renewnotificationduration.Duration.ToString());
            var dataList = Data.ProceduresGetData("sp_RenewUserData", param);
            if (dataList.Count() > 0)
            {
                IDataLayer<MailServerOption> mailSelectedOption = new DataLayer<MailServerOption>();
                var mailopt = mailSelectedOption.GetSingelDetailWithCondition(row => row.MailServiceMaster == "RenewalNotification");
                if (mailopt != null)
                {
                    if (renewnotificationduration.ActionWith == "Email")
                    {
                        IDataLayer<MailServer> mailServer = new DataLayer<MailServer>();
                        var mail = mailServer.GetSingelDetailWithCondition(row => row.MailserveroptionID == mailopt.ID);
                        if (mail != null)
                        {
                            Parallel.ForEach(dataList, (data) =>
                             {
                                 Common.mailMaster(mail.FromEmail, data.Email.Trim(), "Renewal Notification", renewnotificationduration.RenewBody,
                                     mail.FromEmail, mail.Password, mail.HostName, mail.Port, mail.UseDefaultCredential,
                                     mail.EnableSsl, ref message);
                             });
                        }
                    }
                    else
                    {
                        IDataLayer<SmsServer> mailServer = new DataLayer<SmsServer>();
                        var mail = mailServer.GetSingelDetailWithCondition(row => row.MailServerOptionID == mailopt.ID);
                        if (mail != null)
                        {
                            Parallel.ForEach(dataList, (data) =>
                            {
                                if (data.Mobile != null)
                                {
                                    mail.SMSAPI = mail.SMSAPI.Replace("[to]", data.Mobile);
                                    mail.SMSAPI = mail.SMSAPI.Replace("[message]", renewnotificationduration.RenewBody);
                                    var url = mail.SMSAPI;
                                    Common.SendSms(url, out message);
                                }
                            });
                        }
                    }
                }
            }
        }
        #endregion AutoService
        #region Release Object
        private void ReleaseObject<T>(ref T obj)
        {
            GC.SuppressFinalize(obj);
        }

        #endregion Release Object

        #region EndUserData
        public LoginResponseModel EndLoginUser(Login Model)
        {
            LoginResponseModel obj = new LoginResponseModel();
            IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
            var ContainClient = ClientData.GetSingelDetailWithCondition(row => row.companyURL.ToLower().Contains(Model.ClientURL.ToLower()));
            if (ContainClient != null)
            {
                IDataLayer<sp_EndUserLogin> Data = new DataLayer<sp_EndUserLogin>();
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("@p_logincred", Model.UserID);
                param.Add("@p_ClientID", ContainClient.Id.ToString());
                var dataList = Data.ProceduresGetData("sp_EndUserLogin", param);
                if (dataList.Count() > 0)
                {
                    var ContainUser = dataList.FirstOrDefault();
                    EndUserModel user = new EndUserModel()
                    {
                        ClientID = ContainUser.ClientID,
                        Email = ContainUser.Email,
                        UserID = ContainUser.UserID,
                        UserName = ContainUser.UserName,
                        Mobile = ContainUser.Mobile
                    };
                    obj.Token = EndAuthenticate(user);
                    obj.UserName = ContainUser.UserName;
                    obj.Status = "Success";
                    Random rnd = new Random();
                    int OTP = rnd.Next(6589, 9861);
                    try { EndUserLoginOTP(user, OTP); } catch { }
                    try { EndUserLoginSMSOtp(user, OTP); } catch { }
                    IDataLayer<EnduserToken> enduserToken = new DataLayer<EnduserToken>();
                    var Exist = enduserToken.GetSingelDetailWithCondition(row => row.Token == obj.Token);
                    if (Exist != null)
                    {
                        enduserToken = null;
                        enduserToken = new DataLayer<EnduserToken>();
                        Exist.OTP = OTP.ToString();
                        enduserToken.Update(Exist);
                    }

                }
                else
                    obj.Status = "Not Found";
            }
            return obj;
        }
        private string EndUserLoginOTP(EndUserModel endUserModel, int OTP)
        {
            string Message = "";
            IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
            var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster == "EndUserLoginOTP");
            if (mailoptiondata != null)
            {
                IDataLayer<MailServer> MailServerList = new DataLayer<MailServer>();
                var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == endUserModel.ClientID
                                                                            && row.MailserveroptionID == mailoptiondata.ID);
                if (mailServer != null)
                {
                    IDataLayer<NotificationEmail> NotificationEmailList = new DataLayer<NotificationEmail>();
                    var notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "EndUserLoginOTP"
                                            && row.ClientID == endUserModel.ClientID);
                    if (notificationEmail != null)
                    {
                        notificationEmail.MailBody = notificationEmail.MailBody.Replace("[OTP]", OTP.ToString());
                        Common.mailMaster(mailServer.FromEmail, endUserModel.Email, notificationEmail.MailSubject, notificationEmail.MailBody,
                            mailServer.UserName, mailServer.Password, mailServer.HostName, mailServer.Port,
                            mailServer.UseDefaultCredential, mailServer.EnableSsl, ref Message);
                    }
                    else
                    {
                        Message = "Mail body not configured.";
                    }
                }
                else
                {
                    Message = "Mail not configured.";
                }
            }
            else
            {
                Message = "Mail Option not configured.";
            }
            return Message;
        }
        private string EndUserLoginSMSOtp(EndUserModel endUserModel, int OTP)
        {
            string Response = "";
            IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
            var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster == "EndUserLoginOTP");
            if (mailoptiondata != null)
            {
                IDataLayer<SmsServer> MailServerList = new DataLayer<SmsServer>();
                NotificationSMS notificationEmail = null;
                var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == endUserModel.ClientID && row.MailServerOptionID == mailoptiondata.ID);
                if (mailServer != null)
                {
                    IDataLayer<NotificationSMS> NotificationEmailList = new DataLayer<NotificationSMS>();
                    notificationEmail = NotificationEmailList.GetSingelDetailWithCondition(row => row.ForAction == "EndUserLoginOTP" && row.ClientID == endUserModel.ClientID);
                    if (notificationEmail != null)
                    {
                        IDataLayer<ClientMaster> ClientData = new DataLayer<ClientMaster>();
                        var client = ClientData.GetSingelDetailWithCondition(row => row.Id == endUserModel.ClientID);
                        notificationEmail.MsgBody = notificationEmail.MsgBody.Replace("[OTP]", OTP.ToString());
                    }
                }
                mailServer.SMSAPI = mailServer.SMSAPI.Replace("[to]", endUserModel.Mobile);
                mailServer.SMSAPI = mailServer.SMSAPI.Replace("[message]", notificationEmail.MsgBody);
                var url = mailServer.SMSAPI;
                Common.SendSms(url, out Response);
            }
            return "successfully sent";
        }
        public string MatchEnduserOTP(Login Model)
        {
            IDataLayer<EnduserToken> enduserToken = new DataLayer<EnduserToken>();
            var exist = enduserToken.GetSingelDetailWithCondition(row => row.Token == Model.UserID && row.OTP == Model.OTP);
            enduserToken = null;
            enduserToken = new DataLayer<EnduserToken>();
            if (exist != null)
            {
                exist.OTP = null;
                enduserToken.Update(exist);
                return "Success";
            }
            else
                return "Invalid OTP.";
        }
        public string CheckEndUserToken(string Token)
        {
            IDataLayer<EnduserToken> ut = new DataLayer<EnduserToken>();
            var IsExist = ut.GetSingelDetailWithCondition(row => row.Token == Token);
            ReleaseObject<IDataLayer<EnduserToken>>(ref ut);
            if (IsExist != null)
                return "Ok";
            else
                return "Not";
        }
        public IEnumerable<sp_EndUserData> GetMotordata(EndUserProductDetailsParam Item)
        {
            EndUserModel Um = Common.EndUserDecodeToken(Item.Token);
            IDataLayer<sp_EndUserData> cities = new DataLayer<sp_EndUserData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EndUserEmail", Um.Email.ToString());
            param.Add("@p_Clientid", Um.ClientID.Value.ToString());
            param.Add("@p_MotorType", Item.Product);
            var Response = cities.ProceduresGetData("sp_EndUserData", param);
            return Response;
        }
        public IEnumerable<sp_EndUserhealthData> GetHealthdata(EndUserProductDetailsParam Item)
        {
            EndUserModel Um = Common.EndUserDecodeToken(Item.Token);
            IDataLayer<sp_EndUserhealthData> cities = new DataLayer<sp_EndUserhealthData>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_EndUserEmail", Um.Email.ToString());
            param.Add("@p_Clientid", Um.ClientID.Value.ToString());
            var Response = cities.ProceduresGetData("sp_EndUserhealthData", param);
            return Response;
        }
        private string EndAuthenticate(EndUserModel user)
        {
            // generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Startup.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.ClientID.ToString()),
                    new Claim(ClaimTypes.UserData, user.UserID.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Token = tokenHandler.WriteToken(token);
            // remove password before returning
            EnduserToken obj = new EnduserToken()
            {
                ID = 0,
                Token = Token,
                UserID = user.UserID
            };
            IDataLayer<EnduserToken> ut = new DataLayer<EnduserToken>();
            var Response = ut.InsertRecord(obj);
            return Token;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion EndUserDataEnd
        #region SFTP
        public string GetMotorBusinessReport(FromDateToDate param)
        {
            try
            {
                string Query = "";
                if (param.ProductType.ToLower() == "motor")
                {
                    Query = "call sp_DownloadMotorpolicyReport(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                }
                else if (param.ProductType.ToLower() == "hlt")
                {
                    var GetTableDataRec = "";
                    //Query = "call sp_DownloadHealthpolicyReport(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                    Query = "call sp_DownloadHealthpolicyReport(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec = GetTableData(Query);
                    Query = "call sp_DownloadManualOfflineHLTBusiness(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                    GetTableDataRec += GetTableData(Query, "Block");

                    return GetTableDataRec;
                }
                else if (param.ProductType.ToLower() == "gcvpcv")
                {
                    Query = "call sp_DownloadComMotorpolicyReport(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                }
                return GetTableData(Query);
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }
        public string GetUserDataReport(int UserID, int Client, string Option, DateTime StartDate, DateTime EndDate)
        {
            string path = "";
            string Qry = "";
            if (Option == "noniib")
                Qry = "call sp_DownloadUserList(" + UserID + "," + Client + ",'" + StartDate.ToString("yyyy-MM-dd") + "','" + EndDate.ToString("yyyy-MM-dd") + "')";
            else if (Option == "iib")
                Qry = "call sp_DownloadUserListIIB(" + UserID + "," + Client + ",'" + StartDate.ToString("yyyy-MM-dd") + "','" + EndDate.ToString("yyyy-MM-dd") + "')";
            path = GetTableData(Qry);
            return path;
        }
        //public string GetOfflineBusinessReport(FromDateToDate param)
        //{
        //    string Query = "";
        //    if (param.ProductType.ToLower() == "motor")
        //    {
        //        Query = "call sp_ManualOfflineBusiness(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
        //        return GetTableData(Query);
        //    }
        //    return null;
        //}
        public string GetOfflineBusinessReport(FromDateToDate param)
        {
            string Query = "";
            if (param.ProductType.ToLower() == "motor")
            {
                Query = "call sp_DOwnloadMotorManualOfflieReport(" + param.Userid + "," + param.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                return GetTableData(Query);
            }
            return null;
        }

        public string SendSFTPMail(SFTPMail param)
        {
            string Message = "";
            IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
            var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster == param.MailService);
            if (mailoptiondata != null)
            {
                IDataLayer<MailServer> MailServerList = new DataLayer<MailServer>();
                var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == param.ClientID && row.MailserveroptionID == mailoptiondata.ID);
                if (mailServer != null)
                {
                    if (String.IsNullOrEmpty(param.ToEmail))
                    {
                        IDataLayer<UserMaster> Umas = new DataLayer<UserMaster>();
                        var UserInfo = Umas.GetSingelDetailWithCondition(row => row.ClientID == param.ClientID && row.UserID == param.Userid);
                        param.ToEmail = UserInfo.EmailAddress;
                    }
                    Common.mailMaster(mailServer.FromEmail, param.ToEmail, param.MailSubject, param.MailBody,
                            mailServer.UserName, mailServer.Password, mailServer.HostName, mailServer.Port,
                            mailServer.UseDefaultCredential, mailServer.EnableSsl, ref Message);
                    return Message;
                }
            }
            return null;
        }
        #endregion SFTP
        private string GetCurrentPath()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

        #region Offline Life Policy
        public string OfflineLifePolicy(OfflineLifePolicyParam Item)
        {
            string Response = "";
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<ManualOfflineLifePolicy> offlineLifePolicy = new DataLayer<ManualOfflineLifePolicy>();
            var exist = offlineLifePolicy.GetSingelDetailWithCondition(row => row.PolicyNumber == Item.PolicyNumber && row.ClientID == umodel.ClientID.Value);
            if (exist == null)
            {
                Response = offlineLifePolicy.InsertRecord(new ManualOfflineLifePolicy()
                {
                    Address = Item.Address,
                    BusinessType = Item.BusinessType,
                    CityID = Item.CityID,
                    ClientID = umodel.ClientID.Value,
                    CreatedID = umodel.UserID,
                    CustName = Item.CustName,
                    DOB = Item.DOB,
                    Email = Item.Email,
                    EndDate = Item.EndDate,
                    Enquiryno = Item.Enquiryno,
                    UserID = Item.UserID,
                    Entrydate = DateTime.Now,
                    GST = Item.GST,
                    Insurer = Item.Insurer,
                    InsurerID = Item.InsurerID,
                    IsPayoutDone = false,
                    PayoutDate = null,
                    MobileNo = Item.MobileNo,
                    Modifydate = null,
                    NetPremium = Item.NetPremium,
                    PhoneNo = Item.PhoneNo,
                    Pin = Item.Pin,
                    PolicyIssueDate = Item.PolicyIssueDate,
                    PolicyNumber = Item.PolicyNumber,
                    PolicyTerm = Item.PolicyTerm,
                    POSCode = Item.POSCode,
                    POSName = Item.POSName,
                    POSPProduct = Item.POSPProduct,
                    POSSource = Item.POSSource,
                    PremiumPayingFrequency = Item.PremiumPayingFrequency,
                    PremiumPayingTerm = Item.PremiumPayingTerm,
                    Product = Item.Product,
                    ProductIssuanceType = Item.ProductIssuanceType,
                    ProductName = Item.ProductName,
                    ProductType = Item.ProductType,
                    RegionalManagerName = Item.RegionalManagerName,
                    ReportingManagerName = Item.ReportingManagerName,
                    StartDate = Item.StartDate,
                    StateID = Item.StateID,
                    SumAssured = Item.SumAssured,
                    TotalPremium = Item.TotalPremium
                });
            }
            else
            {
                exist.Address = Item.Address;
                exist.BusinessType = Item.BusinessType;
                exist.CityID = Item.CityID;
                exist.CustName = Item.CustName;
                exist.DOB = Item.DOB;
                exist.Email = Item.Email;
                exist.EndDate = Item.EndDate;
                exist.GST = Item.GST;
                exist.Insurer = Item.Insurer;
                exist.InsurerID = Item.InsurerID;
                exist.UserID = Item.UserID;
                exist.MobileNo = Item.MobileNo;
                exist.Modifydate = DateTime.Now;
                exist.NetPremium = Item.NetPremium;
                exist.PhoneNo = Item.PhoneNo;
                exist.Pin = Item.Pin;
                exist.PolicyIssueDate = Item.PolicyIssueDate;
                exist.PolicyNumber = Item.PolicyNumber;
                exist.PolicyTerm = Item.PolicyTerm;
                exist.POSCode = Item.POSCode;
                exist.POSName = Item.POSName;
                exist.POSPProduct = Item.POSPProduct;
                exist.POSSource = Item.POSSource;
                exist.PremiumPayingFrequency = Item.PremiumPayingFrequency;
                exist.PremiumPayingTerm = Item.PremiumPayingTerm;
                exist.Product = Item.Product;
                exist.ProductIssuanceType = Item.ProductIssuanceType;
                exist.ProductName = Item.ProductName;
                exist.ProductType = Item.ProductType;
                exist.RegionalManagerName = Item.RegionalManagerName;
                exist.ReportingManagerName = Item.ReportingManagerName;
                exist.StartDate = Item.StartDate;
                exist.StateID = Item.StateID;
                exist.SumAssured = Item.SumAssured;
                exist.TotalPremium = Item.TotalPremium;
                Response = offlineLifePolicy.Update(exist);
            }
            return Response;
        }

        public string UploadBulkOfflineLifeBusiness(BulkOfflineLifeParam param)
        {
            string Response = "";
            StringBuilder ResponseData = new StringBuilder();
            UserModel umodel = Common.DecodeToken(param.Token);
            IDataLayer<ClientMaster> client = new DataLayer<ClientMaster>();
            var Clientinfo = client.GetSingelDetailWithCondition(row => row.Id == umodel.ClientID);
            int userID = 0;
            IDataLayer<ManualOfflineLifePolicy> offlineLifePolicy = null;
            foreach (var item in param.bulkLifeBusinessList)
            {
                IDataLayer<UserMaster> umaster = new DataLayer<UserMaster>();
                var CheckUser = umaster.GetSingelDetailWithCondition(row => row.EmailAddress.Trim().ToLower() == item.UserEmail.Trim().ToLower());
                if (CheckUser != null)
                {
                    userID = CheckUser.UserID;
                }
                IDataLayer<Companies> companies = new DataLayer<Companies>();
                var Comp = companies.GetSingelDetailWithCondition(row => row.CompanyName.ToLower() == item.InsurerName.ToLower());
                offlineLifePolicy = new DataLayer<ManualOfflineLifePolicy>();
                IDataLayer<Cities> cityLayer = new DataLayer<Cities>();
                var cityData = cityLayer.GetSingelDetailWithCondition(row => row.CityName.ToLower().Trim() == item.City.ToLower().Trim());
                if (cityData != null)
                {
                    try
                    {
                        Response = offlineLifePolicy.InsertRecord(new ManualOfflineLifePolicy()
                        {
                            ClientID = umodel.ClientID.Value,
                            CreatedID = umodel.UserID,
                            UserID = userID,
                            InsurerID = Comp.CompanyID,
                            POSCode = item.POSCode,
                            POSName = item.POSName,
                            POSSource = item.POSSource,
                            ReportingManagerName = item.ReportingManagerName,
                            RegionalManagerName = item.RegionalManagerName,
                            CustName = item.CustName,
                            Address = item.Address,
                            CityID = cityData.CityID,
                            Pin = item.Pin,
                            StateID = cityData.StateID,
                            Entrydate = DateTime.Now,
                            PhoneNo = item.PhoneNo,
                            MobileNo = item.MobileNo,
                            Email = item.Email,
                            DOB = Convert.ToDateTime(item.DOB),
                            ProductType = item.ProductType,
                            Product = item.Product,
                            ProductName = item.ProductName,
                            PolicyTerm = Convert.ToInt32(item.PolicyTerm),
                            PremiumPayingTerm = Convert.ToInt32(item.PremiumPayingTerm),
                            PremiumPayingFrequency = item.PremiumPayingFrequency,
                            BusinessType = item.BusinessType,
                            PolicyNumber = item.PolicyNumber,
                            StartDate = Convert.ToDateTime(item.StartDate),
                            EndDate = Convert.ToDateTime(item.EndDate),
                            PolicyIssueDate = Convert.ToDateTime(item.PolicyIssueDate),
                            SumAssured = Convert.ToDecimal(item.SumAssured),
                            NetPremium = Convert.ToDecimal(item.NetPremium),
                            GST = Convert.ToDecimal(item.GST),
                            TotalPremium = Convert.ToDecimal(item.TotalPremium),
                            Enquiryno = item.Enquiryno,
                            ProductIssuanceType = item.ProductIssuanceType,
                            POSPProduct = item.POSPProduct,
                        });
                    }
                    catch (Exception ex)
                    {
                        Response = ex.Message;
                    }
                }
                else
                    Response = "City Not Found.";
                ResponseData.Append(item.PolicyNumber + " :" + Response + "\n");
                Thread.Sleep(100);
            }
            return ResponseData.ToString();
        }

        public IEnumerable<sp_ManualOfflieTermLifeReport> ManualOfflieTermLifeReport(BusinessReportReq Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);
            IDataLayer<sp_ManualOfflieTermLifeReport> Data = new DataLayer<sp_ManualOfflieTermLifeReport>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@p_UserID", umodel.UserID.ToString());
            param.Add("@p_ClientID", umodel.ClientID.ToString());
            param.Add("@P_Fromdate", Item.FromDate.ToString("yyyy-MM-dd"));
            param.Add("@P_ToDate", Item.ToDate.ToString("yyyy-MM-dd"));
            var dataList = Data.ProceduresGetData("sp_ManualOfflieTermLifeReport", param);
            return dataList;
        }

        public string DownloadOfflineLifeReport(BusinessReportReq param)
        {
            try
            {
                string GetTableDataRec = "";
                string Query = "";
                UserModel model = Common.DecodeToken(param.Token);
                Query = "call sp_ManualOfflieTermLifeReportDownload(" + model.UserID + "," + model.ClientID + ",'" + param.FromDate.ToString("yyyy-MM-dd") + "','" + param.ToDate.ToString("yyyy-MM-dd") + "')";
                GetTableDataRec = GetTableData(Query);

                return FileReturnUrl(GetTableDataRec, model.UserID, model.ClientID.Value, param.Product + "_");
            }
            catch (Exception ex) { return ex.Message; }
        }

        public object OfflineLifeHeader(CommonParam token)
        {
            IDataLayer<sp_LifeManualbusinessReportHeader> Datamanual = new DataLayer<sp_LifeManualbusinessReportHeader>();

            UserModel umodel = Common.DecodeToken(token.Token);
            var param = new Dictionary<string, string>();
            param.Add("@p_userid", umodel.UserID.ToString());
            param.Add("@p_clientid", umodel.ClientID.ToString());
            var dataListManual = Datamanual.ProceduresGetData("sp_LifeManualbusinessReportHeader", param);
            if (dataListManual.Any())
            {
                return new
                {
                    TodayCollection = dataListManual.FirstOrDefault().TodayCollection,
                    TodayNoPS = dataListManual.FirstOrDefault().TodayNoPS,
                    TotalCollection = dataListManual.FirstOrDefault().TotalCollection,
                    TotalNOP = dataListManual.FirstOrDefault().TotalNOP
                };
            }
            return null;
        }

        public ManualOfflineLifePolicy GetOfflineLifePolicyInfo(OfflinePolicy Item)
        {
            UserModel umodel = Common.DecodeToken(Item.Token);

            IDataLayer<ManualOfflineLifePolicy> manualLife = new DataLayer<ManualOfflineLifePolicy>();
            var data = manualLife.GetSingelDetailWithCondition(row => row.ClientID == umodel.ClientID.Value && row.PolicyNumber == Item.PolicyNo);
            return data;
        }
        #endregion

        #region Zoho Signup
        /*
         -By: Sunil
         -Update on : 31 Aug 2021
         */
        public List<sp_POSUserListForZoho> GetUsersforDocSign(FromDateToDate param)
        {
            try
            {
                int clientId = param.ClientID;
                if (!string.IsNullOrEmpty(param.Token))
                {
                    UserModel umodel = Common.DecodeToken(param.Token);
                    if (umodel.ClientID.HasValue)
                    {
                        clientId = umodel.ClientID.Value;
                    }
                }

                IDataLayer<sp_POSUserListForZoho> posRepo = new DataLayer<sp_POSUserListForZoho>();
                Dictionary<string, string> parm = new Dictionary<string, string>();
                parm.Add("@p_ClientID", clientId.ToString());
                var activePosList = posRepo.ProceduresGetData("sp_POSUserListForZoho", parm);
                return activePosList.ToList();
            }
            catch { }
            return new List<sp_POSUserListForZoho>();
        }

        public string POSMailingForStamp(POSMailingIn param)
        {
            string Message = "";
            try
            {
                IDataLayer<sp_POSUserList> posRepo = new DataLayer<sp_POSUserList>();
                IDataLayer<PosStampMaster> posStmp = new DataLayer<PosStampMaster>();
                List<PosStampMaster> posStmpList = new List<PosStampMaster>();
                IDataLayer<UserMaster> userMasters = new DataLayer<UserMaster>();
                //Getting activated POS and fill to PosStaMaster
                try
                {
                    Dictionary<string, string> parm = new Dictionary<string, string>();
                    parm.Add("@p_ClientID", param.ClientID.ToString());
                    parm.Add("@p_StartDate", param.StartDate.ToString("yyyy-MM-dd"));
                    parm.Add("@p_EndDate", param.EndDate.ToString("yyyy-MM-dd"));
                    var activePosList = posRepo.ProceduresGetData("sp_POSUserList", parm);

                    foreach (var item in activePosList)
                    {
                        var posObj = posStmp.GetSingelDetailWithCondition(x => x.UserID == item.UserID);
                        if (posObj == null)
                        {
                            posStmpList.Add(new PosStampMaster
                            {
                                POSCode = item.PosCode,
                                POSName = item.UserName,
                                SentToZoho = false,
                                UserID = item.UserID,
                                Entrydate = DateTime.Now,
                                PosActiveDate=item.Timing
                            });
                        }
                        else
                        {
                            posObj.POSCode = item.PosCode;
                            posObj.POSName= item.UserName;
                            posObj.PosActiveDate = item.Timing;
                            posStmp.Update(posObj);
                        }
                    }
                    if (posStmpList.Count > 0)
                    {
                        var res = posStmp.InsertRecordList(posStmpList);
                    }
                }
                catch (Exception)
                {

                }
                DataTable dt = new DataTable();
                dt.Columns.Add("POSCode");
                dt.Columns.Add("POSName");
                dt.Columns.Add("POSActiveDate");
                dt.Columns.Add("Email");
                dt.Columns.Add("MobileNo");
                dt.Columns.Add("Address");
                dt.Columns.Add("PanNo");
                dt.Columns.Add("AadharNo");
                dt.Columns.Add("DateOfBirth");
                dt.Columns.Add("SignDate");
                dt.Columns.Add("StampID");

                var posListToEmail = posStmp.GetDetailsWithCondition(x => x.SentToZoho == false && String.IsNullOrEmpty(x.StampID));
                
                foreach (var item in posListToEmail)
                {
                    var user = userMasters.GetSingelDetailWithCondition(x => x.UserID == item.UserID);

                    DataRow dr = dt.NewRow();
                    dr["POSCode"] = String.IsNullOrEmpty(item.POSCode)?"--": item.POSCode;
                    dr["POSName"] = String.IsNullOrEmpty(item.POSName)?"--": item.POSName;
                    dr["POSActiveDate"] = item.PosActiveDate.HasValue ? item.PosActiveDate.Value.ToString("MM/dd/yyyy") : "--" ;
                    if (user != null)
                    {
                        dr["Email"] = String.IsNullOrEmpty(user.EmailAddress)?"--": user.EmailAddress;
                        dr["MobileNo"] = String.IsNullOrEmpty(user.MobileNo) ? "--" : user.MobileNo;
                        dr["Address"] = String.IsNullOrEmpty(user.Address) ? "--" : user.Address;
                        dr["PanNo"] = String.IsNullOrEmpty(user.PANNumber) ? "--" : user.PANNumber;
                        dr["AadharNo"] = user.AdhaarNumber.HasValue? user.AdhaarNumber.ToString():"--" ;
                        dr["DateOfBirth"] = user.DOB.HasValue? user.DOB.Value.ToString("MM/dd/yyyy") : "--";
                    }
                    dt.Rows.Add(dr);
                }

                string csvString = GenerateCSVFromTable(dt);
                using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(csvString)))
                {
                    //Add a new attachment to the E-mail message, using the correct MIME type
                    Attachment attachment = new Attachment(stream, new ContentType("text/csv"));
                    attachment.Name = String.Format("POSSignup_{0:yyyyMMdd}_{1:yyyyMMdd}.csv", param.StartDate, param.EndDate);

                    IDataLayer<MailServerOption> mailOption = new DataLayer<MailServerOption>();
                    var mailoptiondata = mailOption.GetSingelDetailWithCondition(row => row.MailServiceMaster == param.MailService);
                    if (mailoptiondata != null)
                    {
                        IDataLayer<MailServer> MailServerList = new DataLayer<MailServer>();
                        var mailServer = MailServerList.GetSingelDetailWithCondition(row => row.ClientID == param.ClientID && row.MailserveroptionID == mailoptiondata.ID);
                        if (mailServer != null)
                        {
                            Message = Common.MailMasterWithAttachment(mailServer.FromEmail, param.MailTo, param.MailSubject, param.MailBody,
                                       mailServer.UserName, mailServer.Password, mailServer.HostName, mailServer.Port,
                                       mailServer.UseDefaultCredential, mailServer.EnableSsl, attachment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "Exception:" + ex.Message + Environment.NewLine + ex.StackTrace;
            }
            return Message;
        }

        private string GenerateCSVFromTable(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            //headers    
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i]);
                if (i < dt.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(Environment.NewLine);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value).Replace("\n", "").Replace("\r", "");
                            sb.Append(value);
                        }
                        else
                        {
                            sb.Append(dr[i].ToString().Replace("\n", "").Replace("\r", ""));
                        }
                    }
                    if (i < dt.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
        public string POSStampUpdate(POSUpload param)
        {
            IDataLayer<PosStampMaster> posStmp = new DataLayer<PosStampMaster>();
            StringBuilder sbErrors = new StringBuilder();
            foreach (var item in param.posList)
            {
                try
                {
                    var posObj = posStmp.GetSingelDetailWithCondition(x => x.POSCode == item.POSCode.Trim());
                    if (posObj != null)
                    {
                        posObj.StampID = item.StampID;
                        posObj.SignDate = item.SignDate;
                    }
                    posStmp.Update(posObj);
                }
                catch (Exception ex)
                {
                    sbErrors.Append(string.Format("Error: POS code: {0} not updated" + Environment.NewLine, item.POSCode));
                }
            }
            sbErrors.Append("Done");
            return sbErrors.ToString();
        }
        public string POSZohoStatusUpdate(int UserId)
        {
            IDataLayer<PosStampMaster> posStmp = new DataLayer<PosStampMaster>();
            try
            {
                var posObj = posStmp.GetSingelDetailWithCondition(x => x.UserID == UserId);
                if (posObj != null)
                {
                    posObj.SentToZoho = true;

                    posStmp.Update(posObj);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Done";
        }
        #endregion
    }
}
