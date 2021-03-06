using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using LawyersFirm.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext db;

        public HomeController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Slider = db.Sliders.Include(i => i.SliderImages.OrderByDescending(f => f.Order)).First(),
                FirmInfo = db.FirmInfos.Include(i => i.InfoDescs).Include(f => f.OfficeImages).First(),
                Attorneys = db.Attorneys.Include(k => k.AttorneyContacts).ToList(),
                Advantage = db.Advantages.Include(a => a.AdvantageDescs).First(),
                Testimonials = db.Testimonials.ToList(),
                Practices = db.Practices.ToList(),
                Blogs = db.Blogs.Include(w => w.BlogWriter).Include(k => k.Practice).ToList(),
                Setting = db.Settings.First()
            };
            return View(home);
        }


        [HttpPost]
        public async Task<IActionResult> SendMail(string name, string email, string subject, string message)
        {
            Notification notification = new Notification()
            {
                Fullname = name,
                Email = email,
                Subject = subject,
                Message = message
            };

            db.Notifications.Add(notification);
            await db.SaveChangesAsync();

            return LocalRedirect("/Home/Index");
        }

       
        
    }
}
