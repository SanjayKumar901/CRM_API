using API.CommonMethods;
using API.DbManager.DbModels;
using API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.BAL
{
    public interface IBusinessLayer:IDisposable
    {
        #region Home Controller
        IEnumerable<SP_PRIVILAGES> UserPrevileges(Privilege param);
        sp_DashboardHeader DahsboardHeader(ClientUser param);
        IEnumerable<sp_LeadDetailGrid> DashboardLeadGrid(FromDateToDate item);
        IEnumerable<vw_RegionZone> RegionZones();
        IEnumerable<SP_BUSINESSBYCITY> BUSINESSBYCITY(BusinessbyCityParam item);
        sp_QuoteInfo EnquiryInfo(EnquiryInfo item);
        sp_AfterQuoteInfo EnquiryInfoAfterQuote(EnquiryInfo item);
        sp_hltQouteInfo HltEnquiryInfo(EnquiryInfo item);
        sp_HltAfterQuoteInfo HltEnquiryInfoAfterQuote(EnquiryInfo item);
        Response_Register_as_pos GetPosRegistrationData(RegisterPosParam Item);
        RMWIthMobile AcitveOrNot(CommonParam Item);
        PosExamStartBase CheckHourTestProcess(CommonParam Item);
        bool PosTrainingDone(CompleteTraining Item);
        string ForFurtherProcess(ExamProcessParam Item);
        string PosExamDone(CompleteExam item);
        IEnumerable<sp_ClaimDetails> Claim(RegisterPosParam Item);
        IEnumerable<sp_SharedDetails> Sharedatadetail(RegisterPosParam Item);
        string GetClientLogo(CommonParam Item);
        IEnumerable<sp_Logintimehistory> GetLoginHistory(LoginTimeHis Item);
        string DownloadLoginHistory(LoginTimeHis item);
        string LeadDownload(BusinessReportReq param);
        GotoProposalRespose CheckForGotoProposal(CheckForGotoProposalParam Item);
        string Campaigns(CamapignsParam Item);
        IEnumerable<vw_Uploadpolicy> GetOfflineFeatureLead(GetOfflineFeatureParam Item);
        string SaveOfflineMotorQuote(SaveOfflineMotorQuoteParam Item);
        string SaveOfflineHealthQuote(SaveOfflineHealthQuoteParam Item);
        IEnumerable<sp_OfflineLead> DashbordOfflineleadGrid(CommonParam Item);
        string OfflineGotoPayment(OfflineGotoPayment Item);
        IEnumerable<PosQuestions> PosExamQuestions(CommonParam Item);
        string QuoteRelatedMessage(OfflineQueryMessage Item);
        int QuoteRelatedMessageCounter(CommonParam Item);
        IEnumerable<OfflineQueryRelatedMessage> QuoteRelatedMessageList(CommonParam Item);
        string ReadedMessage(OfflineQueryUpdateMessage Item);
        IEnumerable<sp_privilegelist> DashoardModulePrivilege(CommonParam Item);
        string GetPosRqstDownload(BusinessReportReq Item);
        string AddFadBack(AddFadBackParam Item);
        IEnumerable<sp_GetUserBirthdayList> BirthdayUserList(CommonParam Item);
        string SendBirthdayMessages(BithdayWisheshParam item);
        #endregion
        #region User Controller
        public IEnumerable<RoleType> RoleTypes();
        public IEnumerable<sp_Rolelist> RoleTypeWithID(RoleTypeParam role);
        public IEnumerable<sp_RoleMastWithClientID> RoleTeamList(RoleTypeParam role);
        public IEnumerable<sp_TeamUserList> TeamUserList(TeamUserListParam param);
        public IEnumerable<sp_GetCitiesWithRegionid> GetCities(RoleTypeParam item);
        public IEnumerable<sp_RegionZoneWithClientid> GetRegions(CommonParam Item);
        public IEnumerable<UserRegionBranch> UserAllocation(BaseParam param);
        public string UserMasterSave(UserCreationParam Item);
        public LoginResponseModel LoginUser(Login Model);
        public LoginResponseModel LoginWithOTP(Login Model);
        public string CheckExistOrNot(Login Model);
        //public UserModel LoginUser(Login Model);
        PrivilegeListWithRoleid PrivilegeList(User Param);
        public IEnumerable<sp_CreateByList> UserListWithUserID(FilterUserByDate item);
        public sp_UserInfo UserInfo(User param);
        public IEnumerable<sp_UserInfo> SubUserList(User param);
        public Myprofile MyProfile(User param);
        public string PayoutData(PayyoutParam param);
        public string PersonalDetails(PersonalDetails param);
        public string Gstin(GSTINParam param);
        public string GSTCeritificate(GSTCertificateParam param);
        public string DocUrls(DocumentsParam param, string DocName);
        public string checkToken(string Token);
        string SavePrivileges(PrivilegeMap param);
        string DeletePrivileges(PrivilegeMap param);
        string Deregister(User param);
        sp_ReferAndPosSeries ReferAndPosSeries(RoleTypeParam item);
        IEnumerable<sp_PriviligeList> PriviligeList(CommonParam Item);
        string GenerateLink(GenerateLinkParam Token);
        LoginResponseModel LogWithLinkiUser(Login Model);
        IEnumerable<sp_AcitveUserList> ActiveUserList(CommonParam Item);
        string UploadLogoUrl(UserModel Um, string fileName);
        string UnlockIncorrectAttempt(BaseParam Item);
        Myprofile GetUserDetilas(CommonParam Item);
        string UpdateUserinfo(UpdateUserParam Item);
        IEnumerable<dynamic> FilterUserByNameMobileEmail(FilterText item);
        string UploadProfilePic(UserModel Um, string fileName);
        string UpdateAlterNet(UpdateUserParam Item);
        string GetUserReport(int UserID, int Client,string Option,DateTime StartDate,DateTime EndDate);
        string GetUserReportWithFilter(int UserID, int Client);
        IEnumerable<GetFilterUserList> GetUserFilterByRole(FilterRoleUser item);
        string GetCertificate(CommonParam Item);
        string UserVerification(UserVerificationParam item);
        IEnumerable<vw_userregiondata> RegionNDBranch(CommonParam Item);
        UserModel UserInfoCheckWithStatus(CommonParam param);
        vw_userregiondata GetUserRegionndBranch(User param);
        IEnumerable<BindPosExam> GetPosExamSummary(CommonParam param);
        string RemovePosDoc(PosDocParam Item);
        string DocumentCheck(DocCHeckParam param);
        IEnumerable<sp_FeeddbackData> UsersFeedbackList(CommonParam Item);
        string CheckPinAvailable(CheckPinParam param);
        string UpdatePincode(UpdatePinParam Item);
        CrmCityState GetStateCity(CheckPinParam Item);
        #endregion
        #region BusinessReport Controller
        IEnumerable<ManageOnlineOfflineMotor> MotorBusinessReport(FromDateToDate param);
        IEnumerable<HealthBusiness> HealthBusinessReport(FromDateToDate param);
        string DownloadMotorBusinessReport(BusinessReportReq param);
        IEnumerable<sp_PaymentFail> PaymentFailData(CommonParam Item);
        IEnumerable<HealthHeaderReport> HealthHeaderBusiness(CommonParam Item);
        OnlineOfflineBusinessHeaderBase MotorHeaderBusiness(MotorHeader Item);
        string DownloadMotorPolicyPDF(MotorPolicyPDF param);
        IEnumerable<vw_Missingpolicyreport> MissingPolicies(CommonParam Item);
        string SaveMissingPolicies(MissingPolicyParam item);
        IEnumerable<sp_GetProcessPaymentGetway> GetProcessPaymentGetway(CommonParam Item);
        IEnumerable<sp_GetProcessHLTPaymentGetway> GetProcessHLTPaymentGetway(CommonParam Item);
        string OfflineUpdatePolicy(OfflineUpdatePolicy Item);
        string ManualUploadOflineBusiness(ManualOfflineParam Item);
        string OfflineUpdateHLTPolicy(OffLineUpdateHLTPolicyParam Item);
        string LifePolicy(LifePolicyParam Item);
        IEnumerable<Enquiry> FilterLifeEnquiryWithNumber(FilterEnquiry Item);
        IEnumerable<sp_Consolidate> GetConsolidate(ConsolidateParam Item);
        userprivilegerolemapping CheckDownloadOption(CommonParam item);
        IEnumerable<sp_CreateByListOfflineBusines> OfflineUserList(BaseParam item);
        IEnumerable<sp_ManualOfflineBusiness> ManualOfflineBusinessReport(FromDateToDate param);
        IEnumerable<sp_YearConsolidate> GetYearWiseCosolidate(YearConsolidateParam Item);
        IEnumerable<sp_ShowAboutConsolidate> ShowAboutConsolidate(ShowAboutConsolidateParam Item);
        IEnumerable<sp_ManualOfflineHLTBusiness> OfflineHealthHLTPolicy(BusinessReportReq param);
        string UploadBulkManualOfflineMotorBusiness(BulkOfflineManualMotorParam item);
        string UploadBulkManualOfflineHLTBusiness(BulkOfflineManualHLTParam item);
        IEnumerable<States> GetStateList(CommonParam Item);
        Cities GetStateThroughCityID(GetStateParam Item);
        IEnumerable<Cities> GetCityList(GetCitiesParam Item);
        IEnumerable<Rtos> GetRTOList(CommonParam Item);
        string DeleteOfflinePolicy(OfflinePolicy Item);
        dynamic GetPolicyInfo(OfflinePolicy Item);
        #endregion

        #region Products
        IList<EnquiryTypeMaster> BindEnquiryTypeMaster();
        IEnumerable<ReqAndResponseBase> GetReqResData(ReqResParam item);
        IEnumerable<sp_RenewData> RenewData(RenewDataList Item);
        IEnumerable<sp_RenewHealthData> RenewHealthData(RenewDataList Item);
        IEnumerable<sp_ReqResponse> GetAllReqstAndResponse(CallRequestResponseParam item);
        #endregion Products

        #region DomainMapping
        ClientMaster GetClientData(UrlBase Item);
        #endregion DomainMapping

        #region BookingPolicy
        ClientMaster GetCRMCurrentUrl(CommonParam item);
        #endregion BookingPolicy

        #region Account
        string ResetPass(ResetPass item);
        string LogOut(CommonParam item);
        string Renewalenquiry(FindEnquiryParam Item);
        string Authorization(UserAuth Item);
        IEnumerable<ClientMaster> GetClientData();
        string CreateNewClient(CreateCLientParam Item);
        string CreateSuperAdmin(SuperAdmin Item);
        string CheckSuperAdmin(CheckSuperAdminParam Item);
        string ResetBeforeLoginPass(ResetPassParam Item);
        #endregion Account
        #region Setup
        IEnumerable<UserCreationResponse> ImportUser(UserCreationList Item);
        dynamic Endusermapping(EnduserMapping Item);
        IEnumerable<UserDetailBase> GetEndUserDetailWithMPD(EnduserMapping Item);
        IEnumerable<UserMaster> GetUserListWithRoleid(RoleTypeParam Item);
        string SetupUserPrivilege(MergeUserPrivilege Item);
        string SetupUserRoleType(MergeUserPrivilege Item);
        IEnumerable<RegionZone> RegionlistWithUserID(RoleTypeParam Item);
        string UpdateReportingManager(ManageUserMapp Item);
        string UpdateRegionWithTeam(ManageUserMapp Item);
        IEnumerable<Manufacturer> MotorList(MotorParam item);
        IEnumerable<Vehicles> ModelList(MotorParam item);
        IEnumerable<Variants> Variants(MotorParam item);
        IEnumerable<Companies> Companies(CompaniesparamWithProduct item);
        IEnumerable<sp_MapOrUnmapList> MappOrUnmaplist(MappParam Item);
        string GetMapped(MappVariant Item);
        IEnumerable<Manufacturer> Getmanufacturer();
        IEnumerable<vw_vehicles> GetVehicles();
        string AddManufacturer(AddManufacturerParam Item);
        string AddVehicle(AddVehicleParam Item);
        IEnumerable<Fuels> GetFuels();
        string AddVariant(AddVariantParam Item);
        IEnumerable<vw_Variants> GetVariants();
        IEnumerable<RegionZone> GetAllRegionList(CommonParam Item);
        IEnumerable<sp_Regionbranchwithclient> GetAllBranchListByClient(CommonParam Item);
        string SaveRegionWithBranch(ListRegionWithBranch Item);
        string ImportRenewal(ImportRenewal item);
        string UpdateAlterNateCode(MergeUserAlterNateCode Item);
        string UpdateRMCode(MergeUserRMCode Item);
        string GetSampleBulkImportFle(CommonParam Item);
        LICBroking GetAgencyCode(CommonParam Item);
        string SaveAgencyCode(AgencyCodeParam Item);
        string SaveSalesPersonCode(SalesPersonCodeParam Item);
        IEnumerable<sp_GetSalesPersonRecord> GetSalesPersonCode(CommonParam Item);
        LicRedirectionModel ChecksumAPIForSpCode(CommonParam Item);
        #endregion setup
        #region Master Setup
        IEnumerable<sp_GetReferPosCode> GetCodePrifix(FilterPrifix Item);
        string updateOrInsertPosRefer(CodePrifixParam Item);
        string PosDuration(PosDuration Item);
        IEnumerable<sp_MapRoleList> GetRoleForMap(CommonParam Item);
        string SaveRoleList(MapRole Item);
        string IRDAparam(IRDAparam Item);
        bool CheckIRDA(CommonParam Item);
        IEnumerable<MailServerOption> GetMailServerOption();
        string SaveMailSetup(MailserverSetupParam item);
        MailServer GetSelectedMailOption(MailSelectedOption item);
        sp_MailserverData GetSelectedMailOptionWithRole(MailSelectedOptionWithRole item);
        string SaveSMSSetup(SMSServerSetupParam Item);
        SmsServer GetSelectedSmsSeverOption(SMSServerSetupParam Item);
        IEnumerable<CompanyDetails> VehicleMappingData(VehicleVariantMapping Item);
        string MapVehicle(MapUnmap Item);
        IEnumerable<Companies> GetCompanies();
        string SavePayoutData(SavePayoutDataParam Item);
        string DelPayoutData(RemovePayoutData Item);
        IEnumerable<sp_GetPayoutData> GetPayoutData(GetPayoutData Item);
        string PosExam(QuestionData Item);
        string TestMailserver(MailserverSetupParam Item);
        string SaveRegion(ManageRegion Item);
        IEnumerable<sp_BranchListByRegion> BranchList(ManageRegion Item);
        string Getfile(GetOffilePDF item);
        IEnumerable<sp_InactiveUserList> InactivePosList(CommonParam Item);
        string MakeActivePos(PosSeparater Item);
        string BasePosCertification(BasePosCertificationParam Item);
        IEnumerable<PosQuestions> SavedPosQstnList(CommonParam Item);
        string DelPosQstnList(PosQuestionID Item);
        string SMSTestIng(SmsTestParam Item);
        IEnumerable<sp_InactiveUserList> InactivePosListWithDocReq(CommonParam Item);
        DocumentWithAPI GetUploadedDoc(UserDocParam Item);
        string ShareMessage(SendMessageModel Item);
        string VerfyDocument(User Item);
        string SetupActive(RegionActiveParam Item);
        string RenewNotificationConfig(RenewNotificationConfigParam Item);
        IEnumerable<Renewnotificationduration> GetRenewNotificationConfig(CommonParam Item);
        NotificationEmail GetNotificationBody(GetNotificationBodyParam Item);
        NotificationEmail GetNotificationBodyWithRoleID(GetNotificationBodyWithRoleParam Item);
        string SaveMailNotificationBody(SaveMailNotificationBodyParam Item);
        string ReplaceUserInfo(ReplaceUserParam Item);
        string UploadPosExamData(string DocName,string Token, IFormFile file,string SummaryName);
        IEnumerable<ReturnPageUrls> GetPageURLs(CommonParam item);
        string SetPageScript(PageScriptParam item);
        string DelPageScript(PageScriptParam item);
        IEnumerable<ScriptPagePrivilege> GetPageScript(CommonParam Item);
        string ConfigSupport(CnfigSupprtParam item);
        ConfigureSupport CheckConfigSupport(CommonParam item);
        PosUserActiveDuration GetTrainingHours(CommonParam Item);
        IEnumerable<sp_UserProgresive> UserProgresive(ConsolidateParam Item);
        string OfflineHealthPolicy(OfflineHealthPolicyParam Item);
        HeaderFooter GetCertificateHeader(CommonParam Item);
        IEnumerable<FeedbackOption> GetFadBackOptions(CommonParam Item);
        string ConfigureFeedbackOption(FeedbackOptionParam Item);
        string RemoveFeedbackOption(FeedbackOptionIDParam Item);
        string SaveDigitalSign(SaveDigitalSignParam item);
        DigitalSignMaster GetSaveDigitalSign(CommonParam item);
        #endregion Master Setup

        #region PortalMasterSetup
        string SavePosMotorQuoteSetup(SaveMotoRestrictQuoteLstParam Item);
        IEnumerable<Apilistpvc> GetClientInsurer(BrokerFetchParam Item);
        IEnumerable<Apilisthlt> GetHLTClientInsurer(BrokerFetchParam Item);
        string SavePosHLTQuoteSetup(SaveHLTRestrictQuoteLstParam Item);
        IEnumerable<Saveposquotesetup> GetHLTQuoteSetup(BrokerFetchParam Item);
        #endregion PortalMasterSetup

        #region EndUser Manager
        public LoginResponseModel EndLoginUser(Login Model);
        string MatchEnduserOTP(Login Model);
        string CheckEndUserToken(string Token);
        #endregion  EndUser Manager

        #region AutoService
        string SendNotificationForRenew();
        #endregion AutoService

        #region EndUserData
        IEnumerable<sp_EndUserData> GetMotordata(EndUserProductDetailsParam Item);
        IEnumerable<sp_EndUserhealthData> GetHealthdata(EndUserProductDetailsParam Item);
        #endregion EndUserDataEnd

        #region SFTP
        string GetMotorBusinessReport(FromDateToDate param);
        string GetUserDataReport(int UserID, int Client, string Option, DateTime StartDate, DateTime EndDate);
        string GetOfflineBusinessReport(FromDateToDate param);
        string SendSFTPMail(SFTPMail param);
        #endregion SFTP

        #region Offline Life Policy
        string OfflineLifePolicy(OfflineLifePolicyParam Item);
        string UploadBulkOfflineLifeBusiness(BulkOfflineLifeParam param);
        IEnumerable<sp_ManualOfflieTermLifeReport> ManualOfflieTermLifeReport(BusinessReportReq Item);
        string DownloadOfflineLifeReport(BusinessReportReq param);
        object OfflineLifeHeader(CommonParam token);
        ManualOfflineLifePolicy GetOfflineLifePolicyInfo(OfflinePolicy Item);
        #endregion

        #region Zoho Signup
        List<sp_POSUserListForZoho> GetUsersforDocSign(FromDateToDate param);
        string POSMailingForStamp(POSMailingIn param);
        string POSStampUpdate(POSUpload param);
        string POSZohoStatusUpdate(int UserId);
        #endregion
    }
}
