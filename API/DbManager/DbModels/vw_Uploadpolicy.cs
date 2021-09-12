using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("vw_Uploadpolicy")]
    public class vw_Uploadpolicy
    {
        [Key]
        public int UploadPolicyID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? RMID { get; set; }
        public int? RegionID { get; set; }
        public string PolicyUrl { get; set; }
        public string RcURL { get; set; }
        public string Status { get; set; }
        public string SegmentName { get; set; }
        public int? RTOID { get; set; }
        public string PosCode { get; set; }
        public string NCBPercent { get; set; }
        public string GUID { get; set; }
        public string Companies { get; set; }
        public bool IsNCB { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RMName { get; set; }
        public string UserName { get; set; }
        public string Region { get; set; }
        public string RTOName { get; set; }
    }
}
