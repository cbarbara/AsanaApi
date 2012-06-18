using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsanaApi.Models;

namespace Website.Models.Users
{
	public class UsersListModel
	{
		public User[] WorkspaceUsers { get; set; }
		
		public bool ShowCompleteUserDetails { get; set; }
	}
}