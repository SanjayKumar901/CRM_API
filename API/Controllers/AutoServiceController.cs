using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("cors")]
    [ApiController]
    public class AutoServiceController : ControllerBase
    {

        IBusinessLayer businessLayer;
        public AutoServiceController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        public IActionResult SendNotificationForRenew()
        {
            var Response = businessLayer.SendNotificationForRenew();
            return Ok(Response);
        }
    }
}
