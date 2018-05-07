using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityProject.Models;
using Microsoft.AspNetCore.Http;

namespace EntityProject.Controllers
{
    public class HomeController : Controller
    {
        private EntityProjectContext _context;

        public HomeController(EntityProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ModelBundle Bundle = new ModelBundle { model1 = new RegistrationViewModel(), model2 = new LoginViewModel() };
            return View("Index", Bundle);
        }

        [HttpGet]
        [Route("registered")]
        public IActionResult Registered()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegistrationViewModel model1)
        {
            if (ModelState.IsValid)
            {
                User NewUser = new User
                {
                    FirstName = model1.FirstName,
                    LastName = model1.LastName,
                    Email = model1.Email,
                    Password = model1.Password
                };
                _context.users.Add(NewUser);
                _context.SaveChanges();
                User last = _context.users.Last();
                int id = last.UserId;
                HttpContext.Session.SetInt32("id", id);
                return RedirectToAction("Index", "Dashboard");
            }
            ModelBundle Bundle = new ModelBundle { model1 = model1, model2 = new LoginViewModel() };
            return View("Index", Bundle);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel model2)
        {
            if (ModelState.IsValid)
            {
                List<User> user = _context.users.Where(u => u.Email == model2.EmailLog).ToList(); // can use SingleDefault instead of Where
                if (user.Count > 0)
                {
                    if (user[0].Password == model2.PasswordLog)
                    {
                        int id = user[0].UserId;
                        HttpContext.Session.SetInt32("id", id);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    ViewBag.passwordError = "Password is wrong";
                }
                else
                {
                    ViewBag.emailError = "Email doesn't exist";
                }
            }
            ModelBundle Bundle = new ModelBundle { model1 = new RegistrationViewModel(), model2 = model2 };
            return View("Index", Bundle);
        }
    }
}
