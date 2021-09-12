using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("MissingPolicyReport")]
    public class MissingPolicyReport
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string EnquiryStatus { get; set; }
        public string PolicyNo { get; set; }
        public int Insurer { get; set; }
        public string VehicleNo { get; set; }
        public string ChasisNo { get; set; }
    }
}
