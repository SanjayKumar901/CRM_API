using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
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
    [StillLogin]
    public class PortalMasterSetupController : ControllerBase
    {
        IBusinessLayer businessLayer;
        public PortalMasterSetupController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        [HttpPost]
        public IActionResult SavePosMotorQuoteSetup([FromBody] SaveMotoRestrictQuoteLstParam Item)
        {
            var Response = businessLayer.SavePosMotorQuoteSetup(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetInsurerCompanies([FromBody] BrokerFetchParam Item)
        {
            if (Item.Product == "MOT") {
                var Response = businessLayer.GetClientInsurer(Item);
                return Ok(Response);
            }
            else
            {
                var Response = businessLayer.GetHLTClientInsurer(Item);
                return Ok(Response);
            }
        }
        [HttpPost]
        public IActionResult SavePosHLTQuoteSetup([FromBody] SaveHLTRestrictQuoteLstParam Item)
        {
            var Response = businessLayer.SavePosHLTQuoteSetup(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetHLTQuoteSetup([FromBody] BrokerFetchParam Item)
        {
            var Response = businessLayer.GetHLTQuoteSetup(Item);
            return Ok(Response);
        }
    }
}
