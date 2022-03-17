using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Practice
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Explonation { get; set; }

        public string Image { get; set; }
       
        [NotMapped]
        public IFormFile Photo { get; set; }

        [Required]
        public string Icon { get; set; }
        public List<PriceToPractice> PriceToPractices { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
