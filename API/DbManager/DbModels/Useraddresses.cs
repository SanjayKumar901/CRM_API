using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Useraddresses")]
    public class Useraddresses
    {
        [Key]
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public int? CityID { get; set; }
        public int? StateID { get; set; }
        public int? PinCode { get; set; }
        public int? CountryID { get; set; }
        public bool? IsPrimary { get; set; }
        public string Mobile_No { get; set; }
    }
}
