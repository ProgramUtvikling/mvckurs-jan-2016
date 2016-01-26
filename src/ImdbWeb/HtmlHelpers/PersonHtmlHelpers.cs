using ImdbDAL;
using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.HtmlHelpers
{
    public static class PersonHtmlHelpers
    {
		public static string PrettyJoin(this IHtmlHelper html, IEnumerable<Person> persons)
		{
			int count = 0;
			string res = null;
			foreach (var person in persons)
			{
				switch (count++)
				{
					case 0:
						res = person.Name;
						break;

					case 1:
						res = person.Name + " and " + res;
						break;

					default:
						res = person.Name + ", " + res;
						break;
				}
			}

			return res;
		}
    }
}
