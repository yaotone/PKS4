using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Pks4Core.Models;

namespace Pks4Core.Controllers
{
    public class AuthorizedController : Controller
    {
        private readonly ILogger<AuthorizedController> _logger;
        Pks4Context db;
        public AuthorizedController(ILogger<AuthorizedController> logger, Pks4Context context)
        {
            _logger = logger;
            db = context;
        }

		[HttpGet]
		public IActionResult messages(FIlterModel model)
		{
			
			var user = (from users in db.Users
						where users.Login == Request.Cookies["Login"]
						where users.Password == Request.Cookies["Password"]
						select users).FirstOrDefault();

			if (user == null)
			{
				return RedirectToAction("login", "Home");
			}

			var messages = (from message in db.Messages
							where message.To == user.FirstName
							select message).Where(obj => !obj.Status || model.Status != true).Select(msg => new MessageModel
							{
								Id = msg.Id,
								From = msg.From,
								MessageTitle = msg.MessageTitle,
								MessageText = msg.MessageText,
								SendingTime = msg.SendingTime,
								Status = msg.Status

							}).ToList().OrderByDescending(m => m.SendingTime);
			FIlterModel filter = new FIlterModel();
			SendMessageModel send = new SendMessageModel();

			Tuple<FIlterModel, SendMessageModel, List<MessageModel>> tuple = new Tuple<FIlterModel, SendMessageModel, List<MessageModel>>(filter, send, messages.ToList());

			return View(tuple);
		}


		[HttpPost]
		public IActionResult ReadedMarking(int id)
		{
			var message = db.Messages.Find(id);
			if (message == null)
			{
				return NotFound();
			}

			message.Status = true;
			db.Messages.Update(message);
			db.SaveChanges();

			return NoContent();
		}



		[HttpPost]
		public IActionResult messages(SendMessageModel model)
		{
			
			var user = (from usr in db.Users
						where usr.Login == Request.Cookies["Login"]
						where usr.Password == Request.Cookies["Password"]
						select usr).FirstOrDefault();
			

			if (user == null)
			{
				return RedirectToAction("Login", "Home");
			}

			var GetUserFromDB = (from users in db.Users
								where users.FirstName == model.MessageTo
								select users).FirstOrDefault();
			Console.WriteLine(GetUserFromDB.Login);
			
			

			if (GetUserFromDB == null)
			{
				ViewData["Message"] = $"Пользователь с именем: '{model.MessageTo}' не найден";
				return RedirectToAction("messages", "Authorized");
			}
			var year = DateTime.Now.Year;
			var month = DateTime.Now.Month;
			var day = DateTime.Now.Day;
			var hour = DateTime.Now.Hour;
			var minute = DateTime.Now.Minute;

			db.Messages.Add(new Message
			{
				From = user.FirstName,
				To = GetUserFromDB.FirstName,
				MessageTitle = model.MessageTitle,
				MessageText = model.MessageText,
				SendingTime = new LocalDateTime(year, month, day, hour, minute),
				Status = false

			}) ; 
			db.SaveChanges();
			Console.WriteLine("Попыталось добавить");

			ViewData["Message"] = "Отправлено";
			return RedirectToAction("messages", "Authorized");
		}

	}
}

