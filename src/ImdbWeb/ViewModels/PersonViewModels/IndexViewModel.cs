using ImdbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.ViewModels.PersonViewModels
{
    public class IndexViewModel
    {
		public string Title { get; set; }
		public IEnumerable<Person> Persons { get; set; }
	}
}
