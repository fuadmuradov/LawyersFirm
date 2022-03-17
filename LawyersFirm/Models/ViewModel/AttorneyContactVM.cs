using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.ViewModel
{
    public class AttorneyContactVM
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Jobname { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string Biography { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string Education { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string SummarySentence { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string Phone { get; set; }
        [Required]
        public string Facebook { get; set; }
        [Required]
        public string Twitter { get; set; }
        [Required]
        public string Linkedin { get; set; }
        [Required]
        public int AttorneyId { get; set; }
    }
}
