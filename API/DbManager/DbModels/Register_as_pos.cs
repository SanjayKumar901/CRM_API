using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("register_as_pos")]
    public class Register_as_pos
    {
        [Key]
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AadhaarNo { get; set; }
        public string PanNo { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LeadSource { get; set; }
        public int? UserID { get; set; }
        public string Qualification { get; set; }
        public string Status { get; set; }
        public bool? IsDuplicate { get; set; }
    }
    public class Response_Register_as_pos
    {
        public int RoleID { get; set; }
        public IEnumerable<sp_PosRequestData> Register_as_poss { get; set; }
    }

    public class sp_PosRequestData
    {
        [Key]
        public int? ID { get; set; }
        public int? ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AadhaarNo { get; set; }
        public string PanNo { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string CreatedDate { get; set; }
        public string LeadSource { get; set; }
        public int? UserID { get; set; }
        public string Qualification { get; set; }
        public string Status { get; set; }
        public bool? IsDuplicate { get; set; }
        public string ActiveUser { get; set; }
        public string Rolename { get; set; }
        public string UserInfo { get; set; }
    }
}
