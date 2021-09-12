using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("sp_OfflineLead")]
    public class sp_OfflineLead
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public string InsurerFiles { get; set; }
        public string UserName { get; set; }
        public string RmCode { get; set; }
        public int? KeyAccountManager { get; set; }
        public string Remarks { get; set; }
    }
}
