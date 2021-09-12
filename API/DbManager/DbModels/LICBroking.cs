using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("LICBroking")]
    public class LICBroking
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string AgencyCode { get; set; }
        public string APIURL { get; set; }
        public string Authorization { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RedirectionURL { get; set; }
    }
}
