using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.DbTables
{
    public class SliderImage
    {
        public int Id { get; set; }
        
        public string Image { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int SliderId { get; set; }
        public Slider Slider { get; set; }
    }
}
