using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsanaApi.Service;

namespace Website.Controllers
{
	public class TasksController : Controller
	{
		public ActionResult Index( string option )
		{
			ViewBag.Message = "Tasks";

			bool getFullDetails = false;
			if( string.IsNullOrEmpty( option ) == false )
			{
				bool.TryParse( option, out getFullDetails );
			}

			Website.Models.Tasks.TasksListModel model = new Models.Tasks.TasksListModel()
			{
				ProjectId = Shared.Projects[ Shared.DefaultProjectIndex ].Id,
				ProjectName = Shared.Projects[ Shared.DefaultProjectIndex ].Name,
				Tasks = new TasksService( Shared.API_KEY ).GetTasks( new TasksQuery() { AssigneeId = Shared.Me.Id, ProjectId = Shared.Projects[ Shared.DefaultProjectIndex ].Id, ReturnCompleteTaskRecords = getFullDetails } ),
				ShowCompleteTaskDetails = getFullDetails
			};

			return View( model );
		}

	}
}
