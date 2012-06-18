using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website.Models.Projects
{
	public class ProjectsListModel
	{
		public Project[] Projects { get; set; }

		public int DefaultProjectIndex { get; set; }
	}
}