using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace e_Project.Controllers
{
    public class AccountController : Controller
    {

        private readonly DB _context;

        public AccountController(DB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Login()
        { 
            string cookie = Request.Cookies["loginCookie"];
            if(cookie != null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("loginCookie");
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashed;
            _context.Add(user);
            _context.SaveChanges();
            ViewBag.message = "Registration is successful";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            //Login Validation
            var usr = _context.user_accounts.Where(u => u.Email == user.Email).FirstOrDefault();
            if (usr != null)
            {
                // Password hashing yet to be implemented
                if(BCrypt.Net.BCrypt.Verify(user.Password, usr.Password))
                {
                    createCookie("loginCookie", usr.Email, 30);
                    ViewBag.message = "The user " + user.Email + " is logged now.";
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                ViewBag.message = "Invalid password!";
                return View();
            }         
            ViewBag.message = "Invalid email!";
            return View();
        }

        private void createCookie(string key, string name, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies.Append(key, name, option);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string email)
        {
            var usr = _context.user_accounts.Where(u => u.Email == email).FirstOrDefault();
            if (usr != null)
            {
                return Json($"Email {email} is already in use.");
            }

            return Json(true);
        }

    }
}
