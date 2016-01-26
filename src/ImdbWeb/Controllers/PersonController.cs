using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ImdbDAL;
using ImdbWeb.ViewModels.PersonViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
	public class PersonController : Controller
	{
		[FromServices]
		public ImdbContext Db { get; set; }

		public IActionResult Actors()
		{
			var persons = from person in Db.Persons
						  where person.ActedMovies.Any()
						  select person;

			ViewData.Model = new IndexViewModel
			{
				Title = "Actors",
				Persons = persons
			};
			return View("Index");
		}

		public IActionResult Producers()
		{
			var persons = Db.Persons.Where(person => person.ProducedMovies.Any());

			ViewData.Model = new IndexViewModel
			{
				Title = "Producers",
				Persons = persons
			};
			return View("Index");
		}

		public IActionResult Directors()
		{
			var persons = from person in Db.Persons
						  where person.DirectedMovies.Any()
						  select person;

			ViewData.Model = new IndexViewModel
			{
				Title = "Directors",
				Persons = persons
			};
			return View("Index");
		}

		[Route("Person/{id:int}")]
		public IActionResult Details(int id)
		{
			ViewData.Model = Db.Persons.Find(id);
			return View();
		}
	}
}
