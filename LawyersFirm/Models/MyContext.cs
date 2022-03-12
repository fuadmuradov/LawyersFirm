using LawyersFirm.Models.DbTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Models
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<FirmInfo> FirmInfos { get; set; }
        public DbSet<InfoDesc> InfoDescs { get; set; }
        public DbSet<OfficeImage> OfficeImages { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<FaqImage> FaqImages { get; set; }
        public DbSet<FaqQuestion> FaqQuestions { get; set; }
        public DbSet<Advantage> Advantages { get; set; }
        public DbSet<AdvantageDesc> AdvantageDescs { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<Attorney> Attorneys { get; set; }
        public DbSet<AttorneyAward> AttorneyAwards { get; set; }
        public DbSet<AttorneyPractice> AttorneyPractices { get; set; }
        public DbSet<AttorneyContact> AttorneyContacts { get; set; }
    }
}
