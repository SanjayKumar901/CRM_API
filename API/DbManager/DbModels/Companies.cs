using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Companies")]
    public class Companies
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool HealthInsurance { get; set; }
        public bool CarInsurance { get; set; }
        public bool TermlifeInsurance { get; set; }
        public bool TravelInsurance { get; set; }
        public bool TwowheelerInsurance { get; set; }
        public bool IsActive { get; set; }
    }
}
