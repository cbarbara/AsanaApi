using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website.Models.BusyWeek
{
	public class BusyWeekModel
	{
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
	}

	public class TasksSearchQuery
	{
		public string Start { get; set; }
		public string End { get; set; }
		public List<long> Workspaces { get; set; }
	}

	public class TasksSearchResults
	{
		public Task[] StartedDuringRange { get; set; }

		public Task[] CompletedDuringRange { get; set; }

		public Task[] StartedAndCompletedDuringRange { get; set; }
	}
}