using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AuctionCore.Models.User;
using AuctionCore.Data.Services;
using AuctionCore.Models.Session;
using AuctionCore.Models.Category;

namespace AuctionCore.Controllers
{
	[Route("/api")]
	[ApiController]
	public class ApiController : ControllerBase
	{
		private readonly IUserService _users;

		private readonly ISessionService _sessions;

		private readonly ICategoryService _categories;

		public ApiController(
			IUserService users, ISessionService sessions, ICategoryService categories)
		{
			_users = users;
			_sessions = sessions;
			_categories = categories;
		}

		[HttpGet]
		[Route("users/all")]
		public IEnumerable<User> GetAllUsers() =>
			_users.GetAll();

		[HttpGet]
		[Route("sessions/get")]
		public string GetSession()
		{
			if (_sessions.Exists(HttpContext, "session:id", out Session session))
			{
				return session.ToJSON();
			}

			return null;
		}
	}
}
