using LawyersFirm.Models.DbTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models
{
    public class InfoDesc
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Description { get; set; }
        public int FirmInfoId { get; set; }
        public FirmInfo FirmInfo { get; set; }
    }
}
