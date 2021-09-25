using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ZoopVehicleRegistration")]
    public class ZoopVehicleRegistration
    {
        [Key]
        public int Id { get; set; }
        public string response_id { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }
        public string transaction_status { get; set; }
        public DateTime request_timestamp { get; set; }
        public DateTime response_timestamp { get; set; }
        public string blackList_status { get; set; }
        public string chassis_no { get; set; }
        public string engine_no { get; set; }
        public string financier { get; set; }
        public string fitness_upto { get; set; }
        public string fuel { get; set; }
        public string insurance_details { get; set; }
        public string insurance_validity { get; set; }
        public string license_address { get; set; }
        public string maker { get; set; }
        public string mv_tax_upto { get; set; }
        public string owner_name { get; set; }
        public string permit_type { get; set; }
        public string permit_validity { get; set; }
        public string pollution_norms { get; set; }
        public string puc_no_upto { get; set; }
        public string regist_no { get; set; }
        public string registration_date { get; set; }
        public string registration_no { get; set; }
        public string report_id { get; set; }
        public string status { get; set; }
        public string vehicle_class { get; set; }
    }
}
