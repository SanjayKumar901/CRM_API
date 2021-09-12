using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Apilistpvc")]
    public class Apilistpvc
    {
        [Key]
        public int ID { get; set; }
        public string TypeVehicle { get; set; }
        public bool IsAcitve { get; set; }
        public int ClientID { get; set; }
        public string BrokerName { get; set; }
        public string IsActive_For_Comprehensive { get; set; }
        public string IsActive_For_OdOnly { get; set; }
        public string IsActive_For_ThirdParty { get; set; }
    }
}
