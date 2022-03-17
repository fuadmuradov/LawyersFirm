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
    public class PracticeController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public PracticeController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }

        public IActionResult PracticePage()
        {
            List<Practice> practices = db.Practices.ToList();
            return View(practices);
        }

        public IActionResult PracticeCreate()
        {

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PracticeCreate(Practice practice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!practice.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image Format does not Right");
                return View(practice);
            }

            string folder = @"assets\images\practice\";
            practice.Image = practice.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            await db.Practices.AddAsync(practice);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/practice/practicePage");
        }

        public async Task<IActionResult> PracticeEdit(int? id)
        {
            if (id == null) return NotFound();
            Practice practice = await db.Practices.FirstOrDefaultAsync(i => i.Id == id);
            if (practice == null) return NotFound();

            return View(practice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PracticeEdit(Practice practice)
        {
            if (!ModelState.IsValid)
            {
                return View(practice);
            }

            Practice dbpractice = db.Practices.FirstOrDefault(i => i.Id == practice.Id);
            if (dbpractice == null) return NotFound();

            if (practice.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\practice\";
                    string newImg = await practice.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbpractice.Image);
                    dbpractice.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }
            dbpractice.Name = practice.Name;
            dbpractice.Description = practice.Description;
            dbpractice.Title = practice.Title;
            dbpractice.Explonation = practice.Explonation;
            dbpractice.Icon = practice.Icon;

            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Practice/PracticePage");
        }




        public IActionResult PracticeDelete(int? id)
        {
            if (id == null) return NotFound();
            Practice practice = db.Practices.FirstOrDefault(i => i.Id == id);
            if (practice == null) return NotFound();

            db.Practices.Remove(practice);
            db.SaveChanges();

            return LocalRedirect("/Admin/Practice/PracticePage");
        }

    }
}
