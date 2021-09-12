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
    public class BookingPolicyController : ControllerBase
    {
        IBusinessLayer businessLayer;
        public BookingPolicyController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
        }
        [HttpPost]
        public IActionResult GetCRMCurrentUrl(CommonParam item)
        {
            var response = businessLayer.GetCRMCurrentUrl(item);
            return Ok(response);
        }
    }
}