using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Exceptions")]
    public class Exceptions
    {
        [Key]
        public int ID { get; set; }
        public string ExceptionFunction { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
