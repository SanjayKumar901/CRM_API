using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
using API.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Onboarding
{
    [Route("api/[controller]/[action]")]
    [EnableCors("cors")]
    [ApiController]
    public class OnboardingAPIController : ControllerBase
    {
        private readonly IBusinessLayer business;
        public OnboardingAPIController(IBusinessLayer _business)
        {
            business = _business;
        }
        [HttpGet]
        public IActionResult GetClientList()
        {
            var data = business.GetClientData();
            return Ok(data);
        }
        [HttpPost]
        public IActionResult CreateNewClient([FromBody] CreateCLientParam Item)
        {
            var data = business.CreateNewClient(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult CreateSuperAdmin([FromBody] SuperAdmin Item)
        {
            var data = business.CreateSuperAdmin(Item);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult CheckSuperAdmin([FromBody] CheckSuperAdminParam Item)
        {
            var data = business.CheckSuperAdmin(Item);
            return Ok(data);
        }
        [HttpGet]
        public IActionResult GetCompanyList()
        {
            var data = business.GetCompanies();
            return Ok(data);
        }
    }
}
