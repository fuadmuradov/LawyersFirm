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
using LawyersFirm.Models.ViewModel;

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


        // SLider Tables Information and Tables
        public IActionResult Slider()
        {
            LawyersFirm.Models.DbTables.Slider slider = db.Sliders.Include(s => s.SliderImages).First();
            return View(slider);
        }

        #region SLIDER SECTION

        //Slider Content Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Slider(string Content)
        {
            Slider slider = db.Sliders.First();
            if (string.IsNullOrEmpty(Content))
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


        //SliderImage Update
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            SliderImage sliderImage = await db.SliderImages.FirstOrDefaultAsync(f => f.Id == id);

            if (sliderImage == null) return NotFound();

            return View(sliderImage);

        }

        //SliderImage Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderImage slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            SliderImage sliderImage = db.SliderImages.Find(slider.Id);
            if (slider.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\home\";
                    string newImg = await slider.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, sliderImage.Image);
                    sliderImage.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }
            sliderImage.Subject = slider.Subject;
            sliderImage.Order = slider.Order;

            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/Home/Slider");

        }


        #endregion


        //InfoFirm and relation tables Information
        public IActionResult HomeInfo()
        {
            FirmInfo firm = db.FirmInfos.Include(x => x.InfoDescs).Include(i => i.OfficeImages).First();
            return View(firm);
        }

        #region FIRMINFO SECTION AND DESCRIPTION
     
        // FirmInfo Earned Updated
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HomeInfo(FirmInfo info)
        {
            FirmInfo firm = db.FirmInfos.First();
            firm.Earned = info.Earned;
            db.SaveChanges();
            return LocalRedirect("/Admin/Home/HomeInfo");
        }

        //(Get Method) InfoDesc Tables Description Column Edit
        public IActionResult InfoDescriptionEdit(int? id)
        {
            InfoDesc infoDesc = db.InfoDescs.FirstOrDefault(i => i.Id == id);

            return View(infoDesc);
        }

        //(Post Method) InfoDesc Tables Description Column Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InfoDescriptionEdit(InfoDesc info)
        {
            InfoDesc infoDesc = db.InfoDescs.FirstOrDefault(i => i.Id == info.Id);
            infoDesc.Description = info.Description;
            db.SaveChanges();

            return LocalRedirect("/Admin/Home/HomeInfo");
        }

        //Office Image Create
        public async Task<IActionResult> OfficeImageCreate()
        {
            return View();
        }


        //Office Image Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OfficeImageCreate(OfficeImageDescVM officeImageDesc)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!string.IsNullOrEmpty(officeImageDesc.Description))
            {
                InfoDesc infoDesc = new InfoDesc();
                infoDesc.Description = officeImageDesc.Description;
                infoDesc.FirmInfoId = db.FirmInfos.First().Id;
                db.InfoDescs.Add(infoDesc);
                db.SaveChanges();
                if (officeImageDesc.Photo == null)
                {
                    return LocalRedirect("/Admin/Home/HomeInfo");
                }
            }

            if (!officeImageDesc.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image Format does not Right");
                return View(officeImageDesc);
            }

            OfficeImage officeImage = new OfficeImage();
            string folder = @"assets\images\home\";
            officeImage.Image = officeImageDesc.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            officeImage.FirmInfoId = db.OfficeImages.First().Id;
            await db.OfficeImages.AddAsync(officeImage);
            await db.SaveChangesAsync();


            return LocalRedirect("/Admin/Home/HomeInfo");
        }

        public async Task<IActionResult> InfoDescriptionDelete(int? id)
        {
            if (id == null) return NotFound();
            InfoDesc infoDesc = await db.InfoDescs.FirstOrDefaultAsync(i=>i.Id==id);
            if (infoDesc == null) return NotFound();
            db.InfoDescs.Remove(infoDesc);
            db.SaveChanges();
            return LocalRedirect("/Admin/Home/HomeInfo");
        }


            //Office Image Delete
            public async Task<IActionResult> DeleteOfficeImage(int? id)
        {
            if (id == null) return NotFound();

            OfficeImage officeImage = await db.OfficeImages.FirstOrDefaultAsync(f => f.Id == id);

            if (officeImage == null) return NotFound();
            string folder = @"assets\images\home\";
            FileExtension.Delete(webHost.WebRootPath, folder, officeImage.Image);
            db.OfficeImages.Remove(officeImage);

            db.SaveChanges();

            return LocalRedirect("/Admin/Home/HomeInfo");
        }

        #endregion

        //     ADVANTAGE CRUD

        public IActionResult Advantage()
        {
            Advantage advantage = db.Advantages.Include(d => d.AdvantageDescs).First();
            
            return View(advantage);
        }


        #region ADVANTAGE SECTION
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Advantage(Advantage advantage)
        {
            if (!ModelState.IsValid)
            {
                return View(advantage);
            }
            Advantage advantage1 = db.Advantages.First();

            advantage1.Title = advantage.Title;
            advantage1.CustomerCount = advantage.CustomerCount;
            advantage1.Experience = advantage.Experience;
            advantage1.Expert = advantage.Expert;
            advantage1.Award = advantage.Award;

            db.SaveChanges();


            return LocalRedirect("/Admin/Home/Advantage");
        }

        public IActionResult AdvantageDescCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvantageDescCreate(AdvantageDesc advantageDesc)
        {
            if (!ModelState.IsValid && advantageDesc.Description==null)
            {
                return View(advantageDesc);
            }
            advantageDesc.AdvantageId = db.Advantages.First().Id;
            db.AdvantageDescs.Add(advantageDesc);

            db.SaveChanges();
            return LocalRedirect("/Admin/Home/Advantage");
        }


        public IActionResult AdvantageDescDelete(int? id)
        {
            if (id == null) return NotFound();
            AdvantageDesc advantageDesc = db.AdvantageDescs.FirstOrDefault(a=>a.Id==id);
            if (advantageDesc == null) return NotFound();
            db.AdvantageDescs.Remove(advantageDesc);
            db.SaveChanges();
            return LocalRedirect("/Admin/Home/Advantage");
        }

        #endregion

        }
}
