using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pks4Core.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Pks4Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Pks4Context db;
        public HomeController(ILogger<HomeController> logger, Pks4Context context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult login() { 
            return View();
        }
        [HttpGet]
        public IActionResult register() {
            return View();
        }

        [HttpPost]
        public IActionResult login_process(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var is_in_base = (from users in db.Users
                                  where model.login == users.Login && model.password == users.Password
                                  select users).FirstOrDefault();
                if (is_in_base is null)
                {
                    ViewBag.Error = "Ошибка";
                    return View("login");
                }
                else
                {
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Append("Login", is_in_base.Login, cookieOptions);
                    Response.Cookies.Append("Password", is_in_base.Password, cookieOptions);

                    return RedirectToAction("messages", "Authorized");

                }
            }
            else { 
                return View("login");
            }
        }
        [HttpPost]
        public IActionResult register_process(RegisterModel registerModel) {
            if (ModelState.IsValid)
            {
                User results = new User();
                results.FirstName = registerModel.first_name;
                results.SecondName = registerModel.second_name;
                results.ThirdName = registerModel.third_name;
                results.Login = registerModel.login;
                results.Password = registerModel.password;
                db.Users.Add(results);
                db.SaveChanges();
                Console.WriteLine("sucess");
                
                return View("Index");
            }
            else
            {
                return View("register");
            }
        }

        [HttpPost]
        public IActionResult to_register() {
            return View("register");
        }
        [HttpPost]
        public IActionResult to_login() {
            return View("login");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
