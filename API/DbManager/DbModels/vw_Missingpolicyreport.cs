using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("vw_missingpolicyreport")]
    public class vw_Missingpolicyreport
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string EnquiryStatus { get; set; }
        public string PolicyNo { get; set; }
        public string CompanyName { get; set; }
        public string VehicleNo { get; set; }
        public string ChasisNo { get; set; }
    }
}
