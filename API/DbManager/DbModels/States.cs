using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("States")]
    public class States
    {
        [Key]
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string Statename { get; set; }
        public bool IsActive { get; set; }
    }
}
