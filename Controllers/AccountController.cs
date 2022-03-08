using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models.User;
using AuctionCore.Data.Services;

namespace AuctionCore.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _users;

        public AccountController(IUserService users)
        {
            _users = users;
        }


        [HttpGet("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/Account/Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {

            }

            return View(user);
        }

        [HttpGet("/Account/Settings")]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost("/Account/Settings")]
        public IActionResult Settings(User user)
        {
            if (ModelState.IsValid)
            {
                // update user ->
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}