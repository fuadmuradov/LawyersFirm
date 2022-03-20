using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class ContactController : Controller
    {
        private readonly MyContext db;

        public ContactController(MyContext db)
        {
            this.db = db;
        }
        public IActionResult Contactt()
        {
            Setting setting = db.Settings.FirstOrDefault();
            if (setting == null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string name, string email, string subject, string message)
        {
            Notification notification = new Notification()
            {
                Fullname = name,
                Email = email,
                Subject = subject,
                Message = message
            };

            db.Notifications.Add(notification);
            await db.SaveChangesAsync();

            return LocalRedirect("/Contact/Contactt");
        }
    }
}
