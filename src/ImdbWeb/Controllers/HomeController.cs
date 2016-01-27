using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Security.Application;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Demo()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Demo(MyPerson model)
		{
			if (ModelState.IsValid)
			{
				model.Bio = Sanitizer.GetSafeHtml(model.Bio);
				ViewData.Model = model;
				return View("DemoResult");
			}

			ModelState.AddModelError("", "This is not connected to a field!");

			return View();
		}
	}

	public class MyPerson
	{
		[Required]
		[Display(Name ="First name")]
		public string Firstname { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 1)]
		[Display(Name = "Last name")]
		public string Lastname { get; set; }

		[Display(Name = "Your age")]
		[Range(0, 200)]
		public int Age { get; set; }

		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name ="Do you want to register?")]
		public bool IsRegistered { get; set; }

		[DataType(DataType.MultilineText)]
		public string Bio { get; set; }

	}
}
