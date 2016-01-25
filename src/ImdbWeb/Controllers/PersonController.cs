using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class PersonController : Controller
    {
        public string Actors()
        {
            return "PersonController.Actors()";
        }

        public string Producers()
        {
            return "PersonController.Producers()";
        }

        public string Directors()
        {
            return "PersonController.Directors()";
        }

		[Route("Person/{id:int}")]
        public string Details(int id)
        {
            return $"PersonController.Details({id})";
        }
    }
}
