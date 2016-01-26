using System.Collections.Generic;
using ImdbDAL;

namespace ImdbWeb.ViewComponents
{
	public class MovieListViewComponentModel
	{
		public IEnumerable<Movie> Movies { get; set; }
		public string Title { get; set; }
	}
}