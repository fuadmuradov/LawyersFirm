using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class FAQ
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:200)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength:1500)]
        public string Description { get; set; }

        public List<FaqImage> FaqImages { get; set; }
        public List<FaqQuestion> FaqQuestions { get; set; }
    }
}
