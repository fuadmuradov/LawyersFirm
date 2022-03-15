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
    public class CaseController : Controller
    {
        private readonly MyContext db;

        public CaseController(MyContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> AllCase()
        {
            List<Case> cases = await db.Cases.Include(c => c.Category).ToListAsync();
            return View(cases);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) id = db.Cases.First().Id;
            Case cases =  db.Cases.Include(c => c.Category).FirstOrDefault(i => i.Id == id);
           
            if (cases == null) return NotFound();
            ViewBag.Cases = db.Cases.Include(c => c.Category).ToList();
            return View(cases);
        }
    }
}
