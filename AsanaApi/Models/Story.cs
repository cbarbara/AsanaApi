using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public class Story : AsanaApiBase
	{
		public DateTime CreatedAt { get; set; }

		public Assignee CreatedBy { get; set; }

		public string Text { get; set; }

		public ITarget Target { get; set; }

		public StorySource? Source { get; set; }

		public StoryType? Type { get; set; }
	}
}
