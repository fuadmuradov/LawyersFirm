using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Testimonial
    {
        public int Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        [StringLength(maximumLength:1500)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Componyname { get; set; }
    }
}
