using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsanaApi.Models;

namespace AsanaApi.Service
{
	public class ProjectsService : AsanaApiRequest
	{
		public ProjectsService( string ApiKey )
			: base( ApiKey )
		{
		}

		public Task[] GetTasks( long projectId )
		{
			return new TasksService( _apiKey ).GetTasksInProject( projectId );
		}

		public Project GetProject( long projectId, OptionalFields[] returnedFields = null )
		{
			return GetProject( "/projects/" + projectId.ToString(), returnedFields );
		}

		public Project[] GetProjects( OptionalFields[] returnedFields = null )
		{
			return GetProjects( "/projects", returnedFields );
		}
		public Project[] GetProjectsInWorkspace( long workspaceId, OptionalFields[] returnedFields = null )
		{
			return GetProjects( "/workspaces/" + workspaceId.ToString() + "/projects", returnedFields );
		}

		private dynamic MakeRequest( string urlPath, OptionalFields[] returnedFields )
		{
			return ExecuteRequest( urlPath, returnedFields, ",name,id" );
		}

		private Project[] GetProjects( string urlPath, OptionalFields[] returnedFields )
		{
			dynamic dProjects = MakeRequest( urlPath, returnedFields );

			return ObjectConversions.ToProjects( dProjects );
		}
		private Project GetProject( string urlPath, OptionalFields[] returnedFields )
		{
			dynamic dProject = MakeRequest( urlPath, returnedFields );

			return ObjectConversions.ToProject( dProject );
		}
	}
}
