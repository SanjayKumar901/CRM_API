using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MappingBusinesModuleController : ControllerBase
    {
        public IActionResult SavePengingBusiness()
        {
            return Ok("");
        }
    }
}
