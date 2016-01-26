using ImdbDAL;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.HtmlHelpers
{
    public static class PersonHtmlHelpers
    {
		public static HtmlString PrettyJoin(this IHtmlHelper html, IEnumerable<Person> persons)
		{
			var encoder = new HtmlEncoder();
			Func<Person, string> linkify = p =>
			{
				using (var w = new StringWriter())
				{
					html.ActionLink(p.Name, "Details", "Person", new { id = p.PersonId }).WriteTo(w, encoder);
					return w.ToString();
				}
			};


			int count = 0;
			string res = null;
			foreach (var person in persons)
			{
				switch (count++)
				{
					case 0:
						res = linkify(person);
						break;

					case 1:
						res = linkify(person) + " and " + res;
						break;

					default:
						res = linkify(person) + ", " + res;
						break;
				}
			}

			return new HtmlString(res);
		}
    }
}
