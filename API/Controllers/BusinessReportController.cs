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
    public class BusinessReportController : ControllerBase
    {
        IBusinessLayer businessLayer;
        UserModel model;
        public BusinessReportController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        #region Motor Business
        [HttpPost]
        public IActionResult MotorBusinessReports([FromBody] BusinessReportReq param)
        {
            model = CommonMethods.Common.DecodeToken(param.Token);
            FromDateToDate paramModal = new FromDateToDate()
            {
                ClientID = model.ClientID.Value,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                Userid = model.UserID,
                ProductType = param.Product
            };
            var data = businessLayer.MotorBusinessReport(paramModal);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult DownloadMotorBusinessReport([FromBody] BusinessReportReq param)
        {
            var data = businessLayer.DownloadMotorBusinessReport(param);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult DownloadMotorPolicyPDF([FromBody] MotorPolicyPDF param)
        {
            var data = businessLayer.DownloadMotorPolicyPDF(param);
            return Ok(data);
        }

        #endregion Motor Business
        [HttpPost]
        public IActionResult HealthBusinessReports([FromBody] BusinessReportReq param)
        {
            model = CommonMethods.Common.DecodeToken(param.Token);
            FromDateToDate paramModal = new FromDateToDate()
            {
                ClientID = model.ClientID.Value,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                Userid = model.UserID
            };
            var data = businessLayer.HealthBusinessReport(paramModal);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Paymentfailstatus([FromBody] CommonParam Item)
        {
            var Response = businessLayer.PaymentFailData(Item);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult HealthHeaderBusiness([FromBody] CommonParam Item)
        {
            var response = businessLayer.HealthHeaderBusiness(Item);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult MotorHeaderBusiness([FromBody] MotorHeader Item)
        {
            var response = businessLayer.MotorHeaderBusiness(Item);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult MissingPolicies([FromBody] CommonParam item)
        {
            var response = businessLayer.MissingPolicies(item);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult SaveMissingPolicies([FromBody] MissingPolicyParam item)
        {
            var response = businessLayer.SaveMissingPolicies(item);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetProcessPaymentGetway([FromBody] CommonParam Item)
        {
            var data = businessLayer.GetProcessPaymentGetway(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult GetProcessHLTPaymentGetway([FromBody] CommonParam Item)
        {
            var data = businessLayer.GetProcessHLTPaymentGetway(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult OfflineUpdatePolicy([FromBody] OfflineUpdatePolicy Item)
        {
            var data = businessLayer.OfflineUpdatePolicy(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult OfflineUpdateHLTPolicy([FromBody] OffLineUpdateHLTPolicyParam Item)
        {
            var data = businessLayer.OfflineUpdateHLTPolicy(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult LifePolicy([FromBody] LifePolicyParam Item)
        {
            var data = businessLayer.LifePolicy(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult FilterEnquiryWithNumber([FromBody] FilterEnquiry Item)
        {
            var Data = businessLayer.FilterLifeEnquiryWithNumber(Item);
            return Ok(Data);
        }
        [HttpPost]
        public IActionResult GetConsolidate([FromBody] ConsolidateParam Item)
        {
            var Response = businessLayer.GetConsolidate(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CheckDownloadOption([FromBody] CommonParam item)
        {
            var Response = businessLayer.CheckDownloadOption(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ManualUploadOflineBusiness([FromBody] ManualOfflineParam item)
        {
            var Response = businessLayer.ManualUploadOflineBusiness(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult OfflineUserListData([FromBody] CommonParam item)
        {
            model = Common.DecodeToken(item.Token);
            BaseParam param = new BaseParam()
            {
                ClientID = model.ClientID.Value,
                UserID = model.UserID.ToString()
            };
            var data = businessLayer.OfflineUserList(param);
            return Ok(data);
            //var data = businessLayer.FilterUserByNameMobileEmail(item);
            //return Ok(data);
        }
        [HttpPost]
        public IActionResult ManualOfflineBusinessReport([FromBody] BusinessReportReq param)
        {
            model = CommonMethods.Common.DecodeToken(param.Token);
            FromDateToDate paramModal = new FromDateToDate()
            {
                ClientID = model.ClientID.Value,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                Userid = model.UserID
            };
            var data = businessLayer.ManualOfflineBusinessReport(paramModal);
            return Ok(data);
        }

        [AllowAnonymous]
        public IActionResult ManualOffline()
        {
            string Response = "";
            var GstFiles = Request.Form.Files;
            string Userid = Request.Form["UserID"];
            string DocName = Request.Form["DocName"];
            IFormFile file;
            file = GstFiles[0];
            //var GetExtention = System.IO.Path.GetExtension(file.FileName);
            var fileName = file.FileName;// System.IO.Path.GetFileNameWithoutExtension(Userid) + GetExtention;
            if (DocName == "ManualOffline")
            {
                var GetExtention = Path.GetExtension(file.FileName);
                fileName = file.Name + GetExtention;
            }
            Response = UploadFiledata(file, fileName, DocName);
            return Ok(Response);

        }
        private string UploadFiledata(IFormFile file, string Filename, string Dir)
        {
            string Response = "";
            try
            {
                var GetExtention = System.IO.Path.GetExtension(file.FileName);
                var fileName = Filename;// + GetExtention;
                string Url = "/Downloads/" + Dir + "/" + fileName;
                if (System.IO.File.Exists(GetCurrentPath() + Url))
                    Response = "Policy File already exist";
                else
                    using (var stream = new FileStream(GetCurrentPath() + Url, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        Response = "Success";
                    }
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Response;
        }
        private string GetCurrentPath()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }
        public IActionResult GetOffilePolicy([FromBody] GetOffilePDF item)
        {
            var Response = businessLayer.Getfile(item);
            return Ok(Response);
        }
        public IActionResult GetYearWiseCosolidate([FromBody] YearConsolidateParam Item)
        {
            var Response = businessLayer.GetYearWiseCosolidate(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ShowAboutConsolidate([FromBody] ShowAboutConsolidateParam Item)
        {
            var Response = businessLayer.ShowAboutConsolidate(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UserProgresive([FromBody] ConsolidateParam Item)
        {
            var Response = businessLayer.UserProgresive(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult OfflineHealthPolicy([FromBody] OfflineHealthPolicyParam Item)
        {
            var Response = businessLayer.OfflineHealthPolicy(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult OfflineHealthHLTPolicy([FromBody] BusinessReportReq param)
        {
            var Response = businessLayer.OfflineHealthHLTPolicy(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UploadBulkManualOfflineMotorBusiness([FromBody] BulkOfflineManualMotorParam item)
        {
            var Response = businessLayer.UploadBulkManualOfflineMotorBusiness(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UploadBulkManualOfflineHLTBusiness([FromBody] BulkOfflineManualHLTParam item)
        {
            var Response = businessLayer.UploadBulkManualOfflineHLTBusiness(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetStateList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetStateList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetStateThroughCityID([FromBody] GetStateParam Item)
        {
            var Response = businessLayer.GetStateThroughCityID(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetCityList([FromBody] GetCitiesParam Item)
        {
            var Response = businessLayer.GetCityList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetRTOList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetRTOList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DeleteOfflinePolicy([FromBody] OfflinePolicy Item)
        {
            var Response = businessLayer.DeleteOfflinePolicy(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetPolicyInfo([FromBody] OfflinePolicy Item)
        {
            var Response = businessLayer.GetPolicyInfo(Item);
            return Ok(Response);
        }

        #region Offline Life Policy 
        /*
         -By: Sunil
         -Upated on: 23 Aug 2021  
         */
        [HttpPost]
        public IActionResult SaveOfflineLifePolicy([FromBody] OfflineLifePolicyParam param)
        {
            var Response = businessLayer.OfflineLifePolicy(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UploadBulkOfflineLifeBusiness([FromBody] BulkOfflineLifeParam param)
        {
            var Response = businessLayer.UploadBulkOfflineLifeBusiness(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult OfflineLifePolicyReport([FromBody] BusinessReportReq param)
        {
            var Response = businessLayer.ManualOfflieTermLifeReport(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult OfflineLifePolicyReportDownload([FromBody] BusinessReportReq param)
        {
            var data = businessLayer.DownloadOfflineLifeReport(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult LifeHeaderBusiness([FromBody] CommonParam Item)
        {
            var response = businessLayer.OfflineLifeHeader(Item);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult GetOfflineLifePolicyInfo([FromBody] OfflinePolicy Item)
        {
            var Response = businessLayer.GetOfflineLifePolicyInfo(Item);
            return Ok(Response);
        }

        #endregion
    }
}