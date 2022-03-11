using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
         
            return View();
        }

        public IActionResult Testimonial()
        {
            return View();
        }

        public IActionResult Pricing()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }
    }
}
