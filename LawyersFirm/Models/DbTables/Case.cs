using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Case
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        [StringLength(maximumLength:1000)]
        public string Challenge { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Solution { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Result { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public string LawherFullname { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
