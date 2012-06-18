using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsanaApi.Service;

namespace Website.Controllers
{
	public class WorkspacesController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Workspaces";

			Website.Models.Workspaces.WorkspacesListModel model = new Models.Workspaces.WorkspacesListModel()
			{
				Workspaces = new AsanaApi.Service.WorkspacesService(Shared.API_KEY).GetWorkspaces()
			};

			return View( model );
		}
	}
}
