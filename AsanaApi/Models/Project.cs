using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public class Project : TargetBase, ITarget
	{
		public bool IsArchived { get; set; }
	}
}
