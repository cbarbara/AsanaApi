using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanaApi.Models
{
	public enum SchedulingStatus
	{
		/// <summary>
		/// In the inbox.
		/// </summary>
		Inbox,
		/// <summary>
		/// Scheduled for later.
		/// </summary>
		Later,
		/// <summary>
		/// Scheduled for today.
		/// </summary>
		Today,
		/// <summary>
		/// Marked as upcoming.
		/// </summary>
		Upcoming
	}
}
