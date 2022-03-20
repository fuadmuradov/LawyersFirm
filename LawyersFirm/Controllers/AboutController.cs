using LawyersFirm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawyersFirm.Models.DbTables;
using Microsoft.EntityFrameworkCore;

namespace LawyersFirm.Controllers
{
    public class AboutController : Controller
    {
        private readonly MyContext db;

        public AboutController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult About()
        {
         
            return View();
        }
        //Okay
        public IActionResult Testimonial()
        {
            List<LawyersFirm.Models.DbTables.Testimonial> testimonials = db.Testimonials.ToList();
            return View(testimonials);
        }

        public async Task<IActionResult> Pricing()
        {
            PriceContent priceContent = db.PriceContents.Include(i=>i.Prices).First();
            ViewBag.PriceToPractices = await db.PriceToPractices.Include(i=>i.Practice).ToListAsync();
            ViewBag.Prices = await db.Prices.ToListAsync();
            return View(priceContent);
        }

        public IActionResult Faq()
        {
            FAQ faq = db.FAQs.Include(f => f.FaqQuestions).Include(f => f.FaqImages).First();
            if (faq==null)
            {
                NotFound();
            }
            return View(faq);
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

            return LocalRedirect("/About/Faq");
        }

    }
}
