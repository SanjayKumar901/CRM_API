using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Manufacturer")]
    public class Manufacturer
    {
        [Key]
        public int ManufacturerID { get; set; }
        public string Manufacturername { get; set; }
        public bool IsBike { get; set; }
        public bool IsCar { get; set; }
        public bool IsActive { get; set; }
    }
}
