using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public class User : Assignee
	{
		public string Email { get; set; }

		public Workspace[] Workspaces { get; set; }
	}
}
