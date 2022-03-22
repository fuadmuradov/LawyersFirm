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
    public class ContactController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public ContactController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }
        public IActionResult Index()
        {
            Setting setting = db.Settings.First();
            return View(setting);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting dbsetting = db.Settings.FirstOrDefaultAsync(i => i.Id == setting.Id).Result;
            if (dbsetting == null) return LocalRedirect("/admin/statuserror/notfoundpage");

            if (setting.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\";
                    string newImg = await setting.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbsetting.Logo);
                    dbsetting.Logo = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }

            dbsetting.Phone = setting.Phone;
            dbsetting.Email = setting.Email;
            dbsetting.Address = setting.Address;
            dbsetting.Facebook = setting.Facebook;
            dbsetting.Twitter = setting.Twitter;
            dbsetting.Linkedin = setting.Linkedin;


            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/Contact/Index");
        }



    }
}
