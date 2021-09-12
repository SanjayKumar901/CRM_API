using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
using API.CommonMethods;
using API.Model;
using API.SecurityAccesControl;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("cors")]
    [ApiController]
    //[Authorize]
    [StillLogin]
    public class HomeController : ControllerBase
    {
        IBusinessLayer businessLayer;
        UserModel model;
        public HomeController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        public IActionResult DashBoardPrivilages([FromBody] Privilege param)
        {
            model = CommonMethods.Common.DecodeToken(param.Token);
            param.Userid = model.UserID;
            var data = businessLayer.UserPrevileges(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult ClientLogo([FromBody] CommonParam item)
        {
            var Logo = businessLayer.GetClientLogo(item);
            return Ok(Logo);
        }
        [HttpPost]
        public async Task<IActionResult> DashbordHeaders([FromBody] CommonParam item)
        {
            model = CommonMethods.Common.DecodeToken(item.Token);
            ClientUser user = new ClientUser()
            {
                Userid = model.UserID,
                ClientID = model.ClientID.Value
            };
            var data = await Task.Run(()=> businessLayer.DahsboardHeader(user));
            if (data == null)
                return NotFound("Not Found");
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> DashbordOfflineleadGrid([FromBody] CommonParam item)
        {
            var data = await Task.Run(()=>businessLayer.DashbordOfflineleadGrid(item));
            if (data == null)
                return NotFound("Not Found");
            return Ok(data);
        }
        [HttpPost]
        public IActionResult OfflineGotoPayment([FromBody] OfflineGotoPayment Item)
        {
            var data = businessLayer.OfflineGotoPayment(Item);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> DashbordleadGrid([FromBody] BusinessReportReq item)
        {
            model = CommonMethods.Common.DecodeToken(item.Token);
            FromDateToDate user = new FromDateToDate()
            {
                Userid = model.UserID,
                ClientID = model.ClientID.Value,
                FromDate = item.FromDate,
                ToDate = item.ToDate
            };
            var data = await Task.Run(()=> businessLayer.DashboardLeadGrid(user));
            if (data == null)
                return NotFound("Not Found");
            return Ok(data);
        }
        [HttpPost]
        public IActionResult RegionZones(CommonParam param)
        {
            try
            {
                var zones = businessLayer.RegionZones();
                return Ok(zones);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult BusinessByCity(BusinessbyCityParam item)
        {
            var Response = businessLayer.BUSINESSBYCITY(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult QuetationInfo([FromBody] EnquiryInfo item)
        {
            dynamic Response = null;
            string[] Product = { "car", "two" , "pcv","gcv" };
            if (Product.Where(row=>row==item.Product.ToLower()).Count()>0)
            {
                if (item.Action.ToLower() == "quote")
                    Response = businessLayer.EnquiryInfo(item);
                else if (item.Action.ToLower() == "afterquote")
                    Response = businessLayer.EnquiryInfoAfterQuote(item);
            }
            else if (item.Product.ToLower() == "hlt")
            {
                if (item.Action.ToLower() == "quote")
                    Response = businessLayer.HltEnquiryInfo(item);
                else if (item.Action.ToLower() == "afterquote")
                    Response = businessLayer.HltEnquiryInfoAfterQuote(item);
            }
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult PosRegisterData([FromBody] RegisterPosParam Item)
        {
            var Response = businessLayer.GetPosRegistrationData(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult AcitveOrNot([FromBody] CommonParam Item)
        {
            var Response = businessLayer.AcitveOrNot(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CheckHourTestProcess([FromBody] CommonParam Item)
        {
            var Response = businessLayer.CheckHourTestProcess(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult StartExamForFurtherProcess([FromBody] ExamProcessParam Item)
        {
            var Response = businessLayer.ForFurtherProcess(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult TrainingDone([FromBody] CompleteTraining Item)
        {
            var Response = businessLayer.PosTrainingDone(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ExamOver([FromBody] CompleteExam Item)
        {
            var Response = businessLayer.PosExamDone(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Claim([FromBody] RegisterPosParam Item)
        {
            var Response = businessLayer.Claim(Item);
            if (Response == null)
                return NotFound("Not Found");
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Sharedatadetail([FromBody] RegisterPosParam Item)
        {
            var Response = businessLayer.Sharedatadetail(Item);
            if (Response == null)
                return NotFound("Not Found");
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetLoginHistory([FromBody] LoginTimeHis Item)
        {
            var Response = businessLayer.GetLoginHistory(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DownloadLoginHistory([FromBody] LoginTimeHis Item)
        {
            var Response = businessLayer.DownloadLoginHistory(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetLeadDownload([FromBody] BusinessReportReq Item)
        {
            var Response = businessLayer.LeadDownload(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetPosRqstDownload([FromBody] BusinessReportReq Item)
        {
            var Response = businessLayer.GetPosRqstDownload(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CheckForGotoProposal([FromBody] CheckForGotoProposalParam Item)
        {
            var Response = businessLayer.CheckForGotoProposal(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetOfflineFeatureLead([FromBody] GetOfflineFeatureParam Item)
        {
            var Response = businessLayer.GetOfflineFeatureLead(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveOfflineMotorQuote([FromBody] SaveOfflineMotorQuoteParam Item)
        {
            var Response = businessLayer.SaveOfflineMotorQuote(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveOfflineHealthQuote([FromBody] SaveOfflineHealthQuoteParam Item)
        {
            var Response = businessLayer.SaveOfflineHealthQuote(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Encode([FromBody] CheckForGotoProposalParam Item)
        {
            string ss = Common.Encrypt(Item.EnquiryNo);
            return Ok(ss);
        }
        [HttpPost]
        public IActionResult PosExamQuestions([FromBody] CommonParam Item)
        {
            var Response = businessLayer.PosExamQuestions(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult QuoteRelatedMessage([FromBody] OfflineQueryMessage Item)
        {
            var Response = businessLayer.QuoteRelatedMessage(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult QuoteRelatedMessageCounter([FromBody] CommonParam Item)
        {
            var Response = businessLayer.QuoteRelatedMessageCounter(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult QuoteRelatedMessageList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.QuoteRelatedMessageList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ReadedMessage([FromBody] OfflineQueryUpdateMessage Item)
        {
            var Response = businessLayer.ReadedMessage(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DashoardModulePrivilege([FromBody] CommonParam Item)
        {
            var Response = businessLayer.DashoardModulePrivilege(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult AddFadBack([FromBody] AddFadBackParam Item)
        {
            var Response = businessLayer.AddFadBack(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult BirthdayUserList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.BirthdayUserList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SendBirthdayMessages([FromBody] BithdayWisheshParam item)
        {
            var Response = businessLayer.SendBirthdayMessages(item);
            return Ok(Response);
        }
    }
}