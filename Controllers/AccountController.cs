using CRUD2.Data;
using CRUD2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.users.FirstOrDefault(u => u.username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.passwordhash))
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string username, string password, string email)
        {
            var user = new User
            {
                username = username,
                passwordhash = BCrypt.Net.BCrypt.HashPassword(password),
                email = email
            };
            _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}
