using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly MyContext db;

        public HeaderViewComponent(MyContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Setting setting = db.Settings.FirstOrDefault();
         
            return View(setting);
        }

    }
}
