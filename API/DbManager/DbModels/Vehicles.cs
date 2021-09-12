using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Vehicles")]
    public class Vehicles
    {
        [Key]
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public int ManufacturerID { get; set; }
        public string VehicleType { get; set; }
        public bool IsActive { get; set; }
    }
    [Table("vw_vehicles")]
    public class vw_vehicles
    {
        [Key]
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public int ManufacturerID { get; set; }
        public string Manufacturername { get; set; }
        public string VehicleType { get; set; }
        public bool IsActive { get; set; }
    }
}
