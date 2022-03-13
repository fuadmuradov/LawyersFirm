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
    public class PracticeController : Controller
    {
        private readonly MyContext db;

        public PracticeController(MyContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> AllPractices()
        {
            List<Practice> practice= await db.Practices.ToListAsync();

            return View(practice);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) id = db.Practices.First().Id;
            Practice practice = await db.Practices.FirstOrDefaultAsync(i => i.Id == id);
            ViewBag.Practices = await db.Practices.ToListAsync();
            return View(practice);
        }
    }
}
