using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public int? ClientID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Mobile { get; set; }
        public string StdCode { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public int? MaritalStatus { get; set; }
        public string Password { get; set; }
        public string PasswordRecoveryKey { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? Status { get; set; }
        public int? OccupationID { get; set; }
        public string EnquiryNo { get; set; }
    }
}
