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
    public class EndUserControllController : ControllerBase
    {
        private readonly IBusinessLayer business;
        public EndUserControllController(IBusinessLayer _business)
        {
            business = _business;
        }
        [HttpPost]
        public IActionResult GetProductData([FromBody] EndUserProductDetailsParam Item)
        {
            IEnumerable<dynamic> Response = null;
            switch (Item.Product)
            {
                case "Car":
                    Response = business.GetMotordata(Item);
                    break;
                case "Two":
                    Response = business.GetMotordata(Item);
                    break;
                case "Health":
                    Response = business.GetHealthdata(Item);
                    break;
            }
            return Ok(Response);
        }
    }
}
