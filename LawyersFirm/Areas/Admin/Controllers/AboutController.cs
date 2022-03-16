using LawyersFirm.Extensions;
using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public AboutController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }

        #region ABOUT US SECTION

        public IActionResult AboutUs()
        {
            About about = db.Abouts.Include(a=>a.Subjects).First();
            return View(about);       
        }

        //Update Abouts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> AboutUs(About about)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            About about1 = db.Abouts.First() ;

            if (about.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\home\";
                    string newImg = await about.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, about1.Image);
                    about1.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }
            about1.Title = about.Title;
            about1.Description = about.Description;
            about1.Writer = about.Writer;


            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/About/AboutUs");
        }


        public IActionResult SubjectCreate()
        {
            Subject subject = new Subject(); 
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubjectCreate(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            subject.AboutId = db.Abouts.First().Id;

            db.Subjects.Add(subject);
            db.SaveChanges();

           return LocalRedirect("/Admin/About/AboutUs");
        }


        //Delete Subjects
        public IActionResult SubjectDelete(int? id)
        {
            if (id == null) return NotFound();
            Subject subject = db.Subjects.FirstOrDefault(a=>a.Id==id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return LocalRedirect("/Admin/About/AboutUs");
        }


        #endregion

        #region Pricing Section
        public IActionResult Pricing()
        {
            PriceContent  content = db.PriceContents.Include(a => a.Prices).First();
            ViewBag.PriceToPractice = db.PriceToPractices.Include(x=>x.Practice).ToList();
            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pricing(PriceContent content)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            PriceContent priceContent = db.PriceContents.First();

            if (content.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\home\";
                    string newImg = await content.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, priceContent.Image);
                    priceContent.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }
            priceContent.Title = content.Title;
            priceContent.Description = content.Description;
            priceContent.Writer = content.Writer;


            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/About/Pricing"); 
        }

        public IActionResult PriceCreate()
        {
            ViewBag.Practice = db.Practices.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PriceCreate(Price price)
        {
            ViewBag.Practice = db.Practices.ToList();
            if (!ModelState.IsValid) return View();


            price.PriceToPractices = new List<PriceToPractice>();
            foreach (var id in price.PracticeIds)
            {
                PriceToPractice practice = new PriceToPractice
                {
                    Price = price,
                    PracticeId = id
                };
                price.PriceToPractices.Add(practice);
            }
            price.PriceContentId = db.PriceContents.First().Id;
            db.Prices.Add(price);
            db.SaveChanges();
            return LocalRedirect("/Admin/About/Pricing");
        }

        public async Task<IActionResult> PriceDelete(int? id)
        {
            if (id == null) return NotFound();

            Price price = await db.Prices.FirstOrDefaultAsync(i=>i.Id==id);
            if (price == null) return NotFound();
            List<PriceToPractice> priceToPractices = await db.PriceToPractices.Where(k => k.PriceId == price.Id).ToListAsync();
          
            db.PriceToPractices.RemoveRange(priceToPractices);
            db.Prices.Remove(price);
            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/About/Pricing");
        }
        #endregion


    }
}
