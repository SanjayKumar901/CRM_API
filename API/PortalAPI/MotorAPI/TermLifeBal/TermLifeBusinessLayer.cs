using API.DAL;
using API.PortalAPI.MotorAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.TermLifeBal
{
    public class TermLifeBusinessLayer : ITermLifeBusinessLayer
    {
        public string SaveEnquiry(LifeEnquiry Item)
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
            param.Add("@p_YourAge", Item.YourAge.ToString());
            param.Add("@p_Gender", Item.Gender);
            param.Add("@p_SmokeStaus", Item.SmokeStaus);
            param.Add("@p_AnnualInCome", Item.AnnualInCome.ToString());
            param.Add("@p_PreferredCover", Item.PreferredCover.ToString());
            param.Add("@p_CoverageAge", Item.CoverageAge.ToString());
            param.Add("@p_PolicyType", Item.PolicyType);
            var dataList = Data.ProcedureOutput("sp_TermLifeSaveEnquiry", param);
            return dataList;
        }
    }
}
