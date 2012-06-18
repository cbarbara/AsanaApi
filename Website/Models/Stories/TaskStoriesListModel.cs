using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website.Models.Stories
{
	public class TaskStoriesListModel
	{
		public Task Task { get; set; }

		public Story[] Stories { get; set; }
	}
}