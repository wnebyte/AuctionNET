using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models.UserModel;
using AuctionCore.Models;
using AuctionCore.BLL.Services;
using AuctionCore.Utils;

namespace AuctionCore.Controllers
{
    public class AccountController : BaseController
    {

        private readonly UserService _users;

        public AccountController() =>
            _users = new UserService();


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