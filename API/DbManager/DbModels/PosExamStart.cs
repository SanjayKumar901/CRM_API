using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("posexamstart")]
    public class PosExamStart
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ClientID { get; set; }
        public bool Start { get; set; }
        public bool Automatically { get; set; }
        public DateTime? Timing { get; set; }
        public bool? TrainingComplete { get; set; }
        public bool? Result { get; set; }
        public string PassOrFail { get; set; }
        public DateTime? TrainingCompleteDate { get; set; }
        public DateTime? ExamCompleteDate { get; set; }
    }

    public class PosExamStartBase
    {
        public bool Start { get; set; }
        public bool Automatically { get; set; }
        public double hours { get; set; }
        public bool TrainingComplete { get; set; }
        public bool Result { get; set; }
        public string PassOrFail { get; set; }
    }
}
