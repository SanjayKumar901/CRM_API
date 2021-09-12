using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Cities")]
    public class Cities
    {
        [Key]
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? DistrictID { get; set; }
        public int StateID { get; set; }
        public bool IsActive { get; set; }
    }
}
