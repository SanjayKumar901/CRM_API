using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class CompanyDetails
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string CubicCapacity { get; set; }
        public string VehicleType { get; set; }
        public string Variant { get; set; }
        public string fueltype { get; set; }
        public int isMapp { get; set; }
    }
}
