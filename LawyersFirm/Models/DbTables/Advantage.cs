﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Advantage
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string CustomerCount { get; set; }
        public int Experience { get; set; }
        public int Expert { get; set; }
        public int Award { get; set; }

        public List<AdvantageDesc> AdvantageDescs { get; set; }
    }
}