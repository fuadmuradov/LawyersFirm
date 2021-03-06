using LawyersFirm.Extensions;
using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using LawyersFirm.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AttorneyController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public AttorneyController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }


        public IActionResult AttorneyPage()
        {
            List<Attorney> attorneys = db.Attorneys.ToList();
            ViewBag.AttorneyContact = db.AttorneyContacts.ToList();
            ViewBag.AttorneyAward = db.AttorneyAwards.Include(a => a.Attorney).ToList();
            return View(attorneys);
        }
        

        #region Attorney and Contact Table

        public IActionResult AttorneyCreate()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttorneyCreate(AttorneyContactVM attorneyContact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!attorneyContact.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image Format does not Right");
                return View(attorneyContact);
            }
            Attorney attorney = new Attorney
            {
                Fullname = attorneyContact.Fullname,
                Jobname = attorneyContact.Jobname,
                Photo = attorneyContact.Photo,
                Biography = attorneyContact.Biography,
                Education = attorneyContact.Education,
                SummarySentence = attorneyContact.SummarySentence
            };

            string folder = @"assets\images\team\";
            attorney.Image = attorney.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            await db.Attorneys.AddAsync(attorney);
            await db.SaveChangesAsync();

            AttorneyContact contact = new AttorneyContact()
            {
                Email = attorneyContact.Email,
                Phone = attorneyContact.Phone,
                Facebook = attorneyContact.Facebook,
                Twitter = attorneyContact.Twitter,
                Linkedin = attorneyContact.Linkedin,
                Attorney = attorney
            };

            db.AttorneyContacts.Add(contact);
            db.SaveChanges();
            return LocalRedirect("/Admin/Attorney/AttorneyPage");
        }

        public async Task<IActionResult> AttorneyEdit(int? id)
        {

            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            Attorney attorney = await db.Attorneys.FirstOrDefaultAsync(i => i.Id == id);

            if (attorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyContact attorneyContact = db.AttorneyContacts.FirstOrDefault(i => i.AttorneyId == id);
            if (attorneyContact == null) return LocalRedirect("/admin/statuserror/notfoundpage");

            AttorneyContactVM attorneyContactVM = new AttorneyContactVM()
            {
                AttorneyId = attorney.Id,
                Fullname = attorney.Fullname,
                Jobname = attorney.Jobname,
                Photo = attorney.Photo,
                Biography = attorney.Biography,
                Education = attorney.Education,
                SummarySentence = attorney.SummarySentence,
                Email = attorneyContact.Email,
                Phone = attorneyContact.Phone,
                Facebook = attorneyContact.Facebook,
                Twitter = attorneyContact.Twitter,
                Linkedin = attorneyContact.Linkedin
            };

            return View(attorneyContactVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttorneyEdit(AttorneyContactVM attorneyContactVM)
        {
            if (!ModelState.IsValid)
            {
                return View(attorneyContactVM);
            }

            Attorney dbattorney = db.Attorneys.FirstOrDefault(i => i.Id == attorneyContactVM.AttorneyId);
            if (dbattorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyContact dbattorneyContact = db.AttorneyContacts.FirstOrDefault(i => i.AttorneyId == dbattorney.Id);


            if (attorneyContactVM.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\team\";
                    string newImg = await attorneyContactVM.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbattorney.Image);
                    dbattorney.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }
            dbattorney.Fullname = attorneyContactVM.Fullname;
            dbattorney.Jobname = attorneyContactVM.Jobname;
            dbattorney.Biography = attorneyContactVM.Biography;
            dbattorney.Education = attorneyContactVM.Education;
            dbattorney.SummarySentence = attorneyContactVM.SummarySentence;

            dbattorneyContact.Email = attorneyContactVM.Email;
            dbattorneyContact.Phone = attorneyContactVM.Phone;
            dbattorneyContact.Facebook = attorneyContactVM.Facebook;
            dbattorneyContact.Twitter = attorneyContactVM.Twitter;
            dbattorneyContact.Linkedin = attorneyContactVM.Linkedin;


            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Attorney/AttorneyPage");
        }

        public IActionResult AttorneyDelete(int? id)
        {
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            Attorney attorney = db.Attorneys.FirstOrDefault(i => i.Id == id);
            if (attorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyContact attorneyContact = db.AttorneyContacts.FirstOrDefault(i => i.AttorneyId == attorney.Id);
            List<AttorneyAward> attorneyAward = db.AttorneyAwards.Where(k => k.AttorneyId == attorney.Id).ToList();
            List<AttorneyPractice> attorneyPractice = db.AttorneyPractices.Where(k => k.AttorneyId == attorney.Id).ToList();
         
            db.AttorneyContacts.Remove(attorneyContact);
            db.AttorneyAwards.RemoveRange(attorneyAward);
            db.AttorneyPractices.RemoveRange(attorneyPractice);
            db.Attorneys.Remove(attorney);
            db.SaveChanges();

            return LocalRedirect("/Admin/Attorney/AttorneyPage");
        }


        #endregion



        #region AttornewAward Table Crud

        public IActionResult AttorneyAward()
        {
            List<AttorneyPractice> attorneyPractices = db.AttorneyPractices.Include(i=>i.Attorney).ToList();
            return View(attorneyPractices);
        }


        public IActionResult AttorneyAwardCreate()
        {
            ViewBag.Attorney = db.Attorneys.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttorneyAwardCreate(AttorneyAward attorneyAward)
            {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Attorney attorney = db.Attorneys.FirstOrDefault(k=>k.Id==attorneyAward.AttorneyId);
            if (attorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            db.AttorneyAwards.Add(attorneyAward);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Attorney/AttorneyPage");
        }


        public IActionResult AttorneyAwardEdit(int? id)
        {
            ViewBag.Attorney = db.Attorneys.ToList();
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyAward attorneyAward = db.AttorneyAwards.FirstOrDefault(k => k.Id == id);
            if (attorneyAward == null) return LocalRedirect("/admin/statuserror/notfoundpage");
           

            return View(attorneyAward);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttorneyAwardEdit(AttorneyAward attorneyAward)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AttorneyAward attorneyAward1 = await db.AttorneyAwards.FirstOrDefaultAsync(k => k.Id == attorneyAward.Id);
            if (attorneyAward1 == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            Attorney attorney = await db.Attorneys.FirstOrDefaultAsync(i=>i.Id==attorneyAward.AttorneyId);
            if (attorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            attorneyAward1.Name = attorneyAward.Name;
            attorneyAward1.AttorneyId = attorneyAward.AttorneyId;
             await db.SaveChangesAsync();


            return LocalRedirect("/Admin/Attorney/AttorneyPage");
        }


        public async Task<IActionResult> AttorneyAwardDelete(int? id)
        {
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyAward attorneyAward = db.AttorneyAwards.FirstOrDefault(k => k.Id == id);
            if (attorneyAward == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            db.AttorneyAwards.Remove(attorneyAward);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Attorney/AttorneyPage");
        }




        #endregion

        #region AttorneyPractice Table
        public IActionResult AttorneyPractice()
        {
            List<AttorneyPractice> practices = db.AttorneyPractices.Include(k => k.Attorney).ToList();
            return View(practices);
        }



        public IActionResult AttorneyPracticeCreate()
        {
            ViewBag.Attorney = db.Attorneys.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttorneyPracticeCreate(AttorneyPractice attorneyPractice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Attorney attorney = db.Attorneys.FirstOrDefault(k => k.Id == attorneyPractice.AttorneyId);
            if (attorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            db.AttorneyPractices.Add(attorneyPractice);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Attorney/AttorneyPractice");
        }



        public IActionResult AttorneyPracticeEdit(int? id)
        {
            ViewBag.Attorney = db.Attorneys.ToList();
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyPractice attorneyPractice = db.AttorneyPractices.FirstOrDefault(k => k.Id == id);
            if (attorneyPractice == null) return LocalRedirect("/admin/statuserror/notfoundpage");


            return View(attorneyPractice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttorneyPracticeEdit(AttorneyPractice attorneyPractice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AttorneyPractice attorneyPractice1 = await db.AttorneyPractices.FirstOrDefaultAsync(k => k.Id == attorneyPractice.Id);
            if (attorneyPractice1 == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            Attorney attorney = await db.Attorneys.FirstOrDefaultAsync(i => i.Id == attorneyPractice.AttorneyId);
            if (attorney == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            attorneyPractice1.Name = attorneyPractice.Name;
            attorneyPractice1.AttorneyId = attorneyPractice.AttorneyId;
            await db.SaveChangesAsync();


            return LocalRedirect("/Admin/Attorney/AttorneyPractice");
        }


        public async Task<IActionResult> AttorneyPracticeDelete(int? id)
        {
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            AttorneyPractice attorneyPractice = db.AttorneyPractices.FirstOrDefault(k => k.Id == id);
            if (attorneyPractice == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            db.AttorneyPractices.Remove(attorneyPractice);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Attorney/AttorneyPractice");
        }

        #endregion

    }
}
