using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public abstract class TargetBase : AsanaApiBase, ITarget
	{
		public DateTime CreatedAt { get; set; }

		public Assignee[] Followers { get; set; }

		public DateTime? ModifiedAt { get; set; }

		public string Name { get; set; }

		public string Notes { get; set; }

		public Workspace Workspace { get; set; }
	}
}
