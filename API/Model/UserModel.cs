using API.DbManager.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class UserModel
    {
        public int UserID { get; set; }
        public int? ClientID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool ISActive { get; set; }
        public int? RoleID { get; set; }
        //public DateTime Createddate { get; set; }
        public string status { get; set; }
        public string mobileNo { get; set; }
    }
    public class BaseUser
    {
        public int UserID { get; set; }
        public int? ClientID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public class EndUserModel:BaseUser
    {
        public string Mobile { get; set; }
    }
    public class GetFilterUserList : BaseUser { }
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
    public class RMWIthMobile
    {
        public string RmName { get; set; }
        public string MobileNo { get; set; }
        public string DOcStatus { get; set; }
        public bool active { get; set; }
        public bool IsAffilate { get; set; }
        public List<UserDocWithStatus> userDocWithStatus { get; set; }
    }
    public class PrivilegeListWithRoleid
    {
        public int RoleID { get; set; }
        public IList<PrivilegeMaster> privilegeMaster { get; set; }
    }
}
