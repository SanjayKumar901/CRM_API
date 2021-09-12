using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("Fuels")]
    public class Fuels
    {
        [Key]
        public int FuelID { get; set; }
        public string FuelName { get; set; }
        public bool IsActive { get; set; }
    }
}
