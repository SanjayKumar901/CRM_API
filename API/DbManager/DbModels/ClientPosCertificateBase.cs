using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ClientPosCertificateBase")]
    public class ClientPosCertificateBase
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string HTMLFormat { get; set; }
    }
}
