using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("variants")]
    public class Variants
    {
        [Key]
        public int VariantID { get; set; }
        public int? VehicleID { get; set; }
        public string VariantName { get; set; }
        public int? FuelID { get; set; }
        public int? VehicleCC { get; set; }
        public int? SeatingCapacity { get; set; }
        public decimal? ExShowroomPrice { get; set; }
        public bool IsActive { get; set; }
    }
    [Table("vw_Variants")]
    public class vw_Variants
    {
        [Key]
        public int VariantID { get; set; }
        public int? ManufacturerID { get; set; }
        public int? VehicleID { get; set; }
        public int? FuelID { get; set; }
        public string VehicleType { get; set; }
        public string ManufacturerName { get; set; }
        public string VehicleName { get; set; }
        public string FuelName { get; set; }
        public string VariantName { get; set; }
        public bool IsActive { get; set; }
    }

}
