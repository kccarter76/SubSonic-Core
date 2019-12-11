﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SubSonic.Extensions.Test.Models
{
    [Table(nameof(Status))]
    public class Status
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
