using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class StudyController : Controller
    {
        public IActionResult AllStudy()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
