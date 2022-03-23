using LawyersFirm.Models;
using LawyersFirm.Models.DbTables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyersFirm.Services
{
    public class LayoutServices
    {
        private readonly MyContext db;
        private readonly UserManager<AppUser> userManager;

        public LayoutServices(MyContext db, UserManager<AppUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public List<Notification> getNotificationList()
        {
            List<Notification> notifications = db.Notifications.ToList();
            return notifications;
        }

        public async Task<string> getUser(string username)
        {
            AppUser user = await userManager.FindByNameAsync(username);
            string email = user.Email.ToString();
            return email;
        }
    }
}
