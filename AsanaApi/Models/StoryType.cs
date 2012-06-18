using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public enum StoryType
	{
		/// <summary>
		/// A comment from a user. The text will be the message portion of the comment.
		/// </summary>
		Comment,
		/// <summary>
		/// A system-generated story based on a user action. The text will be a description of the action.
		/// </summary>
		System,
	}
}
