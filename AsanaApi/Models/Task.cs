using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public class Task : TargetBase, ITarget
	{
		public Assignee Assignee { get; set; }

		public SchedulingStatus? AssigneeStatus { get; set; }

		public bool Completed { get; set; }

		public DateTime? CompletedAt { get; internal set; }

		public DateTime? DueDate { get; set; }

		public Project[] Projects { get; set; }
	}
}
