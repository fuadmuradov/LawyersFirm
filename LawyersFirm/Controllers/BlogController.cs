using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class BlogController : Controller
    {
        private readonly MyContext db;
        private readonly MyContext db2;

        public BlogController(MyContext db, MyContext db2)
        {
            this.db = db;
            this.db2 = db2;
        }

        public async Task<IActionResult> AllBlog()
        {
            List<Blog> blogs = await db.Blogs.Include(w => w.BlogWriter).Include(k=>k.Practice).ToListAsync();
            return View(blogs);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) id = db2.Blogs.First().Id;
                Blog blog = db2.Blogs.Include(w=>w.BlogWriter).Include(p=>p.Practice).FirstOrDefault(k=>k.Id==id);
            if (blog == null) return NotFound();
            ViewBag.Blogs = db2.Blogs.OrderByDescending(d => d.Date).Include(w => w.BlogWriter).Include(k => k.Practice).ToList();
            return View(blog);
        }
    }
}
