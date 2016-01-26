using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.Filters
{
    public class TimingFilterAttribute : ActionFilterAttribute
    {
		private Stopwatch _stopwatch;
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			_stopwatch = Stopwatch.StartNew();
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			Debug.WriteLine($"Action ended in {_stopwatch.Elapsed}");
		}

		public override void OnResultExecuting(ResultExecutingContext context)
		{
			Debug.WriteLine($"Result started after {_stopwatch.Elapsed}");
		}

		public override void OnResultExecuted(ResultExecutedContext context)
		{
			Debug.WriteLine($"Result ended after {_stopwatch.Elapsed}");
		}
	}
}
