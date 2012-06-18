using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public interface ITarget
	{
		long Id { get; set; }
		
		DateTime CreatedAt { get; set; }

		Assignee[] Followers { get; set; }

		DateTime? ModifiedAt { get; set; }

		string Name { get; set; }

		string Notes { get; set; }

		Workspace Workspace { get; set; }
	}
}
