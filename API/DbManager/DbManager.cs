using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.EntityFrameworkCore;
using API.DbManager.DbModels;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace API.DbManager
{
    public class DbManager : DbContext
    {
        private DbManager _dbManager;
        #region Models
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<sp_Rolelist> sp_Rolelists { get; set; }
        public DbSet<sp_RoleMastWithClientID> sp_RoleMastWithClientIDs { get; set; }
        public DbSet<sp_TeamUserList> sp_TeamUserLists { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<RegionZone> RegionZones { get; set; }
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<UserRegionBranch> UserRegionBranchs { get; set; }
        public DbSet<sp_CreateByList> sp_CreateByLists { get; set; }
        public DbSet<sp_UserInfo> sp_UserInfos { get; set; }
        public DbSet<sp_PayoutData> sp_PayoutDatas { get; set; }
        public DbSet<Usertoken> Usertokens { get; set; }
        public DbSet<SP_BusinessReportViewDetails> SP_BusinessReportViewDetailss { get; set; }
        public DbSet<sp_HealthBusinessReport> sp_HealthBusinessReports { get; set; }
        public DbSet<SP_PRIVILAGES> SP_PRIVILAGESs { get; set; }
        public DbSet<sp_DashboardHeader> sp_DashboardHeaders { get; set; }
        public DbSet<sp_LeadDetailGrid> LeadDetailGrids { get; set; }
        public DbSet<vw_RegionZone> vw_RegionZones { get; set; }
        public DbSet<ClientMaster> ClientMasters { get; set; }
        public DbSet<LoginMobileOTP> LoginMobileOTPs { get; set; }
        public DbSet<sp_MobileLoginData> sp_MobileLoginDataS { get; set; }
        public DbSet<sp_privilegelist> sp_privilegelists { get; set; }
        public DbSet<userprivilegerolemapping> userprivilegerolemappings { get; set; }
        public DbSet<Users> Userss { get; set; }
        public DbSet<Motorenquiry> Motorenquirys { get; set; }
        public DbSet<Motorpolicydetails> Motorpolicydetailss { get; set; }
        public DbSet<sp_ReferAndPosSeries> sp_ReferAndPosSerieses { get; set; }
        public DbSet<SP_BUSINESSBYCITY> SP_BUSINESSBYCITYs { get; set; }
        public DbSet<sp_ReqAndResponse> sp_ReqAndResponses { get; set; }
        public DbSet<EnquiryTypeMaster> EnquiryTypeMasters { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<LoginWithIncorrectAttempt> LoginWithIncorrectAttempts { get; set; }
        public DbSet<sp_QuoteInfo> sp_QuoteInfos { get; set; }
        public DbSet<sp_AfterQuoteInfo> sp_AfterQuoteInfos { get; set; }
        public DbSet<sp_PriviligeList> sp_PriviligeLists { get; set; }
        public DbSet<GeneratedUniquePOSID> GeneratedUniquePOSIDs { get; set; }
        public DbSet<testData> testDatas { get; set; }
        public DbSet<sp_PaymentFail> sp_PaymentFails { get; set; }
        public DbSet<sp_UserDetailViaMPD> sp_UserDetailViaMPDs { get; set; }
        public DbSet<sp_HealthbusinessReportHeader> sp_HealthbusinessReportHeaders { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Vehicles> Vehicless { get; set; }
        public DbSet<Variants> Variantss { get; set; }
        public DbSet<Companies> Companiess { get; set; }
        public DbSet<sp_MapOrUnmapList> sp_MapOrUnmapLists { get; set; }
        public DbSet<sp_AcitveUserList> sp_AcitveUserLists { get; set; }
        public DbSet<sp_RenewData> sp_RenewDatas { get; set; }
        public DbSet<sp_RenewHealthData> sp_RenewHealthDatas { get; set; }
        public DbSet<sp_RegionZoneWithClientid> sp_RegionZoneWithClientids { get; set; }
        public DbSet<sp_hltQouteInfo> sp_hltQouteInfos { get; set; }
        public DbSet<sp_HltAfterQuoteInfo> sp_HltAfterQuoteInfos { get; set; }
        public DbSet<ReferPosCodeMaster> ReferPosCodeMasters { get; set; }
        public DbSet<Register_as_pos> Register_as_poss { get; set; }
        public DbSet<PosUserActiveDuration> PosUserActiveDurations { get; set; }
        public DbSet<PosExamStart> posExamStarts { get; set; }
        public DbSet<ClaimDetails> ClaimDetailss { get; set; }
        public DbSet<Sharedatadetails> Sharedatadetailss { get; set; }
        public DbSet<LoginTimeHistory> LoginTimeHistorys { get; set; }
        public DbSet<sp_Logintimehistory> sp_Logintimehistorys { get; set; }
        public DbSet<MissingPolicyReport> MissingPolicyReports { get; set; }
        public DbSet<vw_Missingpolicyreport> vw_Missingpolicyreports { get; set; }
        public DbSet<sp_MapRoleList> sp_MapRoleLists { get; set; }
        public DbSet<RoletypeClientActive> RoletypeClientActives { get; set; }
        public DbSet<IRDAHiddenProcess> IRDAHiddenProcesss { get; set; }
        public DbSet<sp_QuoteReqAndResponse> sp_QuoteReqAndResponses { get; set; }
        public DbSet<vw_vehicles> vw_vehicless { get; set; }
        public DbSet<Fuels> Fuelss { get; set; }
        public DbSet<vw_Variants> vw_Variantss { get; set; }
        public DbSet<sp_Regionbranchwithclient> sp_Regionbranchwithclients { get; set; }
        public DbSet<BranchForClient> BranchForClients { get; set; }
        public DbSet<sp_GetCitiesWithRegionid> sp_GetCitiesWithRegionids { get; set; }
        public DbSet<sp_MotorBusinessHeader> sp_MotorBusinessHeaders { get; set; }
        public DbSet<WhoDeregOrReg> WhoDeregOrRegs { get; set; }
        public DbSet<MailServerOption> MailServerOptions { get; set; }
        public DbSet<MailServer> MailServer { get; set; }
        public DbSet<Motorpolicydetailsothersdata> Motorpolicydetailsothersdatas { get; set; }
        public DbSet<sp_UserDetailViaHPD> sp_UserDetailViaHPDs { get; set; }
        public DbSet<HealthGotoPaymentData> HealthGotoPaymentDatas { get; set; }
        public DbSet<UploadPolicy> UploadPolicys { get; set; }
        public DbSet<vw_Uploadpolicy> vw_Uploadpolicys { get; set; }
        public DbSet<Offlinerequestmotor> Offlinerequestmotors { get; set; }
        public DbSet<sp_EndUserData> sp_EndUserDatas { get; set; }
        public DbSet<sp_EndUserhealthData> sp_EndUserhealthDatas { get; set; }
        public DbSet<sp_OfflineLead> sp_OfflineLeads { get; set; }
        public DbSet<sp_GetProcessPaymentGetway> sp_GetProcessPaymentGetways { get; set; }
        public DbSet<sp_GetProcessHLTPaymentGetway> sp_GetProcessHLTPaymentGetways { get; set; }
        public DbSet<Lifepolicy> Lifepolicys { get; set; }
        public DbSet<SmsServer> SmsServers { get; set; }
        public DbSet<ClientDBConnection> ClientDBConnections { get; set; }
        public DbSet<Enquiry> Enquirys { get; set; }
        public DbSet<Useraddresses> Useraddressess { get; set; }
        public DbSet<Healthenquiry> Healthenquirys { get; set; }
        public DbSet<Exceptions> Exceptionss { get; set; }
        public DbSet<sp_Consolidate> sp_Consolidates { get; set; }
        public DbSet<Insurerpayout> Insurerpayouts { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<SaveRequestResponseData> SaveRequestResponseDatas { get; set; }
        public DbSet<Rtos> Rtoss { get; set; }
        public DbSet<Cities> Citiess { get; set; }
        public DbSet<States> Statess { get; set; }
        public DbSet<PosQuestions> PosQuestionss { get; set; }
        public DbSet<Responses> Responsess { get; set; }
        public DbSet<Logtable> Logtables { get; set; }
        public DbSet<sp_GetReferPosCode> sp_GetReferPosCodes { get; set; }
        public DbSet<OfflineQueryRelatedMessage> OfflineQueryRelatedMessages { get; set; }
        public DbSet<Manualofflinepolicy> Manualofflinepolicys { get; set; }
        public DbSet<sp_ManualOfflineBusiness> sp_ManualOfflineBusinesss { get; set; }
        public DbSet<sp_CreateByListOfflineBusines> sp_CreateByListOfflineBusiness { get; set; }
        public DbSet<NotificationEmail> NotificationEmails { get; set; }
        public DbSet<NotificationSMS> NotificationSMSs { get; set; }
        public DbSet<Branchcity> Branchcitys { get; set; }
        public DbSet<sp_BranchListByRegion> sp_BranchListByRegions { get; set; }
        public DbSet<sp_InactiveUserList> sp_InactiveUserLists { get; set; }
        public DbSet<sp_GetEnquiryNo> sp_GetEnquiryNos { get; set; }
        public DbSet<CertificateBack> CertificateBacks { get; set; }
        public DbSet<ClientPosCertificateBase> ClientPosCertificateBases { get; set; }
        public DbSet<sp_SharedDetails> sp_SharedDetailss { get; set; }
        public DbSet<sp_ClaimDetails> sp_ClaimDetailss { get; set; }
        public DbSet<sp_CheckDashBoardPrivilege> sp_CheckDashBoardPrivileges { get; set; }
        public DbSet<sp_PosRequestData> sp_PosRequestDatas { get; set; }
        public DbSet<sp_RenewUserData> sp_RenewUserDatas { get; set; }
        public DbSet<Renewnotificationduration> Renewnotificationdurations { get; set; }
        public DbSet<vw_userregiondata> vw_userregiondatas { get; set; }
        public DbSet<UsermasterHistory> UsermasterHistorys { get; set; }
        public DbSet<Posexamsummary> Posexamsummarys { get; set; }
        public DbSet<vw_pageurl> vw_pageurls { get; set; }
        public DbSet<ScriptPagePrivilege> ScriptPagePrivileges { get; set; }
        public DbSet<ConfigureSupport> ConfigureSupports { get; set; }
        public DbSet<sp_YearConsolidate> sp_YearConsolidates { get; set; }
        public DbSet<sp_ShowAboutConsolidate> sp_ShowAboutConsolidates { get; set; }
        public DbSet<sp_UserProgresive> sp_UserProgresives { get; set; }
        public DbSet<UserCreationMailRoleOption> UserCreationMailRoleOptions { get; set; }
        public DbSet<sp_MailserverData> sp_MailserverDatas { get; set; }
        public DbSet<sp_UserCreationMailConfig> sp_UserCreationMailConfigs { get; set; }
        public DbSet<sp_MailServerActionTime> sp_MailServerActionTimes { get; set; }
        public DbSet<ManualOfflineHealthPolicy> ManualOfflineHealthPolicys { get; set; }
        public DbSet<sp_ManualOfflineHLTBusiness> sp_ManualOfflineHLTBusinesss { get; set; }
        public DbSet<FeedbackOption> FeedbackOptions { get; set; }
        public DbSet<FeedbackData> FeedbackDatas { get; set; }
        public DbSet<sp_FeeddbackData> sp_FeeddbackDatas { get; set; }
        public DbSet<sp_EndUserLogin> sp_EndUserLogins { get; set; }
        public DbSet<EnduserToken> EnduserTokens { get; set; }
        public DbSet<Saveposquotesetup> Saveposquotesetups { get; set; }
        public DbSet<sp_SavePosMotorQuoteSetup> sp_SavePosMotorQuoteSetups { get; set; }
        public DbSet<Apilistpvc> Apilistpvcs { get; set; }
        public DbSet<Apilisthlt> Apilisthlts { get; set; }
        public DbSet<sp_SavePosHLTQuoteSetup> sp_SavePosHLTQuoteSetups { get; set; }
        public DbSet<sp_GetUserBirthdayList> sp_GetUserBirthdayLists { get; set; }
        public DbSet<CrmCityState> CrmCityStates { get; set; }
        public DbSet<sp_ReqResponse> sp_ReqResponse { get; set; }
        public DbSet<sp_GetPayoutData> sp_GetPayoutDatas { get; set; }
        public DbSet<DigitalSignMaster> DigitalSignMasters { get; set; }
        public DbSet<LicSalesPersonMaster> LicSalesPersonMasters { get; set; }
        public DbSet<LICBroking> LICBrokings { get; set; }
        public DbSet<sp_GetSalesPersonRecord> sp_GetSalesPersonRecords { get; set; }
        public DbSet<sp_MotorManualBusinessHeader> sp_MotorManualBusinessHeaders { get; set; }
        public DbSet<sp_HealthManualbusinessReportHeader> sp_HealthManualbusinessReportHeaders { get; set; }
        public DbSet<PosStampMaster> PosStampMaster { get; set; }
        public DbSet<sp_POSUserList> sp_POSUserList { get; set; }
        public DbSet<sp_POSUserListForZoho> sp_POSUserListForZoho { get; set; }
        #region Offline Life Policy
        public DbSet<ManualOfflineLifePolicy> ManualOfflineLifePolicy { get; set; }
        public DbSet<sp_ManualOfflieTermLifeReport> sp_ManualOfflieTermLifeReport { get; set; }
        public DbSet<sp_LifeManualbusinessReportHeader> sp_LifeManualbusinessReportHeader { get; set; }
        #endregion

        #endregion
        #region Stored Procedures
        public IEnumerable<T> GetProcedureData<T>(string ProcedureWithParam, List<MySqlParameter> param) where T : class
        {
            using (_dbManager = new DbManager())
            {
                try
                {
                    var data = _dbManager.Set<T>().FromSqlRaw(ProcedureWithParam, param.ToArray()).ToList();
                    return data;
                }
                catch
                {
                    try
                    {
                        var data = _dbManager.Set<T>().FromSqlRaw(ProcedureWithParam, param.ToArray()).ToList();
                        return data;
                    }
                    catch (Exception ex)
                    {
                        System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\ErrorText.txt", ex.Message);
                        return null;
                    }
                }
            }
        }

        [Obsolete]
        public string OutputTypeProc(string ProcedureWithParam, List<MySqlParameter> param)
        {
            string Response = "";
            using (_dbManager = new DbManager())
            {
                try
                {
                    Response = _dbManager.Database.ExecuteSqlCommand(ProcedureWithParam, param.ToArray()).ToString();
                }
                catch
                {
                    try
                    {
                        Response = _dbManager.Database.ExecuteSqlCommand(ProcedureWithParam, param.ToArray()).ToString();
                    }
                    catch (Exception ex) { Response = "Exception Procedure : " + ex.Message; }
                }
            };
            return Response;
        }
        #endregion

        #region Connection Configure
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string jsonfile = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\appsettings.json");
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonfile);
            string connection = jsonData.Logging.ConnectionString;
            optionsBuilder.UseMySQL(connection);
        }
        #endregion
    }
}
