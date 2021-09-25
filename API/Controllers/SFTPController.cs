using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
using API.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("cors")]
    [ApiController]
    public class SFTPController : ControllerBase
    {
        IBusinessLayer businessLayer;
        public SFTPController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        [HttpPost]
        public IActionResult GetBusinessReport([FromBody]FromDateToDate param)
        {
            var Response = businessLayer.GetMotorBusinessReport(param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetUserDataReport([FromBody] DownloadUserParam Item)
        {
            var Response = businessLayer.GetUserDataReport(Item.UserID, Item.ClientID, Item.Option, Item.StartDate, Item.EndDate);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult GetOfflineBusinessReport([FromBody] FromDateToDate param)
        {
            var Response = businessLayer.GetOfflineBusinessReport(param);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult SendMail(SFTPMail param)
        {
            if (param == null)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            var Response = businessLayer.SendSFTPMail(param);
            return Ok(Response);
        }

        #region Zoho Signup
        /*
         -By: Sunil
         -Updated on: 31 Aug 2021
         */
        [HttpPost]
        public IActionResult GetUserForDocSign(FromDateToDate param)
        {
            if (param == null)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            var Response = businessLayer.GetUsersforDocSign(param);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult PosMailing(POSMailingIn param)
        {
            if (param == null)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            var Response = businessLayer.POSMailingForStamp(param);
            return Ok(Response);
        }


        [HttpPost]
        public IActionResult UploadPosFile(POSUpload param)
        {
            if (param == null)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            if (string.IsNullOrEmpty(param.Token) || param.posList.Count == 0)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            var Response = businessLayer.POSStampUpdate(param);
            return Ok(Response);
        }

        [HttpGet]
        public IActionResult UpdatePosZohoStatus(int UserId = 0)
        {
            if (UserId == 0)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            var Response = businessLayer.POSZohoStatusUpdate(UserId);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UpdatePosStamp(POSStampUpdate param)
        {
            if (param == null)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            if (param.UserID == 0)
            {
                return BadRequest("Parametrs cannot be empty");
            }
            var stmpParam = new POSUpload();
            stmpParam.posList = new List<DbManager.DbModels.PosStampMaster>();
            stmpParam.posList.Add(new DbManager.DbModels.PosStampMaster
            {
                SignDate = param.SignDate,
                StampID = param.StampID,
                UserID = param.UserID,
                POSCode = param.POSCode,
                IIBDate = param.IIBDate
            });
            var Response = businessLayer.POSStampUpdate(stmpParam);
            return Ok(Response);
        }
        #endregion
        
          #region Zoop 
        [HttpPost]
        public IActionResult ZoopSaveVehicle([FromBody] ZoopModel param)
        {
            var Response = businessLayer.ZoopSaveVehicle(param);
            return Ok(Response);
        }

        [HttpGet]
        public IActionResult ZoopGetVehicle(string regNumber)
        {
            var Response = businessLayer.ZoopGetVehicle(regNumber);
            return Ok(Response);
        }
        #endregion
    }
}
