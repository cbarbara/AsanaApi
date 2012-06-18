using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website.Models.Tasks
{
	public class TasksListModel
	{
		public Task[] Tasks { get; set; }

		public long ProjectId { get; set; }

		public string ProjectName { get; set; }

		public bool ShowCompleteTaskDetails { get; set; }
	}
}