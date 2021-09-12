using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("SaveRequestResponseData")]
    public class SaveRequestResponseData
    {
        [Key]
        public int ID { get; set; }
        public string EnquiryNo { get; set; }
        public string RequestQuoteXml { get; set; }
        public string ResponseQuoteXml { get; set; }
        public string RequestProposalXml { get; set; }
        public string ResponseProposalXml { get; set; }
        public string RequestPaymentXml { get; set; }
        public string ResponsePaymentXml { get; set; }
        public string Product { get; set; }
    }
}
