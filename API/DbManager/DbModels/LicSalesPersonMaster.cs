using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("LicSalesPersonMaster")]
    public class LicSalesPersonMaster
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public string SalesPersonCode { get; set; }
    }
}
