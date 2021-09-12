using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("HealthGotoPaymentData")]
    public class HealthGotoPaymentData
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public string HealthQuoteRequest { get; set; }
        public string HealthQuoteResponse { get; set; }
        public string HealthProposalRequest { get; set; }
        public string HealthProposalResponse { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
