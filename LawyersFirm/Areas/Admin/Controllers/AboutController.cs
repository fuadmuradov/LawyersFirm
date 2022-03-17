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
            About about = db.Abouts.Include(a => a.Subjects).First();
            return View(about);
        }

        //Update Abouts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AboutUs(About about)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            About about1 = db.Abouts.First();

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
            Subject subject = db.Subjects.FirstOrDefault(a => a.Id == id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return LocalRedirect("/Admin/About/AboutUs");
        }


        #endregion

        #region Pricing Section
        public IActionResult Pricing()
        {
            PriceContent content = db.PriceContents.Include(a => a.Prices).First();
            ViewBag.PriceToPractice = db.PriceToPractices.Include(x => x.Practice).ToList();
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

        //Price Create
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

        //Price Delete 
        public async Task<IActionResult> PriceDelete(int? id)
        {
            if (id == null) return NotFound();

            Price price = await db.Prices.FirstOrDefaultAsync(i => i.Id == id);
            if (price == null) return NotFound();
            List<PriceToPractice> priceToPractices = await db.PriceToPractices.Where(k => k.PriceId == price.Id).ToListAsync();

            db.PriceToPractices.RemoveRange(priceToPractices);
            db.Prices.Remove(price);
            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/About/Pricing");
        }


        public IActionResult PriceEdit(int? id)
        {
            ViewBag.Practices = db.Practices.ToList();
            ViewBag.PriceToPractices = db.PriceToPractices.Where(k => k.PriceId == id).ToList();

            Price price = db.Prices.FirstOrDefault(i => i.Id == id);
            return View(price);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PriceEdit(Price price)
        {
            if (!ModelState.IsValid)
            {
                return View(price);
            }

            List<PriceToPractice> teacherHobbies = await db.PriceToPractices.Where(t => t.PriceId == price.Id).ToListAsync();
            db.PriceToPractices.RemoveRange(teacherHobbies);
            price.PriceToPractices = new List<PriceToPractice>();
            foreach (var id in price.PracticeIds)
            {
                PriceToPractice ppracitce = new PriceToPractice
                {
                    PriceId = price.Id,
                    PracticeId = id
                };
                db.PriceToPractices.Add(ppracitce);
            }
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/About/Pricing");
        }
        #endregion


        #region FAQ

        public IActionResult FaqPage()
        {
            FAQ faq = db.FAQs.Include(i => i.FaqImages).Include(k => k.FaqQuestions).First();

            return View(faq);
        }

        //FAQ EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FaqPage(FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return View(faq);
            }

            FAQ faq1 = await db.FAQs.Include(i => i.FaqImages).Include(k => k.FaqQuestions).FirstAsync();
            faq1.Title = faq.Title;
            faq1.Description = faq.Description;
            await db.SaveChangesAsync();
            return View(faq1);
        }

        //*********************************************************************************
        //FAQ Question Create
        public IActionResult FaqCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FaqCreate(FaqQuestion faqQuestion)
        {
            if (!ModelState.IsValid)
            {
                return View(faqQuestion);
            }

            faqQuestion.FAQId = db.FAQs.First().Id;
            db.FaqQuestions.Add(faqQuestion);
            db.SaveChanges();

            return LocalRedirect("/Admin/About/FaqPage");
        }

        //Faq Question Edit
        public IActionResult FaqEdit(int? id)
        {
            FaqQuestion question = db.FaqQuestions.FirstOrDefault(i => i.Id == id);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FaqEdit(FaqQuestion faqQuestion)
        {
            if (!ModelState.IsValid)
            {
                return View(faqQuestion);
            }


            FaqQuestion question = await db.FaqQuestions.FirstOrDefaultAsync(i => i.Id == faqQuestion.Id);
            if (question == null) return NotFound();

            question.Question = faqQuestion.Question;
            question.Answer = faqQuestion.Answer;

            db.SaveChanges();

            return LocalRedirect("/Admin/About/FaqPage");
        }

        // Faq Question Delete
        public IActionResult FaqDelete(int? id)
        {
            FaqQuestion question = db.FaqQuestions.FirstOrDefault(k => k.Id == id);
            if (question == null) return NotFound();
            db.FaqQuestions.Remove(question);
            db.SaveChanges();

            return LocalRedirect("/Admin/About/FaqPage");
        }

        //***********************************************************************************

        public IActionResult FaqImageCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FaqImageCreate(FaqImage faqimage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!faqimage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image Format does not Right");
                return View(faqimage);
            }

            string folder = @"assets\images\cases\";
            faqimage.Image = faqimage.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            faqimage.FAQId = db.FAQs.First().Id;
            await db.FaqImages.AddAsync(faqimage);
            await db.SaveChangesAsync();


            return LocalRedirect("/Admin/About/FaqPage");
        }


        //Faq Images Edit
        public async Task<IActionResult> FaqImageEdit(int? id)
        {
            if (!ModelState.IsValid) return NotFound();

            FaqImage faqImage = await db.FaqImages.FirstOrDefaultAsync(f => f.Id == id);

            if (faqImage == null) return NotFound();

            return View(faqImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FaqImageEdit(FaqImage faqImage)
        {
            if (!ModelState.IsValid)
            {
                return View(faqImage);
            }

            FaqImage faqImage1 = db.FaqImages.Find(faqImage.Id);
            if (faqImage.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\cases\";
                    string newImg = await faqImage.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, faqImage1.Image);
                    faqImage1.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }

            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/About/FaqPage");
        }


        // Faq Image Delete
        public async Task<IActionResult> FaqImageDelete(int? id)
        {
            if (id == null) return NotFound();
            FaqImage faqImage = await db.FaqImages.FirstOrDefaultAsync(k => k.Id == id);

            if (faqImage == null) return NotFound();

            string folder = @"assets\images\cases\";
            FileExtension.Delete(webHost.WebRootPath, folder, faqImage.Image);

            db.FaqImages.Remove(faqImage);
            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/About/FaqPage");
        }

        #endregion


        #region Testimonial
        public IActionResult Testimonial()
        {
            List<Testimonial> testimonials = db.Testimonials.ToList();
            return View(testimonials);
        }

        public IActionResult TestimonialCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestimonialCreate(Testimonial testimonial)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!testimonial.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image Format does not Right");
                return View(testimonial);
            }

            string folder = @"assets\images\testimonials\";
            testimonial.Image = testimonial.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            await db.Testimonials.AddAsync(testimonial);
            await db.SaveChangesAsync();


            return LocalRedirect("/Admin/About/Testimonial");
        }

        public IActionResult TestimonialEdit(int? id)
        {
            if (id == null) return NotFound();
            Testimonial testimonial = db.Testimonials.FirstOrDefault(i => i.Id == id);
            if (testimonial == null) return NotFound();

            return View(testimonial);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TestimonialEdit(Testimonial testimonial)
        {
            if (!ModelState.IsValid)
            {
                return View(testimonial);
            }

            Testimonial dbtestimonial = db.Testimonials.FirstOrDefault(i => i.Id == testimonial.Id);
            if (dbtestimonial == null) return NotFound();

            if (testimonial.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\testimonials\";
                    string newImg = await testimonial.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbtestimonial.Image);
                    dbtestimonial.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }
            dbtestimonial.Fullname = testimonial.Fullname;
            dbtestimonial.Description = testimonial.Description;
            dbtestimonial.Componyname = testimonial.Componyname;

            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/About/Testimonial");
        }


        //Testimonials Delete
        public IActionResult TestimonialDelete(int? id)
        {
            if (id == null) return NotFound();
            Testimonial testimonial = db.Testimonials.FirstOrDefault(i => i.Id == id);
            if (testimonial == null) return NotFound();

            db.Testimonials.Remove(testimonial);
            db.SaveChanges();

            return LocalRedirect("/Admin/About/Testimonial");
        }

        #endregion


    }
}
