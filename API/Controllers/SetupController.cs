using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BAL;
using API.CommonMethods;
using API.Model;
using API.SecurityAccesControl;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("cors")]
    [ApiController]
    //[Authorize]
    [StillLogin]
    public class SetupController : ControllerBase
    {
        IBusinessLayer businessLayer;
        UserModel model;
        public SetupController(IBusinessLayer _businessLayer)
        {
            businessLayer = _businessLayer;
            model = new UserModel();
        }
        [HttpPost]
        public IActionResult ImportUser(UserCreationList Item)
        {
            var Response = businessLayer.ImportUser(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Endusermapping(EnduserMapping Item)
        {
            var Response = businessLayer.Endusermapping(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetEndUserDetailWithMPD(EnduserMapping Item)
        {
            var Response = businessLayer.GetEndUserDetailWithMPD(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult RoleList(CommonParam Item)
        {
            var Response = businessLayer.RoleTypes();
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UserlistWithRoletype([FromBody] RoleTypeParam Item)
        {
            var Response = businessLayer.GetUserListWithRoleid(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SetupUserPrivilege([FromBody] MergeUserPrivilege Item)
        {
            var Response = businessLayer.SetupUserPrivilege(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SetupUserRoleType([FromBody] MergeUserPrivilege Item)
        {
            var Response = businessLayer.SetupUserRoleType(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult RegionlistWithUserID([FromBody] RoleTypeParam Item)
        {
            var Response = businessLayer.RegionlistWithUserID(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UpdateReportingManager([FromBody] ManageUserMapp Item)
        {
            var Response = businessLayer.UpdateReportingManager(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UpdateRegionWithTeam([FromBody] ManageUserMapp Item)
        {
            var Response = businessLayer.UpdateRegionWithTeam(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetMotors([FromBody] MotorParam item)
        {
            var Response = businessLayer.MotorList(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetModels([FromBody] MotorParam item)
        {
            var Response = businessLayer.ModelList(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetVariants([FromBody] MotorParam item)
        {
            var Response = businessLayer.Variants(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetCompanies([FromBody] CompaniesparamWithProduct item)
        {
            var Response = businessLayer.Companies(item);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult MappOrUnmaplist([FromBody]MappParam Item)
        {
            var Response = businessLayer.MappOrUnmaplist(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult VariantGetMapped([FromBody]MappVariant Item)
        {
            var Response = businessLayer.GetMapped(Item);
            return Ok(Response);
        }

        [HttpPost]
        public IActionResult EncryptOrDecrypt([FromBody] User item)
        {
            string Response = "";
            Response = Common.Encrypt(item.Userid.ToString());
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult VehicleDetails([FromBody] VehicleParam item)
        {
            dynamic Response = null;
            try
            {
                switch (item.VehicleProperty.ToLower())
                {
                    case "manufacturer":
                        Response = businessLayer.Getmanufacturer();
                        break;
                    case "vehicle":
                        Response = businessLayer.GetVehicles();
                        break;
                    case "fuels":
                        Response = businessLayer.GetFuels();
                        break;
                    case "variants":
                        Response = businessLayer.GetVariants();
                        break;
                }
            }
            catch(Exception ex) { }
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult AddManufacturer([FromBody]AddManufacturerParam Item)
        {
            var Response = businessLayer.AddManufacturer(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult AddVehicle([FromBody] AddVehicleParam Item)
        {
            var Response = businessLayer.AddVehicle(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult AddVariant([FromBody] AddVariantParam Item)
        {
            var Response = businessLayer.AddVariant(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetRegionList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetAllRegionList(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetBranchList([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetAllBranchListByClient(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveRegionWithBranch([FromBody]ListRegionWithBranch Item)
        {
            var Response = businessLayer.SaveRegionWithBranch(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Camapigns([FromBody] CamapignsParam Item)
        {
            var Response = businessLayer.Campaigns(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ImportRenewal([FromBody] ImportRenewal item)
        {
            var Response = businessLayer.ImportRenewal(item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UpdateAlterNateCode([FromBody] MergeUserAlterNateCode Item)
        {
            var Response = businessLayer.UpdateAlterNateCode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult UpdateRMCode([FromBody] MergeUserRMCode Item)
        {
            var Response = businessLayer.UpdateRMCode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetSampleBulkImportFle([FromBody]CommonParam Item)
        {
            var Response = businessLayer.GetSampleBulkImportFle(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetAgencyCode([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetAgencyCode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveAgencyCode([FromBody] AgencyCodeParam Item)
        {
            var Response = businessLayer.SaveAgencyCode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult SaveSalesPersonCode([FromBody] SalesPersonCodeParam Item)
        {
            var Response = businessLayer.SaveSalesPersonCode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult GetSalesPersonCode([FromBody] CommonParam Item)
        {
            var Response = businessLayer.GetSalesPersonCode(Item);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult ChecksumAPIForSpCode([FromBody] CommonParam Item)
        {
            var Response = businessLayer.ChecksumAPIForSpCode(Item);
            return Ok(Response);
        }
    }
}