using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsanaApi.Service;

namespace Website.Controllers
{
	public class StoriesController : Controller
	{
		public ActionResult TaskStories( string id )
		{
			ViewBag.Message = "Task Stories";

			Website.Models.Stories.TaskStoriesListModel model = new Models.Stories.TaskStoriesListModel()
			{
				Task = new TasksService( Shared.API_KEY ).GetTask( long.Parse( id ) )
			};
			model.Stories = new StoriesService( Shared.API_KEY ).GetStories( model.Task, StoriesService.AllOptionalFieldsForStories );

			return View( model );
		}
	}
}
