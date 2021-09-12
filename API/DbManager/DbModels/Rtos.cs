using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Rtos")]
    public class Rtos
    {
        [Key]
        public int RTOID { get; set; }
        public string RTOName { get; set; }
        public string RTOCode { get; set; }
        public bool IsActive { get; set; }
    }
}
