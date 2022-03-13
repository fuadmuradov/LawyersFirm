using LawyersFirm.Models;
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
                Testimonials = db.Testimonials.ToList()
            };
            return View(home);
        }

       
        
    }
}
