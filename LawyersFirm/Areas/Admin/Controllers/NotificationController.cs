using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly MyContext db;

        public NotificationController(MyContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            List<Notification> notifications = db.Notifications.ToList();
            return View(notifications);
        }

        public IActionResult Details(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Notification notification = db.Notifications.Find(id);
            if (notification == null) return NotFound();
            return View(notification);
        }    


        public IActionResult NotificationDelete(int? id)
        {
            if (id == null) return NotFound();
            Notification notification = db.Notifications.Find(id);
            if (notification == null) return NotFound();
            db.Notifications.Remove(notification);
            db.SaveChanges();
            return LocalRedirect("/Admin/Notification/Index");
        }
    }
}
