using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class PracticeController : Controller
    {
        public IActionResult AllPractices()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
