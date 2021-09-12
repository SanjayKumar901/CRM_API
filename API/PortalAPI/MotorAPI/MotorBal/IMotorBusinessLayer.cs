using API.DbManager.DbModels;
using API.PortalAPI.MotorAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.MotorBal
{
    public interface IMotorBusinessLayer
    {
        string SaveEnquiry(MotorEnquiry Item);
        string SaveGotoProposal(GoToproposal Item);
        string Policydetails(PolicydetailsParam Item);
        dynamic DynamicGet(updateTable Param);
        string DynamicSet(updateTable Param);
        string UpdateQuery(string query);
        string RegisterPos(RegisterPos Item);
        dynamic SelectQuery(string query);
        string SendEmailInfoToEndUser(SendmailToUser Item);
        sp_GetEnquiryNo GenEnquiryNo(GenEnquiryNoParam Item);
    }
}
