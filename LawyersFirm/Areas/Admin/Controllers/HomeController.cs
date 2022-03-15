using LawyersFirm.Models;
using Microsoft.AspNetCore.Mvc;
using LawyersFirm.Models.DbTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LawyersFirm.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace LawyersFirm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public HomeController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Slider()
        {
            LawyersFirm.Models.DbTables.Slider slider = db.Sliders.Include(s=>s.SliderImages).First();
            return View(slider);
        }


        //Slider Content Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Slider(string Content)
        {
                Slider slider = db.Sliders.First();
            if(string.IsNullOrEmpty(Content))
            {
                return LocalRedirect("/Admin/Home/Slider");
            }

            slider.Title = Content;
            db.SaveChanges();
            return LocalRedirect("/Admin/Home/Slider");           
        }

      



        public async Task<IActionResult> SliderCreate()
        {
            return View();
        }


        //Slider Image Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SliderCreate(SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!sliderImage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image Format does not Right");
                return View(sliderImage);
            }

            string folder = @"assets\images\home\";
            sliderImage.Image = sliderImage.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            sliderImage.SliderId = db.Sliders.First().Id;
            await db.SliderImages.AddAsync(sliderImage);
            await db.SaveChangesAsync();


            return LocalRedirect("/Admin/Home/Slider");
        }


        //SLider Image Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Slider));

            SliderImage slider = await db.SliderImages.FirstOrDefaultAsync(f => f.Id == id);

            if (slider == null) return RedirectToAction(nameof(Slider));
            string folder = @"assets\images\home\";
            FileExtension.Delete(webHost.WebRootPath, folder, slider.Image);
            db.SliderImages.Remove(slider);

                db.SaveChanges();

            return LocalRedirect("/Admin/Home/Slider");
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            SliderImage slider = await db.SliderImages.FirstOrDefaultAsync(f => f.Id == id);

            if (slider == null) return NotFound();

            return View(slider);

        }


    }
}
