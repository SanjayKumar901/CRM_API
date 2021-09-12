using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ClientMaster")]
    public class ClientMaster: ClientMasterBase
    {
        [Key]
        public int Id { get; set; }
        
    }
    public class ClientMasterBase
    {
        public string clientId { get; set; }
        public string companyName { get; set; }
        public string companyURL { get; set; }
        public string contactNo { get; set; }
        public string emailAddress { get; set; }
        public DateTime Create_Date { get; set; }
        public string CompanyLogo { get; set; }
        public string CoreAPIURL { get; set; }
        public string clientURL { get; set; }
    }
}
