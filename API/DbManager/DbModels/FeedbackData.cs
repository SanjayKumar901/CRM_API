using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("FeedbackData")]
    public class FeedbackData
    {
        [Key]
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int UserID { get; set; }
        public int Rating { get; set; }
        public int FeedbackOptionID { get; set; }
        public DateTime? FeedbackDate { get; set; }
        public string FeedbackText { get; set; }
    }
}
