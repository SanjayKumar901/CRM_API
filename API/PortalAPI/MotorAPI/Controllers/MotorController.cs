using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.PortalAPI.MotorAPI.Model;
using API.PortalAPI.MotorAPI.MotorBal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.PortalAPI.MotorAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MotorController : ControllerBase
    {
        private IMotorBusinessLayer motorBusinessLayer;
        public MotorController(IMotorBusinessLayer _motorBusinessLayer)
        {
            motorBusinessLayer = _motorBusinessLayer;
        }
        [HttpPost]
        public async Task<IActionResult> SaveEnquiryData(MotorEnquiry item)
        {
            string Response = "";
            Response = await Task.Run(()=> motorBusinessLayer.SaveEnquiry(item));
            return Ok(Response);
        }
        [HttpPost]
        public async Task<IActionResult> GotoProposal(GoToproposal Item)
        {
            string Response = "";
            Response = await Task.Run(()=> motorBusinessLayer.SaveGotoProposal(Item));
            return Ok(Response);
        }
        [HttpPost]
        public async Task<IActionResult> SavePolicydetails(PolicydetailsParam Item)
        {
            string Response = "";
            Response = await Task.Run(()=>motorBusinessLayer.Policydetails(Item));
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetOrUpdate([FromBody]updateTable Param)
        {
            dynamic Response =null;
            if (Param.GetOrSet == "Get")
                Response = motorBusinessLayer.DynamicGet(Param);
            else if(Param.GetOrSet == "Set")
                Response = motorBusinessLayer.DynamicSet(Param);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult CallAdvaceQuery([FromBody] QueryParam item)
        {
            dynamic data = null;
            switch (item.Option)
            {
                case "select":
                    data = motorBusinessLayer.SelectQuery(item.Query);
                    break;
                case "update":
                    data = motorBusinessLayer.UpdateQuery(item.Query);
                    break;
            }
            return Ok(data);
        }

        [HttpPost]
        public IActionResult RegisterPos([FromBody] RegisterPos item)
        {
            var response = motorBusinessLayer.RegisterPos(item);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult SendEmailInfoToEndUser([FromBody]SendmailToUser Item)
        {
            var Response = motorBusinessLayer.SendEmailInfoToEndUser(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GenEnquiryNo([FromBody] GenEnquiryNoParam Item)
        {
            var Response = motorBusinessLayer.GenEnquiryNo(Item);
            return Ok(Response);
        }
    }
}