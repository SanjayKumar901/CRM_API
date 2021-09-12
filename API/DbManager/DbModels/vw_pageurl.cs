using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("vw_pageurl")]
    public class vw_pageurl
    {
        [Key]
        public int SrNo { get; set; }
        public string Privilegename { get; set; }
        public string URL { get; set; }
    }

    public class ReturnPageUrls:vw_pageurl
    {
        public string Script { get; set; }
        public bool IsChecked { get; set; }
    }
}
