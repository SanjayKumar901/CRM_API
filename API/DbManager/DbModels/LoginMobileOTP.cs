using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("LoginMobileOTP")]
    public class LoginMobileOTP
    {
        [Key]
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string MobileNo { get; set; }
        public string Otp { get; set; }
        public DateTime OtpTime { get; set; }
    }
}
