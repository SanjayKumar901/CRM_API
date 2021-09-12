using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.BAL;
using API.CommonMethods;
using API.Model;
using API.SecurityAccesControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.AspNet.WebApi.Cors;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("cors")]
    //[EnableCors(origins:"",headers:"*",methods:"*")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public static string Secret = Guid.NewGuid().ToString();
        private readonly IBusinessLayer business;
        public AccountController(IBusinessLayer _business)
        {
            business = _business;
        }
        public IActionResult LoginUser([FromBody]Login Model)
        {
            if (ModelState.IsValid)
            {
                var model = business.LoginUser(Model);
                return Ok(model);
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }
        public IActionResult ResetBeforeLoginPass([FromBody] ResetPassParam Item)
        {
            var Response = "";
            Response =business.ResetBeforeLoginPass(Item);
            return Ok(Response);
        }
        public IActionResult LoginUserWithOTP([FromBody]Login Model)
        {
            if (ModelState.IsValid)
            {
                var model = business.LoginWithOTP(Model);
                return Ok(model);
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }
        public IActionResult DomainLogo([FromBody] UrlBase Item)
        {
            var Data = business.GetClientData(Item);
            return Ok(Data);
        }
        public IActionResult TokeExist([FromBody]CommonParam param)
        {
            string Response = "";
            if (business.checkToken(param.Token) == "Not")
            {
                Response = "Expired";
                return Unauthorized(Response);
            }
            Response = "Success";
            return Ok(Response);
        }
        public IActionResult TestLoginUser(string userid,string pass)
        {
            if (userid == "shoaib" && pass == "pass123")
            {
                string TokenString = Authenticate(userid);
                return Ok(TokenString);
            }
            return BadRequest();
        }
        public IActionResult CheckExistance([FromBody]Login Model)
        {
            string Response = "";
            Response = business.CheckExistOrNot(Model);
            return Ok(Response);
        }
        public string Authenticate(string username)
        {
            // generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Startup.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Associate"),
                    new Claim(ClaimTypes.SerialNumber, "fddfsa"),
                    new Claim(ClaimTypes.Email, "shoaib@gmil.aomc"),
                    new Claim(ClaimTypes.MobilePhone, "9891687364"),
                }),
                //Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Token = tokenHandler.WriteToken(token);
            // remove password before returning
            return Token;
        }
        [Authorize]
        public IActionResult TestAccess()
        {
            return Ok("Done");
        }
        public IActionResult SessionOut()
        {
            string Response = "Session is abort";
            return BadRequest(Response);
        }
        [HttpPost]
        [StillLogin]
        public IActionResult ResetPass([FromBody] ResetPass item)
        {
            string Response = "";
            if (item.NewPass.Length < 8)
            {
                Response = "Password length can't be less than 8.";
            }
            else if (item.NewPass == item.ConfirmPass)
            {
                var checkNumeric = item.NewPass.ToCharArray().Where(row => char.IsNumber(row));
                if (checkNumeric.Count() <= 0)
                {
                    Response += "\nMinimum one numeric character required.";
                    
                }
                var IsnotLetterOrDigit = item.NewPass.ToCharArray().Where(row => !char.IsLetterOrDigit(row));
                if (IsnotLetterOrDigit.Count() <= 0)
                {
                    Response += "\nMinimum one special character required.";
                   
                }
                var IsUppercase = item.NewPass.ToCharArray().Where(row => char.IsUpper(row));
                if (IsUppercase.Count() <= 0)
                {
                    Response += "\nMinimum one Upper character required.";
                }
                var IsLower = item.NewPass.ToCharArray().Where(row => char.IsLower(row));
                if (IsLower.Count() <= 0)
                {
                    Response += "\nMinimum one Lower character required.";
                }
                if (Response=="")
                    Response = business.ResetPass(item);
            }
            else
            {
                Response = "Not Match";
            }
            GO:
            return Ok(Response);
        }
        [HttpPost]
        [StillLogin]
        public IActionResult LogOut([FromBody] CommonParam Item)
        {
            var Response = business.LogOut(Item);
            if (Response == "Data Deleted Successfully.")
                Response = "You are successfully log out.";
            return Ok(Response);
        }
        public IActionResult LogWithLinkiUser([FromBody]Login Model)
        {
            var Response = business.LogWithLinkiUser(Model);
            return Ok(Response);
        }
        [HttpPost]
        public IActionResult Renewalenquiry([FromBody]FindEnquiryParam Item)
        {
            var FilterEnquiry = business.Renewalenquiry(Item);
            return Ok(FilterEnquiry);
        }
        public IActionResult Decrypt(string encrypt)
        {
            string ss = Common.Decrypt(encrypt.Replace(" ","+"));
            return Ok(ss);
        }
        public IActionResult Encrypt(string encrypt)
        {
            string ss = Common.Encrypt(encrypt);
            return Ok(ss);
        }
        [HttpPost]
        public IActionResult EndUserLogin([FromBody] Login Item)
        {
            if (ModelState.IsValid)
            {
                var model = business.EndLoginUser(Item);
                return Ok(model);
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }
        [HttpPost]
        public IActionResult EndUserMatchOtp([FromBody] Login Item)
        {
            if (ModelState.IsValid)
            {
                var model = business.MatchEnduserOTP(Item);
                return Ok(model);
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }
        [HttpPost]
        public IActionResult EndUserTokeExist([FromBody] CommonParam param)
        {
            string Response = "";
            if (business.CheckEndUserToken(param.Token) == "Not")
            {
                Response = "Expired";
                return Unauthorized(Response);
            }
            Response = "Success";
            return Ok(Response);
        }

        [HttpPost]
        [StillLogin]
        public IActionResult CheckAuthorization([FromBody]UserAuth  Item)
        {
            string Response = "";
            Response = business.Authorization(Item);
            return Ok(Response);
        }
        
        
    }
}