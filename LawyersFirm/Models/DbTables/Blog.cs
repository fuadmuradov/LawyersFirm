using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:120)]
        public string SummaryTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 800)]
        public string SummaryDesc { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string HeaderTitle { get; set; }
        [Required]
        [StringLength(maximumLength: 800)]
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int BlogWriterId { get; set; }
        public BlogWriter BlogWriter { get; set; }
        [Required]
        public int PracticeId { get; set; }
        public Practice Practice { get; set; }


    }
}
