using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsanaApi.Service;

namespace Website.Controllers
{
	public class ProjectsController : Controller
	{
		public ActionResult Index( string option )
		{
			ViewBag.Message = "Projects";

			Website.Models.Projects.ProjectsListModel model = new Models.Projects.ProjectsListModel()
			{
				Projects = Shared.Projects,
				DefaultProjectIndex = Shared.DefaultProjectIndex
			};

			return View( model );
		}

		public ActionResult ChangeDefaultProject( string i )
		{
			int newIndex = -1;
			if( int.TryParse( i, out newIndex ) )
			{
				Shared.DefaultProjectIndex = newIndex;
			}

			return RedirectToAction( "Index" );
		}
	}
}
