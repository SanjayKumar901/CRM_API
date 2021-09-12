using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.PortalAPI.MotorAPI.HealthBal;
using API.PortalAPI.MotorAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.PortalAPI.MotorAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private IHealthBusinessLayer healthBusinessLayer;
        public HealthController(IHealthBusinessLayer _healthBusinessLayer)
        {
            healthBusinessLayer = _healthBusinessLayer;
        }
        [HttpPost]
        public async Task<IActionResult> SaveEnquiry([FromBody]HealthEnquiry item)
        {
            string Response = "";
            Response = await Task.Run(()=> healthBusinessLayer.SaveEnquiry(item));
            return Ok(Response);
        }
        public async Task<IActionResult> GotoProposal([FromBody]HealthGoToProposalPram item)
        {
            string Response = "";
            Response = await Task.Run(()=> healthBusinessLayer.GotoProposal(item));
            return Ok(Response);
        }
        public async Task<IActionResult> UpdateProposalDetails([FromBody]UpdateProposal item)
        {
            string Response = "";
            Response = await Task.Run(()=> healthBusinessLayer.UpdateProposalDetails(item));
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetHealthGotoPaymentData([FromBody] HealthGotoPaymentDataParam Item)
        {
            var Response = healthBusinessLayer.GetHealthGotoPaymentData(Item);
            return Ok(Response);
        }
    }
}