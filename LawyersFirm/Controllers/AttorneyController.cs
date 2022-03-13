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
    public class AttorneyController : Controller
    {
        private readonly MyContext db;

        public AttorneyController(MyContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> AllAttorney()
        {
            List<Attorney> attorneys = await db.Attorneys.Include(a=>a.AttorneyContacts).ToListAsync();
            return View(attorneys);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) id = db.Attorneys.First().Id;
            Attorney attorney = await db.Attorneys.Include(i=>i.AttorneyAwards).Include(i=>i.AttorneyContacts).Include(i=>i.AttorneyPractices).FirstOrDefaultAsync(i => i.Id == id);



            return View(attorney);
        }
    }
}
