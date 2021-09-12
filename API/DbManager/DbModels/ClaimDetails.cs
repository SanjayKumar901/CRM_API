using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("ClaimDetails")]
    public class ClaimDetails: ClaimBase
    {
        public DateTime CreatedDate { get; set; }
    }
    public class ClaimBase
    {
        [Key]
        public int ID { get; set; }
        public string MobileNo { get; set; }
        public string DetailMsg { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ClientID { get; set; }
    }
}
