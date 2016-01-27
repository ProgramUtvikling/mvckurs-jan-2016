using System;
using System.Data.Entity;
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

		public async Task<IActionResult> Actors()
		{
			var persons = from person in Db.Persons
						  where person.ActedMovies.Any()
						  select person;

			ViewData.Model = new IndexViewModel
			{
				Title = "Actors",
				Persons = await persons.ToListAsync()
			};
			return View("Index");
		}

		public async Task<IActionResult> Producers()
		{
			var persons = await Db.Persons.Where(person => person.ProducedMovies.Any()).ToListAsync();

			ViewData.Model = new IndexViewModel
			{
				Title = "Producers",
				Persons = persons
			};
			return View("Index");
		}

		public async Task<IActionResult> Directors()
		{
			var persons = await (from person in Db.Persons
								 where person.DirectedMovies.Any()
								 select person).ToListAsync();

			ViewData.Model = new IndexViewModel
			{
				Title = "Directors",
				Persons = persons
			};
			return View("Index");
		}

		[Route("Person/{id:int}")]
		public async Task<IActionResult> Details(int id)
		{
			ViewData.Model = await Db.Persons.FindAsync(id);
			return View();
		}
	}
}
