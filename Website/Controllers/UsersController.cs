using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models.Users;
using AsanaApi.Service;

namespace Website.Controllers
{
	public class UsersController : Controller
	{
		public ActionResult Index( string option )
		{
			ViewBag.Message = "Users";
			
			bool getFullDetails = false;
			if( string.IsNullOrEmpty( option ) == false )
			{
				bool.TryParse( option, out getFullDetails );
			}

			UsersListModel model = new UsersListModel()
			{
				WorkspaceUsers = new UsersService( Shared.API_KEY ).GetUsers( ( getFullDetails ? UsersService.AllOptionalFieldsForUsers : null ) ),
				ShowCompleteUserDetails = getFullDetails
			};

			return View( model );
		}
	}
}