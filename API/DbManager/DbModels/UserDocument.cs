using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{

    [Table("UserDocument")]
    public class UserDocument
    {
        [Key]
        public int UserID { get; set; }
        public string ProfileImage_URL { get; set; }
        public string Adhaar_Front_URL { get; set; }
        public string Adhaar_Back_URL { get; set; }
        public string PAN_URL { get; set; }
        public string QualificationCertificate_URL { get; set; }
        public string CancelCheque_URL { get; set; }
        public string POS_Certificate_Front_URL { get; set; }
        public string GST_CERTIFICATE_URL { get; set; }
        public string TearnAndCondition { get; set; }
        public DateTime? AdharFrontDate { get; set; }
        public DateTime? PanDate { get; set; }
        public DateTime? QualificationDate { get; set; }
        public DateTime? CancelChequeDate { get; set; }
        public DateTime? POSCertificateDate { get; set; }
        public DateTime? GSTCertificateDate { get; set; }
        public DateTime? AdharBackDate { get; set; }
        public DateTime? TermConditionDate { get; set; }
        public bool? DocVerified { get; set; }
        public bool? IsAdharFrontCheck { get; set; }
        public bool? IsAdharBackCheck { get; set; }
        public bool? IsPanCheck { get; set; }
        public bool? IsQualificationCheck { get; set; }
        public bool? IsCancelChequeCheck { get; set; }
        public bool? IsPosCertificateCheck { get; set; }
        public bool? IsGSTCertificateCheck { get; set; }
        public bool? IsTermCheck { get; set; }
    }
    public class DocumentWithAPI
    {
        public UserDocument UserDocuments { get; set; }
        public string Api { get; set; }
        public List<OfflineQueryRelatedMessage> offlineQueryRelatedMessages { get; set; }
        public string ProfilePic { get; set; }
    }
}
