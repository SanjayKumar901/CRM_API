using API.PortalAPI.MotorAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.TermLifeBal
{
    public interface ITermLifeBusinessLayer
    {
        string SaveEnquiry(LifeEnquiry Item);
        /*string SaveGotoProposal(GoToproposal Item);
        string Policydetails(PolicydetailsParam Item);
        dynamic DynamicGet(updateTable Param);
        string DynamicSet(updateTable Param);
        string UpdateQuery(string query);
        string RegisterPos(RegisterPos Item);
        */
    }
}
