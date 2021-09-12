using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Offlinerequestmotor")]
    public class Offlinerequestmotor
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Enquiryno { get; set; }
        public string InsurerFiles { get; set; }
        public string Remarks { get; set; }
    }
}
