using LawyersFirm.Models.DbTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models.ViewModel
{
    public class HomeVM
    {
        public Slider Slider { get; set; }
        public FirmInfo FirmInfo { get; set; }
        public List<Attorney> Attorneys { get; set; }
        public Advantage Advantage { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Practice> Practices { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
