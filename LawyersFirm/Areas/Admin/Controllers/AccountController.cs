using LawyersFirm.Models.DbTables;
using LawyersFirm.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using LawyersFirm.Models;

namespace LawyersFirm.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly MyContext db;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(MyContext db,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            
        }

        //public async Task CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await roleManager.CreateAsync(new IdentityRole("Member"));

        //}


        //public async Task CreateAdmin()
        //{
        //    AppUser user = new AppUser()
        //    {
        //        FirstName = "Fuad",
        //        LastName = "Muradov",
        //        UserName = "fuadmuradov",
        //        Email = "fuadmuradov570@gmail.com"
        //    };

        //    await userManager.CreateAsync(user, "Fuad12345");
        //    await userManager.AddToRoleAsync(user, "SuperAdmin");
        //}

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.FirstName.ToLower() + register.LastName.ToLower(),
                Email = register.Email,

            };

            AppUser user1 = await userManager.FindByEmailAsync(user.Email);
            if(user1 != null)
            {
                ModelState.AddModelError("", "This Emali Already Exist");
                return View();
            }

            AppUser user2 =await userManager.FindByNameAsync(user.UserName);
            if (user2 != null) user.UserName = user.UserName + "_";


            IdentityResult result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await userManager.AddToRoleAsync(user, "Admin");

            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerifyEmail), "Account", new { Areas="Admin", email = user.Email, token }, Request.Scheme, Request.Host.ToString());

            //string link = Url.Link("", new { Area = "Admin", email = user.Email, token }, Request.Scheme, Request.Host.ToString());
           // string link = Url.RouteUrl("/Admin/account/VerifyEmail", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hrmshrms2000@gmail.com", "LawyerFirm Confirm");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Verify Email";
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/admin/assets/template/VerifyEmail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{link}}", link);
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("hrmshrms2000@gmail.com", "hrms12345");
            smtp.Send(mail);

            TempData["Verify"] = true;

            return LocalRedirect("/Admin/Home/Slider");
        } 

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            await userManager.ConfirmEmailAsync(user, token);

            await signInManager.SignInAsync(user, true);
            ViewBag.username = user.Email;

            return LocalRedirect("/Admin/Home/Slider");
        }









        //*************************************************LOGIN*****************************************
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View();
            }
            
            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (!result.Succeeded)
           {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account has been blocked for 5 minitues due to overtrying");
                    return View();
                }
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View();
            }

            ViewBag.username = user.Email;


            return LocalRedirect("/Admin/Home/Slider");
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return LocalRedirect("/Admin/Account/Login");
        }

        //public IActionResult Show()
        //{
        //    return Content(User.Identity.Name);
        //}





        //******************************************************EDIT*********************************************
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM userEditVM = new UserEditVM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,

            };
            return View(userEditVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM userEdit)
        {
            if (!ModelState.IsValid) return View(userEdit);
            AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM userEditVM = new UserEditVM()
            {
                FirstName = userEdit.FirstName,
                LastName = userEdit.LastName,
                UserName = userEdit.UserName,
                Email = userEdit.Email,

            };

            //if (await userManager.CheckPasswordAsync(user, userEdit.CurrentPassword)) return Content("Yes");
            //else
            //    return Content("No");


            if (user.UserName != userEdit.UserName && await userManager.FindByNameAsync(userEdit.UserName) != null)
            {
                ModelState.AddModelError("", $"{userEdit.UserName} already taken");
                return View(userEditVM);
            }

            if (user.Email != userEdit.Email && await userManager.FindByEmailAsync(userEdit.Email) != null)
            {
                ModelState.AddModelError("", $"{userEdit.Email} this mail already taken");
                return View(userEditVM);
            }

            if (string.IsNullOrEmpty(userEdit.CurrentPassword) && string.IsNullOrEmpty(userEdit.Password) && string.IsNullOrEmpty(userEdit.ConfirmPassword))
            {
                user.FirstName = userEdit.FirstName;
                user.LastName = userEdit.LastName;
                user.UserName = userEdit.UserName;
                user.Email = userEdit.Email;

                await userManager.UpdateAsync(user);
                await signInManager.SignInAsync(user, true);
            }
            else
            {
                if (userEdit.CurrentPassword == null || userEdit.Password == null || userEdit.ConfirmPassword == null)
                {
                    ModelState.AddModelError("", "Fill Password Requirment");
                    return View(userEditVM);
                }

                user.FirstName = userEdit.FirstName;
                user.LastName = userEdit.LastName;
                user.UserName = userEdit.UserName;
                user.Email = userEdit.Email;
                IdentityResult result1 =  await userManager.UpdateAsync(user);
                ViewBag.username = user.Email;
                if (!result1.Succeeded)
                {
                    foreach (var error in result1.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userEditVM);
                }
               
                IdentityResult result = await userManager.ChangePasswordAsync(user, userEdit.CurrentPassword, userEdit.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userEditVM);
                }
                await signInManager.SignInAsync(user, true);
                TempData["useremail"] = user.Email;

            }

            return LocalRedirect("/Admin/Home/Slider");
        }


        public IActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(AccountVM account)
        {
            AppUser user = await userManager.FindByEmailAsync(account.User.Email);
            if (user == null) BadRequest();
            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action(nameof(ResetPassword), "Account", new {Areas="Admin", email = user.Email, token },
                Request.Scheme, Request.Host.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hrmshrms2000@gmail.com", "LawyerFirm Reset");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Verify Email";
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/admin/assets/template/ResetPassword.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{link}}", link);
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("hrmshrms2000@gmail.com", "hrms12345");
            smtp.Send(mail);

            return RedirectToAction(nameof(Login), "Account");
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            AppUser user = await userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();

            AccountVM account = new AccountVM
            {
                User = user,
                Token = token
            };

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(AccountVM account)
        {
            AppUser user = userManager.FindByEmailAsync(account.User.Email).Result;
            if (user == null) return BadRequest();

            AccountVM model = new AccountVM
            {
                User = user,
                Token = account.Token
            };
            if (!ModelState.IsValid) return View(model);
            IdentityResult result = await userManager.ResetPasswordAsync(user, account.Token, account.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }


            return RedirectToAction(nameof(Login), "Account");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AllAdmin()
        {
            List<AppUser> users = db.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await userManager.DeleteAsync(user);
            return LocalRedirect("/Admin/Account/AllAdmin");
        }

    }
}
