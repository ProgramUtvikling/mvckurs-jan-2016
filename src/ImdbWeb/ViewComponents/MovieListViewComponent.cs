using ImdbDAL;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.ViewComponents
{
    public class MovieListViewComponent : ViewComponent
    {
		public IViewComponentResult Invoke(IEnumerable<Movie> movies, string title)
		{
			if(string.IsNullOrWhiteSpace(title) || movies.Any())
			{
				return View("Empty");
			}

			var model = new MovieListViewComponentModel {
				Movies = movies,
				Title = title
			};
			return View(model);
		}
    }
}
