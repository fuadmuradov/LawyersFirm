using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class AttorneyAward
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 80)]
        public string Name { get; set; }
        [Required]
        public int AttorneyId { get; set; }
        public Attorney Attorney { get; set; }
    }
}
