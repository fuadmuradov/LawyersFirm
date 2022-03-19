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
    public class BlogController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment webHost;

        public BlogController(MyContext db, IWebHostEnvironment webHost)
        {
            this.db = db;
            this.webHost = webHost;
        }

        #region BLOG WRITER
        public IActionResult Index()
        {
            List<BlogWriter> blogw = db.BlogWriters.ToList();
            return View(blogw);
        }

        public IActionResult BlogWriterCreate()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogWriterCreate(BlogWriter blogWriter)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            string folder = @"assets\images\";
            blogWriter.Image = blogWriter.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            db.BlogWriters.Add(blogWriter);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Blog/Index");
        }

        public IActionResult BlogWriterEdit(int? id)
        {
            if (id == null) return NotFound();
            BlogWriter dbWriter = db.BlogWriters.FirstOrDefault(i => i.Id == id);
            if (dbWriter == null) return NotFound();
            return View(dbWriter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogWriterEdit(BlogWriter blogWriter)
        {
             if (!ModelState.IsValid)
            {
                return View();
            }
            BlogWriter dbwriter = db.BlogWriters.FirstOrDefaultAsync(i => i.Id == blogWriter.Id).Result;
            if (dbwriter == null) return NotFound();

            if (blogWriter.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\";
                    string newImg = await blogWriter.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbwriter.Image);
                    dbwriter.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }

            dbwriter.Fullname = blogWriter.Fullname;
            dbwriter.Description = blogWriter.Description;
           

            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/Blog/Index");
        }

        public IActionResult BlogWriterDelete(int? id)
        {
            if (id == null) return NotFound();
            BlogWriter blogWriter = db.BlogWriters.FirstOrDefault(i => i.Id == id);
            if (blogWriter == null) return NotFound();
            List<Blog> blogs = db.Blogs.Where(k => k.BlogWriterId == id).ToList();
            foreach (var item in blogs)
            {
                string folderr = @"assets\images\blog";
                FileExtension.Delete(webHost.WebRootPath, folderr, item.Image);
            }
            db.Blogs.RemoveRange(blogs);
            string folder = @"assets\images\";
            FileExtension.Delete(webHost.WebRootPath, folder, blogWriter.Image);
            db.BlogWriters.Remove(blogWriter);
            db.SaveChanges();
            return LocalRedirect("/Admin/Blog/Index");
        }


        #endregion




        #region WRITER BLOGS
        public IActionResult WriterBlogs(int? id)
        {
            if (id == null) return NotFound();
            List<Blog> blogs = db.Blogs.Include(k=>k.BlogWriter).Include(l=>l.Practice).Where(i => i.BlogWriterId == id).ToList();

            return View(blogs);
        }


        public IActionResult BlogCreate()
        {
          
            ViewBag.Practice = db.Practices.ToList();
            ViewBag.BlogWriter = db.BlogWriters.ToList();


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogCreate(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            BlogWriter blogWriter = db.BlogWriters.FirstOrDefault(k => k.Id == blog.BlogWriterId);
            if (blogWriter == null) return NotFound();
            Practice practice = db.Practices.FirstOrDefault(k => k.Id == blog.PracticeId);
            if (practice == null) return NotFound();
            string folder = @"assets\images/blog\";
            blog.Image = blog.Photo.SavaAsync(webHost.WebRootPath, folder).Result;
            db.Blogs.Add(blog);
            await db.SaveChangesAsync();

            return LocalRedirect("/Admin/Blog/Index");
        }




        public IActionResult BlogEdit(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = db.Blogs.FirstOrDefault(i => i.Id == id);
            if (blog == null) return NotFound();
            ViewBag.Practice = db.Practices.ToList();
            ViewBag.BlogWriter = db.BlogWriters.ToList();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogEdit(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Blog dbblog = db.Blogs.FirstOrDefaultAsync(i => i.Id == blog.Id).Result;
            if (dbblog == null) return NotFound();

            if (blog.Photo != null)
            {
                try
                {
                    string folder = @"assets\images\blog\";
                    string newImg = await blog.Photo.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, dbblog.Image);
                    dbblog.Image = newImg;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }

            }

            dbblog.SummaryTitle = blog.SummaryTitle;
            dbblog.SummaryDesc = blog.SummaryDesc;
            dbblog.HeaderTitle = blog.HeaderTitle;
            dbblog.Description = blog.Description;
            dbblog.Date = blog.Date;
            dbblog.BlogWriterId = blog.BlogWriterId;
            dbblog.PracticeId = blog.PracticeId;
          
            await db.SaveChangesAsync();
            return LocalRedirect("/Admin/Blog/Index");
        }

        public IActionResult BlogDelete(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = db.Blogs.FirstOrDefault(i => i.Id == id);
            if (blog == null) return NotFound();
            string folder = @"assets\images\blog\";
            FileExtension.Delete(webHost.WebRootPath, folder, blog.Image);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return LocalRedirect("/Admin/Blog/Index");
        }





        #endregion
    }
}
