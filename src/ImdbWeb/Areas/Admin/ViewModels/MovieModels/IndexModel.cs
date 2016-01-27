using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Areas.Admin.ViewModels.MovieModels
{
    public class IndexModel
    {
		public string Id { get; set; }
		public int RunningLength { get; internal set; }
		public string Title { get; set; }
	}
}
