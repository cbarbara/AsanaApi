using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsanaApi.Models;

namespace AsanaApi.Service
{
	public class UsersService : AsanaApiRequest
	{
		public UsersService( string ApiKey )
			: base( ApiKey )
		{
		}

		static OptionalFields[] _allFields = new OptionalFields[] { OptionalFields.User_Name, OptionalFields.User_Email, OptionalFields.User_Workspaces, OptionalFields.User_Workspaces_Name };
		public static OptionalFields[] AllOptionalFieldsForUsers
		{
			get
			{
				return _allFields;
			}
		}

		public User GetUser()
		{
			return GetUser( "me" );
		}

		public User GetUser( long UserId )
		{
			return GetUser( UserId.ToString() );
		}

		private User GetUser( string userId )
		{
			return ObjectConversions.ToUser( ExecuteRequest( "/users/" + userId ) );
		}

		public User[] GetUsers( OptionalFields[] returnedFields = null )
		{
			return GetUsers( "/users", returnedFields );
		}
		public User[] GetUsersInWorkspace( long workspaceId, OptionalFields[] returnedFields = null )
		{
			return GetUsers( "/workspaces/" + workspaceId.ToString() + "/users", returnedFields );
		}

		private User[] GetUsers( string urlPath, OptionalFields[] returnedFields )
		{
			return ObjectConversions.ToUsers( ExecuteRequest( urlPath, returnedFields, ",name,id" ) );
		}
	}
}
