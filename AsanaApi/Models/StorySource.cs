using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public enum StorySource
	{
		/// <summary>
		/// Via the Asana web app.
		/// </summary>
		Web,
		/// <summary>
		/// Via email.
		/// </summary>
		Email,
		/// <summary>
		/// Via the Asana mobile app.
		/// </summary>
		Mobile,
		/// <summary>
		/// Via the Asana API.
		/// </summary>
		Api,
		/// <summary>
		/// Unknown or unrecorded.
		/// </summary>
		unknown
	}
}
