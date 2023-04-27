﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionAuth.Model.Models
{
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string? Name { get; set; }
    }
}
