using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("FeedbackOption")]
    public class FeedbackOption
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Options { get; set; }
    }
}
