using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("UploadPolicy")]
    public class UploadPolicy
    {
        [Key]
        public int UploadPolicyID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string RMName { get; set; }
        public string Region { get; set; }
        public string PolicyUrl { get; set; }
        public string RcURL { get; set; }
        public string Status { get; set; }
        public string SegmentID { get; set; }
        public string Pos_AgentName { get; set; }
        public string RTO { get; set; }
        public string Pos_AgentCode { get; set; }
        public string NCBPercent { get; set; }
        public string GUID { get; set; }
        public string Companies { get; set; }
        public string IsNCB { get; set; }
        public string CreatedDate { get; set; }
    }
}
