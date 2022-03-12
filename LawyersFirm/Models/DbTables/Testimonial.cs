using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Testimonial
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength:400)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Componyname { get; set; }
    }
}
