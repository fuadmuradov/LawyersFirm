using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Attorney
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Jobname { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        [StringLength(maximumLength: 3000)]
        public string Biography { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string Education { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string SummarySentence { get; set; }

        public List<AttorneyAward> AttorneyAwards { get; set; }

        public List<AttorneyPractice> AttorneyPractices { get; set; }

        public List<AttorneyContact> AttorneyContacts { get; set; }
    }
}
