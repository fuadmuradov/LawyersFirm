using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatusErrorController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return View();
        }

        public IActionResult ServerErrorPage()
        {

            return View();
        }
    }
}
