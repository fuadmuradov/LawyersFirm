﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class AdvantageDesc
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int AdvantageId { get; set; }
        public Advantage Advantage { get; set; }

    }
}
