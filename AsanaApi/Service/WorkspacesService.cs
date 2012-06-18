using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsanaApi.Models;

namespace AsanaApi.Service
{
	public class WorkspacesService : AsanaApiRequest
	{
		public WorkspacesService( string ApiKey )
			: base( ApiKey )
		{
		}

		public Project[] GetProjects( long workspaceId )
		{
			return new ProjectsService( _apiKey ).GetProjectsInWorkspace( workspaceId );
		}

		public Workspace[] GetWorkspaces()
		{
			return GetWorkspaces( "/workspaces" );
		}

		private Workspace[] GetWorkspaces( string urlPath )
		{
			return ObjectConversions.ToWorkspaces( ExecuteRequest( urlPath ) );
		}
	}
}
