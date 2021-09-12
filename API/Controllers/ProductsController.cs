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
    //[Authorize]
    [StillLogin]
    public class ProductsController : ControllerBase
    {
        IBusinessLayer businessLayer;
        UserModel model;
        public ProductsController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
            model = new UserModel();
        }
        [HttpPost]
        public IActionResult GetEnquiryMaster([FromBody] CommonParam item)
        {
            var Response = businessLayer.BindEnquiryTypeMaster();
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetRequestResponse([FromBody] ReqResParam item)
        {
            var Response = businessLayer.GetReqResData(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetRenewData([FromBody] RenewDataList item)
        {
            dynamic Response = null;
            switch (item.Product)
            {
                case "Motor":
                    Response = businessLayer.RenewData(item);
                    break;
                case "Health":
                    Response = businessLayer.RenewData(item);
                    break;
            }
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetAllReqstAndResponse([FromBody] CallRequestResponseParam item)
        {
            var Response = businessLayer.GetAllReqstAndResponse(item);
            return Ok(Response);
        }
    }
}