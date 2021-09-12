using API.DbManager.DbModels;
using API.PortalAPI.MotorAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.PortalAPI.MotorAPI.HealthBal
{
    public interface IHealthBusinessLayer
    {
        string SaveEnquiry(HealthEnquiry Item);
        string GotoProposal(HealthGoToProposalPram Item);
        string UpdateProposalDetails(UpdateProposal item);
        HealthGotoPaymentData GetHealthGotoPaymentData(HealthGotoPaymentDataParam Item);
    }
}
