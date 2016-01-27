using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Areas.Admin.ViewModels.MovieModels
{
    public class CreateModel
    {
		[Display(Name ="ID/EAN")]
		[Required]
		[MaxLength(30)]
		public string MovieId { get; set; }

		[Display(Name = "Title")]
		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Display(Name = "Original Title")]
		[MaxLength(100)]
		public string OriginalTitle { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

		[Display(Name = "Production Year")]
		[MaxLength(4)]
		public string ProductionYear { get; set; }

		[Display(Name = "Hours")]
		[Required]
		[Range(0, int.MaxValue/60-1)]
		public int RunningLengthHours { get; set; }

		[Display(Name = "Minutes")]
		[Required]
		[Range(0, 60)]
		public int RunningLengthMinutes { get; set; }

		[Display(Name = "Genre")]
		public int GenreId { get; set; }
	}
}
