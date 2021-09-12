using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
using API.CommonMethods;
using API.Model;
using API.SecurityAccesControl;
using Microsoft.AspNetCore.Authorization;
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
    public class MasterController : ControllerBase
    {
        IBusinessLayer businessLayer;
        UserModel model;
        public MasterController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        [HttpPost]
        public IActionResult GetCodePrifix([FromBody] FilterPrifix Item)
        {
            var Response = businessLayer.GetCodePrifix(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveCodePrifix([FromBody] CodePrifixParam Item)
        {
            string Response = "";
            Response = businessLayer.updateOrInsertPosRefer(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SetPosAcitveDuration([FromBody] PosDuration Item)
        {
            string Response = "";
            Response = businessLayer.PosDuration(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetRoleForMap([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetRoleForMap(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveRoleList([FromBody] MapRole Item)
        {
            var Response = businessLayer.SaveRoleList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult IRDAparam([FromBody] IRDAparam Item)
        {
            var Response = businessLayer.IRDAparam(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CheckIRDA([FromBody] CommonParam Item)
        {
            var response = businessLayer.CheckIRDA(Item);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult MailServerOption([FromBody] CommonParam item)
        {
            var Response = businessLayer.GetMailServerOption();
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveMailSetup([FromBody] MailserverSetupParam item)
        {
            var Response = businessLayer.SaveMailSetup(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetSelectedMailOption([FromBody] MailSelectedOption Item)
        {
            var Response = businessLayer.GetSelectedMailOption(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetSelectedMailOptionWithRole([FromBody] MailSelectedOptionWithRole Item)
        {
            var Response = businessLayer.GetSelectedMailOptionWithRole(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveSMSSetup([FromBody] SMSServerSetupParam Item)
        {
            var Response = businessLayer.SaveSMSSetup(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetSelectedSmsSeverOption([FromBody]SMSServerSetupParam Item)
        {
            var Response = businessLayer.GetSelectedSmsSeverOption(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetCompanyVehicleDetails([FromBody] VehicleVariantMapping Item)
        {
            var Response = businessLayer.VehicleMappingData(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult MapVehicle([FromBody] MapUnmap Item)
        {
            var Response = businessLayer.MapVehicle(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetInsurerCompanies([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetCompanies();
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SavePayoutData([FromBody] SavePayoutDataParam Item)
        {
            var Response = businessLayer.SavePayoutData(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DelPayoutData([FromBody] RemovePayoutData Item)
        {
            var Response = businessLayer.DelPayoutData(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetPayoutData([FromBody] GetPayoutData Item)
        {
            var Response = businessLayer.GetPayoutData(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult PosExam([FromBody] QuestionData Item)
        {
            var Response = businessLayer.PosExam(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult TestMailserver([FromBody] MailserverSetupParam Item)
        {
            var Response = businessLayer.TestMailserver(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveRegion([FromBody] ManageRegion Item)
        {
            var Response = businessLayer.SaveRegion(Item);
            return Ok(Response);                
        }
        [HttpPost]
        public IActionResult BranchList([FromBody] ManageRegion Item)
        {
            var BranchList = businessLayer.BranchList(Item);
            return Ok(BranchList);
        }
        [HttpPost]
        public IActionResult InactivePosList([FromBody] CommonParam Item)
        {
            var BranchList = businessLayer.InactivePosList(Item);
            return Ok(BranchList);
        }
        [HttpPost]
        public IActionResult InactiveToActivePos ([FromBody] PosSeparater Item)
        {
            var BranchList = businessLayer.MakeActivePos(Item);
            return Ok(BranchList);
        }
        [HttpPost]
        public IActionResult BasePosCertification([FromBody] BasePosCertificationParam Item)
        {
            var Response = businessLayer.BasePosCertification(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Certificate()
        {
            string Response = "";
            try
            {
                UserModel Um = API.CommonMethods.Common.DecodeToken(Request.Form["Token"].ToString());
                var GstFiles = Request.Form.Files;
                IFormFile file = GstFiles[0];
                var Folder = Request.Form["DocName"].ToString();
                var GetExtention = System.IO.Path.GetExtension(file.FileName);
                var fileName = Um.ClientID.ToString() + GetExtention;
                try
                {
                    string Url = GetCurrentPath() + "/Downloads/"+ Folder + "/" + fileName;
                    using (var stream = new FileStream(Url, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    Response = ex.Message;
                }
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Ok(Response);
        }
        private string GetCurrentPath()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

        [HttpPost]
        public IActionResult SavedPosQstnList([FromBody]CommonParam Item)
        {
            var Response = businessLayer.SavedPosQstnList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DelPosQstnList([FromBody] PosQuestionID Item)
        {
            var Response = businessLayer.DelPosQstnList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SMSTestIng([FromBody] SmsTestParam Item)
        {
            var Response = businessLayer.SMSTestIng(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult InactivePosListWithDocReq([FromBody] CommonParam Item)
        {
            var BranchList = businessLayer.InactivePosListWithDocReq(Item);
            return Ok(BranchList);
        }
        [HttpPost]
        public IActionResult GetUploadedDoc([FromBody] UserDocParam Item)
        {
            var Response = businessLayer.GetUploadedDoc(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ShareMessage([FromBody] SendMessageModel Item)
        {
            var Response = businessLayer.ShareMessage(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult VerfyDocument([FromBody] User Item)
        {
            var Response = businessLayer.VerfyDocument(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SetupActive([FromBody] RegionActiveParam Item)
        {
            var Response = businessLayer.SetupActive(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult RenewNotificationConfig([FromBody] RenewNotificationConfigParam Item)
        {
            var Response = businessLayer.RenewNotificationConfig(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetRenewNotificationConfig([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetRenewNotificationConfig(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetNotificationBody([FromBody] GetNotificationBodyParam Item)
        {
            var Response = businessLayer.GetNotificationBody(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetNotificationBodyWithRoleID([FromBody] GetNotificationBodyWithRoleParam Item)
        {
            var Response = businessLayer.GetNotificationBodyWithRoleID(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveMailNotificationBody([FromBody] SaveMailNotificationBodyParam Item)
        {
            var Response = businessLayer.SaveMailNotificationBody(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ReplaceUserInfo([FromBody] ReplaceUserParam Item)
        {
            var Response = businessLayer.ReplaceUserInfo(Item);
            return Ok(Response);
        }
        [AllowAnonymous]
        public IActionResult UploadFiles()
        {
            string Response = "";
            var GstFiles = Request.Form.Files;
            string DocName = Request.Form["DocName"];
            string Token = Request.Form["Token"];
            string Name = Request.Form["UserID"];
            Response = businessLayer.UploadPosExamData(DocName, Token,GstFiles[0],Name);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetPageURLs([FromBody] CommonParam item)
        {
            var Response = businessLayer.GetPageURLs(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SetPageScript([FromBody] PageScriptParam item)
        {
            var Response = businessLayer.SetPageScript(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetPageScript([FromBody]CommonParam Item)
        {
            var Response = businessLayer.GetPageScript(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DelPageScript([FromBody] PageScriptParam item)
        {
            var Response = businessLayer.DelPageScript(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ConfigSupport([FromBody] CnfigSupprtParam item)
        {
            var Response = businessLayer.ConfigSupport(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CheckConfigSupport([FromBody] CnfigSupprtParam item)
        {
            var Response = businessLayer.CheckConfigSupport(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetTrainingHours([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetTrainingHours(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetCertificateHeader([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetCertificateHeader(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetFadBackOptions([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetFadBackOptions(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ConfigureFeedbackOption([FromBody] FeedbackOptionParam Item)
        {
            var Response = businessLayer.ConfigureFeedbackOption(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult RemoveFeedbackOption([FromBody] FeedbackOptionIDParam Item)
        {
            var Response = businessLayer.RemoveFeedbackOption(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveDigitalSign([FromBody] SaveDigitalSignParam item)
        {
            var Response = businessLayer.SaveDigitalSign(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetSaveDigitalSign([FromBody] CommonParam item)
        {
            var Response = businessLayer.GetSaveDigitalSign(item);
            return Ok(Response);
        }
    }
}
