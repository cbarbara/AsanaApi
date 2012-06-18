using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models.BusyWeek;
using AsanaApi.Service;
using AsanaApi.Models;

namespace Website.Controllers
{
	public class BusyWeekController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Did you get a lot accomplished this week?";

			DateTime startDay, endDay;
			FindWeekRange( out startDay, out endDay );

			BusyWeekModel model = new BusyWeekModel()
			{
				StartDate = startDay,
				EndDate = endDay
			};

			return View( model );
		}

		private void FindWeekRange( out DateTime startOfWeek, out DateTime endOfWeek )
		{
			DateTime now = DateTime.UtcNow.Date;
			while( now.DayOfWeek != DayOfWeek.Sunday )
			{
				now = now.AddDays( -1 );
			}
			endOfWeek = now;
			startOfWeek = endOfWeek.AddDays( -7 );
		}


		[HttpPost]
		public JsonResult GetWorkspaces()
		{
			return Json( new WorkspacesService( Shared.API_KEY ).GetWorkspaces() );
		}

		[HttpPost]
		public JsonResult GetTasks( TasksSearchQuery data )
		{
			TasksService tasksService = new TasksService( Shared.API_KEY );
			List<Task> allTasks = new List<Task>();
			Task[] tasks = null,
				started = null,
				completed = null;
			
			foreach( var item in data.Workspaces )
			{
				tasks = tasksService.GetTasks( new TasksQuery() { AssigneeId = Shared.Me.Id, WorkspaceId = item, ReturnCompleteTaskRecords = true } );
				if( tasks != null && tasks.Length > 0 )
				{
					allTasks.AddRange( tasks );
				}
			}

			GetTasks( DateTime.Parse( data.Start ), DateTime.Parse( data.End ), allTasks, out tasks, out started, out completed );

			TasksSearchResults results = new TasksSearchResults()
			{
				StartedDuringRange = started,
				CompletedDuringRange = completed,
				StartedAndCompletedDuringRange = tasks
			};
			return Json( results );
		}

		private void GetTasks( DateTime startDateUtc, DateTime endDateUtc, List<Task> allTasks, out Task[] startedAndCompleted, out Task[] started, out Task[] completed )
		{
			endDateUtc = endDateUtc.AddDays( 1 );

			startedAndCompleted = null;
			started = null;
			completed = null;

			List<Task> startedTasks = new List<Task>(),
				completedTasks = new List<Task>(),
				startedAndCompletedTasks = new List<Task>();

			foreach( var t in allTasks )
			{
				if( t.Completed )
				{
					if( t.CompletedAt.HasValue )
					{
						if( t.CreatedAt >= startDateUtc && t.CompletedAt.Value < endDateUtc )
						{
							startedAndCompletedTasks.Add( t );
						}
						else if( t.CompletedAt.Value < endDateUtc )
						{
							completedTasks.Add( t );
						}
					}
				}
				else
				{
					if( t.CreatedAt >= startDateUtc )
					{
						startedTasks.Add( t );
					}
				}
			}

			if( startedAndCompletedTasks.Count > 0 )
			{
				startedAndCompleted = startedAndCompletedTasks.ToArray();
			}
			if( startedTasks.Count > 0 )
			{
				started = startedTasks.ToArray();
			}
			if( completedTasks.Count > 0 )
			{
				completed = completedTasks.ToArray();
			}
		}
	}
}