using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.PortalAPI.MotorAPI.Model;
using API.PortalAPI.MotorAPI.TermLifeBal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.PortalAPI.MotorAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TermLifeController : ControllerBase
    {
        private ITermLifeBusinessLayer iTermLifeBusinessLayer;
        public TermLifeController(ITermLifeBusinessLayer _iTermLifeBusinessLayer)
        {
            iTermLifeBusinessLayer = _iTermLifeBusinessLayer;
        }
        [HttpPost]
        public IActionResult SaveEnquiryData(LifeEnquiry item)
        {
            string Response = "";
            Response = iTermLifeBusinessLayer.SaveEnquiry(item);
            return Ok(Response);
        }
    }
}
