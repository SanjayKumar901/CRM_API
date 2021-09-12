using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("CrmCityState")]
    public class CrmCityState
    {
        public int ID { get; set; }
        public int Pin_code { get; set; }
        public string State_Name { get; set; }
        public string District_Name { get; set; }
    }
}
