using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class VehicleModel
    {
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public string VehicleType { get; set; }
        public bool IsActive { get; set; }
        public string CombindID { get; set; }
        public string Manu_Vehicle { get; set; }
    }
}
