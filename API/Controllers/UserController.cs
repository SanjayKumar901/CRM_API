using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.BAL;
using API.CommonMethods;
using API.DbManager.DbModels;
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
    public class UserController : ControllerBase
    {

        IBusinessLayer businessLayer;
        UserModel model;
        public UserController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
            model = new UserModel();
        }
        [HttpPost]
        public IActionResult RoleTypes([FromBody] RoleTypeParam param)
        {
            var data = businessLayer.RoleTypeWithID(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult GetUserInfo([FromBody] CommonParam param)
        {
            model = businessLayer.UserInfoCheckWithStatus(param);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult RoleTeamList([FromBody] RoleTypeParam param)
        {
            var data = businessLayer.RoleTeamList(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult TeamList([FromBody] RoleTypeParam item)
        {
            model = CommonMethods.Common.DecodeToken(item.Token);
            TeamUserListParam param = new TeamUserListParam()
            {
                ClientID = model.ClientID.Value,
                RoleID = item.ID,
                UserID = model.UserID.ToString()
            };

            var data = businessLayer.TeamUserList(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult UserallocatedDetail([FromBody] RoleTypeParam item)
        {
            model = CommonMethods.Common.DecodeToken(item.Token);
            BaseParam param = new BaseParam()
            {
                UserID = item.ID.ToString(),
                ClientID = model.ClientID.Value
            };
            var data = businessLayer.UserAllocation(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult UserSaverData([FromBody] UserCreationParam param)
        {
            var data = businessLayer.UserMasterSave(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult UserListData([FromBody] FilterUserByDate item)
        {
            model = Common.DecodeToken(item.Token);
            FilterUserByDate param = new FilterUserByDate()
            {
                ClientID = model.ClientID.Value,
                UserID = model.UserID.ToString(),
                startDate = item.startDate,
                endDate = item.endDate
            };
            var data = businessLayer.UserListWithUserID(param);
            return Ok(data);
            //var data = businessLayer.FilterUserByNameMobileEmail(item);
            //return Ok(data);
        }
        [HttpPost]
        public IActionResult UserListDataForMap([FromBody] FilterText item)
        {
            var data = businessLayer.FilterUserByNameMobileEmail(item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult GetCities([FromBody] RoleTypeParam item)
        {
            var data = businessLayer.GetCities(item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult GetRegions([FromBody] CommonParam Item)
        {
            var data = businessLayer.GetRegions(Item);
            return Ok(data);
        }
        public IActionResult AddUser()
        {
            string test = "shoaib is great.";
            return Ok(test);
        }
        [HttpPost]
        public IActionResult SingleUserInfo([FromBody] User param)
        {
            var data = businessLayer.UserInfo(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult SubUserListInfo([FromBody] User param)
        {
            var data = businessLayer.SubUserList(param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult MyprofileData([FromBody] User Param)
        {
            var data = businessLayer.MyProfile(Param);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Payoutdata([FromBody] PayyoutParam param)
        {
            string Response = "";
            Response = businessLayer.PayoutData(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult PersonalDetails([FromBody] PersonalDetails param)
        {
            string Response = "";
            Response = businessLayer.PersonalDetails(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult PrivilegeList([FromBody] User Param)
        {
            var data = businessLayer.PrivilegeList(Param);
            return Ok(data);
        }
        public IActionResult SavePrivileges(PrivilegeMap param)
        {
            string Response = "";
            if (param.Option == "Save")
                Response = businessLayer.SavePrivileges(param);
            else if (param.Option == "Delete")
                Response = businessLayer.DeletePrivileges(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Gstin([FromBody] GSTINParam param)
        {
            string Response = "";
            Response = businessLayer.Gstin(param);
            return Ok(Response);
        }

        #region Comment
        /*
        [AllowAnonymous]
        [HttpPost]
        public IActionResult GstCertificateFiles()
        {
            string ok = "";
            var GstFiles = Request.Form.Files;
            IFormFile file = GstFiles[0];
            var GetExtention = System.IO.Path.GetExtension(file.FileName);
            var fileName = file.Name + GetExtention;
            using (var stream = new FileStream(GetCurrentPath() + "/UserFileUploads/" + fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            string Gstin = Request.Form["Gstin"];
            string Userid = Request.Form["UserID"];
            GSTCertificateParam param = new GSTCertificateParam()
            {
                GST_CERTIFICATE = Gstin,
                GST_CERTIFICATE_URL = "/UserFileUploads/" + fileName,
                Userid = Convert.ToInt32(Userid)
            };
            businessLayer.GSTCeritificate(param);
            return Ok(ok);
        }
        */
        #endregion  Comment
        [AllowAnonymous]
        public IActionResult UploadFiles()
        {
            string Response = "";
            var GstFiles = Request.Form.Files;
            string Userid = Request.Form["UserID"];
            string DocName = Request.Form["DocName"];
            Userid = Common.Decrypt(Userid);
            DocumentsParam param = new DocumentsParam();
            param.Userid = Convert.ToInt32(Userid);
            IFormFile file;
            file = GstFiles[0];
            var GetExtention = System.IO.Path.GetExtension(file.FileName);
            var fileName = System.IO.Path.GetFileNameWithoutExtension(Userid) + GetExtention;
            switch (DocName)
            {
                case "Certificate":
                    //[Userid + "_FileEducation"];
                    param.QualificationCertificate_URL = UploadFiledata(file, fileName, DocName);
                    break;
                case "Cheque":
                    //file = GstFiles[Userid + "_FileCancelledCheque"];
                    param.CancelCheque_URL = UploadFiledata(file, fileName, DocName);
                    break;
                case "PANCard":
                    //file = GstFiles[Userid + "_FilePANCard"];
                    param.PAN_URL = UploadFiledata(file, fileName, DocName);
                    break;
                case "AadharCardFront":
                    //file = GstFiles[Userid + "_FileAadharCardFront"];
                    param.Adhaar_Front_URL = UploadFiledata(file, fileName, DocName);
                    break;
                case "AadharCardBack":
                    //file = GstFiles[Userid + "_FileAadharCardBack"];
                    param.Adhaar_Back_URL = UploadFiledata(file, fileName, DocName);
                    break;
                case "TandC":
                    //file = GstFiles[Userid + "_FileTandC"];
                    param.TearnAndCondition = UploadFiledata(file, fileName, DocName);
                    break;
                case "UserProfilePic":
                    //file = GstFiles[Userid + "_FileTandC"];
                    param.UserPic = UploadFiledata(file, fileName, DocName);
                    break;
                case "GSTFile":
                    //file = GstFiles[Userid + "_FileTandC"];
                    param.GSTFile = UploadFiledata(file, fileName, DocName);
                    break;
            }
            Response = businessLayer.DocUrls(param, DocName);
            return Ok(Response);
        }
        private string UploadFiledata(IFormFile file, string Filename, string Dir)
        {
            try
            {
                var GetExtention = System.IO.Path.GetExtension(file.FileName);
                var fileName = Filename;// + GetExtention;
                string Url = "/Downloads/" + Dir + "/" + fileName;
                using (var stream = new FileStream(GetCurrentPath() + Url, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Url;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private string GetCurrentPath()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }
        [HttpPost]
        public IActionResult Dereg([FromBody] User Param)
        {
            var Response = businessLayer.Deregister(Param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ReferAndPosSeries([FromBody] RoleTypeParam item)
        {
            var Response = businessLayer.ReferAndPosSeries(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UserInfo([FromBody] CommonParam Item)
        {
            model = CommonMethods.Common.DecodeToken(Item.Token);
            return Ok(model);
        }
        [HttpPost]
        public IActionResult PriviligeList([FromBody] CommonParam Item)
        {
            var Data = businessLayer.PriviligeList(Item);
            return Ok(Data);
        }
        [HttpPost]
        public IActionResult GenerateLink(GenerateLinkParam param)
        {
            string Response = "";
            Response = businessLayer.GenerateLink(param);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult ActiveUserList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.ActiveUserList(Item);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult LogoUpload()
        {
            string Response = "";
            try
            {
                UserModel Um = API.CommonMethods.Common.DecodeToken(Request.Form["Token"].ToString());
                var GstFiles = Request.Form.Files;
                IFormFile file = GstFiles[0];
                var GetExtention = System.IO.Path.GetExtension(file.FileName);
                var fileName = Um.ClientID.ToString() + GetExtention;
                try
                {
                    string Url = GetCurrentPath() + "/Downloads/Logo/" + fileName;
                    using (var stream = new FileStream(Url, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    Response = businessLayer.UploadLogoUrl(Um, fileName);
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

        [HttpPost]
        public IActionResult UserProfilePic()
        {
            string Response = "";
            UserModel Um = API.CommonMethods.Common.DecodeToken(Request.Form["Token"].ToString());
            var GstFiles = Request.Form.Files;
            IFormFile file = GstFiles[0];
            var GetExtention = System.IO.Path.GetExtension(file.FileName);
            var fileName = Um.UserID.ToString() + GetExtention;
            try
            {
                string Url = GetCurrentPath() + "/Downloads/UserPictures/" + fileName;
                using (var stream = new FileStream(Url, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Response = businessLayer.UploadProfilePic(Um, fileName);
            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UnlockIncorrectAttempt([FromBody] BaseParam Item)
        {
            var Response = businessLayer.UnlockIncorrectAttempt(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUserFullDetails([FromBody] CommonParam Item)
        {
            var Details = businessLayer.GetUserDetilas(Item);
            return Ok(Details);
        }
        [HttpPost]
        public IActionResult UpdateUserinfo([FromBody] UpdateUserParam Item)
        {
            var Details = businessLayer.UpdateUserinfo(Item);
            return Ok(Details);
        }
        [HttpPost]
        public IActionResult UpdateAlternetCode([FromBody] UpdateUserParam Item)
        {
            var Response = businessLayer.UpdateAlterNet(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUserReport([FromBody] GetUserReportParam Item)
        {
            string Response = "";
            model = Common.DecodeToken(Item.Token);
            Response = businessLayer.GetUserReport(model.UserID, model.ClientID.Value, Item.Option,Item.StartDate,Item.EndDate);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUserFilterByRole([FromBody] FilterRoleUser item)
        {
            var Response = businessLayer.GetUserFilterByRole(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUserListWithFilter([FromBody] FilterUser Item)
        {
            string Response = "";
            model = Common.DecodeToken(Item.Token);
            Response = businessLayer.GetUserReportWithFilter(Item.UserID, model.ClientID.Value);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUsersListFilterByUserID([FromBody] FilterUserByDate Item)
        {
            model = Common.DecodeToken(Item.Token);
            FilterUserByDate basep = new FilterUserByDate()
            {
                ClientID = model.ClientID.Value,
                Token = Item.Token,
                UserID = Item.UserID.ToString(),
                startDate = Item.startDate,
                endDate = Item.endDate
            };
            var Response = businessLayer.UserListWithUserID(basep);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetCertificate([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetCertificate(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UserVerification([FromBody] UserVerificationParam item)
        {
            var Response = businessLayer.UserVerification(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult RegionNDBranch([FromBody] CommonParam item)
        {
            var Response = businessLayer.RegionNDBranch(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUserRegionndBranch([FromBody] User param)
        {
            var Response = businessLayer.GetUserRegionndBranch(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetPosExamSummary([FromBody] CommonParam param)
        {
            var Response = businessLayer.GetPosExamSummary(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult RemovePosDoc([FromBody] PosDocParam Item)
        {
            var Response = businessLayer.RemovePosDoc(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult DocumentCheck([FromBody] DocCHeckParam param)
        {
            string Response = businessLayer.DocumentCheck(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UsersFeedbackList([FromBody] CommonParam param)
        {
            var Response = businessLayer.UsersFeedbackList(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CheckPinAvailable([FromBody] CheckPinParam param)
        {
            var Response = businessLayer.CheckPinAvailable(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UpdatePincode([FromBody] UpdatePinParam Item)
        {
            var Response = businessLayer.UpdatePincode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetStateCity([FromBody] CheckPinParam Item)
        {
            var Response = businessLayer.GetStateCity(Item);
            return Ok(Response);
        }
    }
}