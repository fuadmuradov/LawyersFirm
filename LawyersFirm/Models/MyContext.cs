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

        public DbSet<About> Abouts { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<PriceContent> PriceContents { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<PriceToPractice> PriceToPractices { get; set; }

        public DbSet<BlogWriter> BlogWriters { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Case> Cases { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

    }
}
