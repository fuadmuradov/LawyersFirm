using LawyersFirm.Extensions;
using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
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
    public class CaseController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public CaseController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }


        public IActionResult Index()
        {
            List<Case> cases = db.Cases.Include(i => i.Category).ToList();
            return View(cases);
        }

        public IActionResult CaseCreate()
        {
            ViewBag.Category = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CaseCreate(Case ccase)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category category = db.Categories.FirstOrDefault(k => k.Id == ccase.CategoryId);
            if (category == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            string folder = @"assets\images\cases\";
            ccase.Image = ccase.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            db.Cases.Add(ccase);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Case/Index");
        }


        public IActionResult CaseEdit(int? id)
        {
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            Case ccase = db.Cases.FirstOrDefault(i => i.Id == id);
            if (ccase == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            ViewBag.Category = db.Categories.ToList();
            return View(ccase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Caseedit(Case ccase)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Case dbcase = db.Cases.FirstOrDefaultAsync(i => i.Id == ccase.Id).Result;
            if (dbcase == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            if (!db.Categories.Any(i => i.Id == ccase.CategoryId)) return LocalRedirect("/admin/statuserror/notfoundpage");
           
            if (ccase.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\cases\";
                    string newImg = await ccase.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbcase.Image);
                    dbcase.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }

            dbcase.Title = ccase.Title;
            dbcase.Description = ccase.Description;
            dbcase.Challenge = ccase.Challenge;
            dbcase.Solution = ccase.Solution;
            dbcase.Result = ccase.Result;
            dbcase.Time = ccase.Time;
            dbcase.LawherFullname = ccase.LawherFullname;
            dbcase.Price = ccase.Price;
            dbcase.CategoryId = ccase.CategoryId;

            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/Case/Index");
        }

        public IActionResult CaseDelete(int? id)
        {
            if (id == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            Case ccase = db.Cases.FirstOrDefault(i => i.Id == id);
            if (ccase == null) return LocalRedirect("/admin/statuserror/notfoundpage");
            string folder = @"assets\images\cases\";
            FileExtension.Delete(webHost.WebRootPath, folder, ccase.Image);
            db.Cases.Remove(ccase);
            db.SaveChanges();
            return LocalRedirect("/Admin/Case/Index");
        }

    }
}
