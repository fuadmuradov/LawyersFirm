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

        public BlogController(MyContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> AllBlog()
        {
            List<Blog> blogs = await db.Blogs.Include(w => w.BlogWriter).Include(k=>k.Practice).ToListAsync();
            return View(blogs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) id = db.Blogs.FirstAsync().Id;
            ViewBag.Blogs = await db.Blogs.OrderByDescending(d=>d.Date).Include(w => w.BlogWriter).Include(k => k.Practice).ToListAsync();
            Blog blog = await db.Blogs.Include(w=>w.BlogWriter).Include(p=>p.Practice).FirstOrDefaultAsync(k=>k.Id==id);
            if (blog == null) return NotFound();
            return View(blog);
        }
    }
}
