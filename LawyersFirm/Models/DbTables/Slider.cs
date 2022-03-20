using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:400)]
        public string Title { get; set; }
        public List<SliderImage> SliderImages { get; set; }
    }
}
