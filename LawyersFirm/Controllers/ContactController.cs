﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contactt()
        {
            return View();
        }
    }
}