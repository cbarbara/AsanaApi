using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website.Models.BusyWeek
{
	[Serializable]
	public class BusyWeekModel
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	[Serializable]
	public class TasksSearchQuery
	{
		public string Start { get; set; }
		public string End { get; set; }
		public List<long> Workspaces { get; set; }
	}

	[Serializable]
	public class TasksSearchResults
	{
		public BusyTask[] StartedDuringRange { get; set; }
		public BusyTask[] CompletedDuringRange { get; set; }
		public BusyTask[] StartedAndCompletedDuringRange { get; set; }
	}

	[Serializable]
	public class BusyTask
	{
		public BusyTask() { }
		public BusyTask( Task task )
		{
			this.Name = task.Name;
			this.CreatedAt = task.CreatedAt;
			this.CompletedAt = task.CompletedAt;
			
			if( this.CompletedAt.HasValue )
			{
				this.TimeTaken = Shared.ToRelativeTime( this.CompletedAt.Value - this.CreatedAt );
			}
			else
			{
				this.CurrentRunTime = Shared.ToRelativeTime( DateTime.UtcNow - this.CreatedAt );
			}
		}

		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? CompletedAt { get; set; }
		public string TimeTaken { get; set; }
		public string CurrentRunTime { get; set; }
	}
}