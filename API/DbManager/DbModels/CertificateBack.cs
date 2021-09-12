﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.DbManager.DbModels
{
    [Table("CertificateBack")]
    public class CertificateBack
    {
        [Key]
        public int ID { get; set; }
        public string HTMLData { get; set; }
    }
   
}
